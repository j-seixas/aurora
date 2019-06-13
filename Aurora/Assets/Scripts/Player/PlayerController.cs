using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public Camera mainCam;

    private Rigidbody rb;
    private float speed = 10.0f;
    private int hurtSfx = 0;

    [Header ("Health")]
    [SerializeField] private int health = 100;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int healthRegenAmount = 5;
    [SerializeField] private float healthRegenRate = 2;

    [Header ("Stamina")]
    [SerializeField] private int stamina = 100;
    [SerializeField] private int maxStamina = 100;
    [SerializeField] private int staminaRegenAmount = 1;
    [SerializeField] private float staminaRegenRate = 0.1f;

    [Header ("Spirits")]
    [SerializeField] private int spirits = 0;
    [SerializeField] private int maxSpirits = 100;

    [Header ("Attack")]
    [SerializeField] public GameObject weapon;

    [Header ("Upgrade")]
    public List<Upgrade> upgrades = new List<Upgrade> ();
    [SerializeField] private int active = -1;

    private HUDUpdater canvas;

    private Animator animator;

    private Vector3 lastPos;

    void Start () {
        this.rb = GetComponent<Rigidbody> ();
        this.canvas = GameObject.FindGameObjectWithTag ("Canvas").GetComponent<HUDUpdater> ();
        this.animator = GetComponent<Animator> ();
        this.lastPos = gameObject.transform.position;
        // Start regenerating health and stamina.
        //InvokeRepeating ("RegenerateHealth", this.healthRegenRate, this.healthRegenRate);
        InvokeRepeating ("RegenerateStamina", this.staminaRegenRate, this.staminaRegenRate);
    }

    private void AdjustDeathPostProcessing () {
        PostProcessVolume ppv = GameObject.FindGameObjectWithTag ("PostProcessing").GetComponent<PostProcessVolume> ();
        ColorGrading colorGradingLayer = null;

        ppv.profile.TryGetSettings (out colorGradingLayer);
        colorGradingLayer.enabled.value = true;

        colorGradingLayer.temperature.value = 0;
        colorGradingLayer.tint.value = 0;

        colorGradingLayer.postExposure.value = 0;
        colorGradingLayer.saturation.value = -100;
        colorGradingLayer.colorFilter = new ColorParameter { value = new Color (0.33f, 0.24f, 0.24f, 1.0f) };
    }

    private IEnumerator PlayDeathAnimation () {
        Animator animator = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Animator>();
        animator.SetBool("IsDead", true);
        this.AdjustDeathPostProcessing();

        // Slow motion on death.
        Time.timeScale = 0.5f;

        // Stop current wave timer.
        GameObject.FindGameObjectWithTag("WaveFactory").GetComponent<WaveFactory>().StopTimer();

        yield return new WaitForSeconds(5);

        // Send player to main menu.
        SceneManager.LoadScene(0);
    }

    public bool IsDead () =>
        this.health <= 0;

    public int GetAttribute (GameManager.Attributes attr) {
        if (attr == GameManager.Attributes.Health)
            return this.health;

        if (attr == GameManager.Attributes.Stamina)
            return this.stamina;

        if (attr == GameManager.Attributes.Spirits)
            return this.spirits;

        return -1;
    }

    public void UpdateAttribute (GameManager.Attributes attr, int inc) {
        if (attr == GameManager.Attributes.Health) {

            // Check whether the player has shield charges.
            // If so, reduce a charge and don't deal damage.
            LifeUpgrade lifeUpgradeScript = GetComponentInChildren<LifeUpgrade> ();

            if (lifeUpgradeScript.HasShieldActive () && inc < 0) {
                lifeUpgradeScript.BreakShield ();
                return;
            }

            int val = this.health + inc;

            if (inc < 0) {
                this.animator.SetTrigger ("tookDamage");
                if (this.health > 0)
                    PlaySoundHurt ();
            }

            if (val > this.maxHealth) this.health = this.maxHealth;
            else if (val < 0) this.health = 0;
            else this.health += inc;

        }

        if (attr == GameManager.Attributes.Stamina) {
            int val = this.stamina + inc;

            if (val > this.maxStamina) this.stamina = this.maxStamina;
            else if (val < 0) this.stamina = 0;
            else this.stamina = val;
        }

        if (attr == GameManager.Attributes.Spirits) {
            int val = this.spirits + inc;

            if (val > this.maxSpirits) this.spirits = this.maxSpirits;
            else if (val < 0) this.spirits = 0;
            else this.spirits = val;
        }
    }

    private void SwitchLevel (int index) {
        // Deactivate previous upgrade only if there's only active.
        if (this.active != -1)
            upgrades[this.active].SetActive (false);

        upgrades[index].SetActive (true);
        this.active = index;
        Debug.Log ("Selected " + upgrades[index].tag + " upgrade.");
    }

    void Update () {
        // Update the UI elements.
        this.canvas.UpdateSlider ("Health", this.health);
        this.canvas.UpdateSlider ("Stamina", this.stamina);
        this.canvas.UpdateSlider ("Essence", this.spirits);
        this.canvas.UpdateCooldownStatus ();

        if (health <= 0) {
            StartCoroutine (this.PlayDeathAnimation());
            return;
        }

        // Process inputs.
        if (Input.GetButtonDown ("Attack")) {
            this.animator.SetBool ("Attack", true);
        }

        // Process dash ability.
        if (Input.GetButtonDown ("Dash")) {
            GetComponent<PlayerDash> ().Perform ();
        }

        if (Input.GetButtonDown ("QuickSwitchLeft")) {
            for (int i = active - 1; i >= 0; i--) {
                if (upgrades[i].GetLevel () > 0) { this.SwitchLevel (i); break; }
            }
        }

        if (Input.GetButtonDown ("QuickSwitchRight")) {
            for (int i = active + 1; i < upgrades.Count; i++) {
                if (upgrades[i].GetLevel () > 0) { this.SwitchLevel (i); break; }
            }
        }

        if (Input.GetButtonDown ("Ability")) {
            if (this.active != -1) this.upgrades[this.active].Active ();
        }

        // TODO: The button here should be changed.
        if (Input.GetButtonDown ("Dash") && GameObject.Find ("WaveFactory")) {
            WaveFactory waveFactory = GameObject.Find ("WaveFactory").GetComponent<WaveFactory> ();

            if (waveFactory.IsShoppingPhase ())
                waveFactory.NextWave ();
        }

    }

    public void UnlockUpgrade (string upTag) {
        for (int i = 0; i < this.upgrades.Count; i++) {
            if (this.upgrades[i].tag == upTag) { this.upgrades[i].LevelUp (); break; }
        }
    }

    void OnDrawGizmosSelected () {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere (transform.position, 4);
    }

    void FixedUpdate () {
        if (this.IsDead()) {
            return;
        }

        HandlePlayerMovement ();
        Vector3 currPos = gameObject.transform.position;
        if (currPos == this.lastPos) {
            this.animator.SetBool ("isMoving", false);
        } else {
            this.animator.SetBool ("isMoving", true);
        }
        this.lastPos = currPos;

    }

    private void RegenerateHealth () =>
        this.UpdateAttribute (GameManager.Attributes.Health, this.healthRegenAmount);

    private void RegenerateStamina () =>
        this.UpdateAttribute (GameManager.Attributes.Stamina, this.staminaRegenAmount);

    void HandlePlayerMovement () {
        Vector3 direction = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical")).normalized;
        direction = Quaternion.AngleAxis (mainCam.transform.localEulerAngles.y, Vector3.up) * direction; //take into account camera yaw/direction

        this.rb.MovePosition (transform.position + direction * Time.deltaTime * this.speed);

        if (direction != Vector3.zero)
            this.rb.MoveRotation (Quaternion.LookRotation (direction));
    }

    void ActivateCollider () {
        this.weapon.GetComponent<PlayerWeapon> ().ToggleWeaponCollider (true);
    }

    void DeactivateCollider () {
        this.weapon.GetComponent<PlayerWeapon> ().ToggleWeaponCollider (false);
    }

    public void AOEAttack () {
        Collider[] hitColliders = Physics.OverlapSphere (this.transform.position, 4);
        foreach (Collider collider in hitColliders) {
            if (collider.tag == "MinionMelee" || collider.tag == "MinionRanged") {
                weapon.GetComponent<PlayerWeapon> ().DealDamage (collider.gameObject, 0, false, false);
            }
        }
    }

    public void PlaySoundAttack (string i) {
        AudioManager.Instance.PlaySFX ("aurora_grunt_" + i); //aurora sound
        AudioManager.Instance.PlaySFX ("Attack" + i); //scythe sound
    }

    void PlaySoundHurt () {
        System.Random rand = new System.Random ();
        int aux = this.hurtSfx;

        while (aux == this.hurtSfx) { //checks if the new sound is equal to the previous one
            aux = rand.Next (1, 4);
        }
        this.hurtSfx = aux;

        AudioManager.Instance.PlaySFX ("aurora_hurt_" + this.hurtSfx); //aurora sound
    }
}