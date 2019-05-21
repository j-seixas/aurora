using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    
    public Camera mainCam;
    
    private Rigidbody rb;
    private float speed = 10.0f;
    
    [SerializeField] private int health = 100;
    [SerializeField] private int maxHealth = 100;

    [SerializeField] private int stamina = 100;
    [SerializeField] private int maxStamina = 100;

    [SerializeField] private int spirits = 0;
    [SerializeField] private int maxSpirits = 100;
    
    [Header("Upgrade")]
    public List<Upgrade> upgrades = new List<Upgrade>();
    [SerializeField] private int active = -1;

    private HUDUpdater canvas;

    void Start() {
        this.rb = GetComponent<Rigidbody>();
        this.canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<HUDUpdater>();
    }

    public int GetAttribute(GameManager.Attributes attr) {
        if (attr == GameManager.Attributes.Health)
            return this.health;

        if (attr == GameManager.Attributes.Stamina)
            return this.stamina;

        if (attr == GameManager.Attributes.Spirits)
            return this.spirits;

        return -1;
    }

    public void UpdateAttribute(GameManager.Attributes attr, int inc) {
        if (attr == GameManager.Attributes.Health) {
            int val = this.health + inc;

            if (val > this.maxHealth) this.health = this.maxHealth;
            else if (val < 0) this.health = 0;
            else this.health += inc;

            // Update the UI.
            this.canvas.UpdateSlider("HealthUI", this.health);
        }

        if (attr == GameManager.Attributes.Stamina) {
            int val = this.stamina + inc;

            if (val > this.maxStamina) this.stamina = this.maxStamina;
            else if (val < 0) this.stamina = 0;
            else this.stamina = val;
            
            // Update the UI.
            this.canvas.UpdateSlider("StaminaUI", this.stamina);
        }

        if (attr == GameManager.Attributes.Spirits) {
            int val = this.spirits + inc;

            if (val > this.maxSpirits) this.spirits = this.maxSpirits;
            else if (val < 0) this.spirits = 0;
            else this.spirits = val;

            // Update the UI.
            this.canvas.UpdateSlider("EssenceUI", this.spirits);
        }
    }

    private void SwitchLevel(int index) {
        // Deactivate previous upgrade only if there's only active.
        if (this.active != -1)
            upgrades[this.active].SetActive(false);

        upgrades[index].SetActive(true);
        this.active = index;
        Debug.Log("Selected " + upgrades[index].tag + " upgrade.");
    }

    void Update() {

        // Process inputs.
        if (Input.GetButton("Start") || health <= 0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        // Process dash ability.
        if (Input.GetButtonDown("Dash")) {
            GetComponent<PlayerDash>().Perform();
        }

        if (Input.GetButtonDown("QuickSwitchLeft")) {
            for (int i = active - 1; i >= 0; i--) {
                if (upgrades[i].GetLevel() > 0) { this.SwitchLevel(i); break; }
            }
        }
        
        if (Input.GetButtonDown("QuickSwitchRight")) {
            for (int i = active + 1; i < upgrades.Count; i++) {
                if (upgrades[i].GetLevel() > 0) { this.SwitchLevel(i); break; }
            }
        }
        
        if (Input.GetButtonDown("Ability")) {
            if (this.active != -1) this.upgrades[this.active].Active();
        }
    }

    public void UnlockUpgrade(string upTag) {
        for (int i = 0; i < this.upgrades.Count; i++) {
            if (this.upgrades[i].tag == upTag) { this.upgrades[i].LevelUp(); break; }
        }
    }

    void FixedUpdate() {
        HandlePlayerMovement();
    }

    void HandlePlayerMovement(){
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        direction = Quaternion.AngleAxis(mainCam.transform.localEulerAngles.y, Vector3.up) * direction; //take into account camera yaw/direction
        
        this.rb.MovePosition(transform.position + direction * Time.deltaTime * this.speed);
        
        if (direction != Vector3.zero)
            this.rb.MoveRotation(Quaternion.LookRotation(direction));
    }
}
