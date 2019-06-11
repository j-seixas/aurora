using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class MinionController : MonoBehaviour {
    public float lookRadius = 10f;

    public float speed = 10f;
    public float range = 3f;
    public int damage = 1;
    public float spawnTime = 2f;

    public float flashAnimDuration;
    public Material hitFlashMaterial;

    protected NavMeshAgent agent;
    protected Animator anim;

    protected Vector3 target;

    [SerializeField]
    protected int health = 100;

    private SkinnedMeshRenderer renderer;
    private Material originalMat;
    private Color32 originalColor;
    private IEnumerator coroutine;

    private Rigidbody rb;

    private Vector3 lastVelocity;

    private bool isBurning = false;
    private bool isSlowed = false;
    public bool isInSpawnCooldown = true;

    protected void Start () {
        agent = GetComponent<NavMeshAgent> ();
        anim = GetComponent<Animator> ();
        agent.speed = speed;
        agent.acceleration = speed;
        agent.updateRotation = true;
        agent.updatePosition = true;
        this.renderer = this.transform.GetChild (0).GetComponent<SkinnedMeshRenderer> ();
        this.originalMat = this.renderer.material;
        this.rb = GetComponent<Rigidbody>();
        this.originalColor = this.renderer.material.color;
        isInSpawnCooldown = true;
    }

    protected void Update () {
        if (health <= 0) {
            // Stop coroutine and reset material
            if(this.coroutine != null) StopCoroutine (this.coroutine);
            this.renderer.material = this.originalMat;
            this.renderer.material.color = this.originalColor;
            
            // On death, spawn a spirit in its place.
            GameObject spirit = ObjectPooler.SharedInstance.GetPooledObject ("Spirit");
            spirit.GetComponent<SpiritController> ().PositionSelf (transform);

            // Despawn minion.
            ObjectPooler.SharedInstance.FreePooledObject (gameObject);
        }
        if(agent.velocity != Vector3.zero){
            lastVelocity = agent.velocity;
           // PlaySoundWalking();  //sound effect of minion walking
        }

    }

    private void FixedUpdate() {
        
    }

    protected void OnDisable () {
        // Reset health so new minions won't have zero health.
        this.health = 100;
        this.isInSpawnCooldown = true;
    }

    protected void OnEnable() {
        Invoke("SpawnCooldown", spawnTime);
    }

    public void ReceiveDamage (int damage) {
        this.coroutine = FlashRed ();
        StartCoroutine (this.coroutine);
        this.health -= damage;
        PlaySoundHurt(); //sound effect  
        
    }

    public Collider checkForPlayer () {
        Collider[] minionRadius = Physics.OverlapSphere (transform.position, lookRadius);
        foreach (Collider col in minionRadius) {
            if (col.tag == "PlayerBody") return col;
        }
        return null;
    }

    public void goToPosition (Vector3 pos) {
        agent.SetDestination (pos);
    }

    public void SpawnCooldown() {
        isInSpawnCooldown = false;
    }

    protected void OnDrawGizmosSelected () {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere (transform.position, lookRadius);
    }

    public void SimpleKnockBack(int power){
        rb.AddForce(lastVelocity.normalized*-power,ForceMode.Impulse);
    }

    private IEnumerator FlashRed () {
        this.renderer.material = hitFlashMaterial;
        this.renderer.material.color = Color.red;
        yield return new WaitForSeconds (this.flashAnimDuration / 2);
        this.renderer.material = originalMat;
        this.renderer.material.color = originalColor;
        yield return new WaitForSeconds (this.flashAnimDuration / 2);
    }

    public void ApplyBurn(int damage, float rate, int ticks) {
        if (!this.isBurning)
            StartCoroutine(BurnEnumerator(damage, rate, ticks));
    }

    public void ApplySlow(float slow, float duration) {
        if (!this.isSlowed)
            StartCoroutine(SlowEnumerator(slow, duration));
    }

    private IEnumerator BurnEnumerator(int damage, float rate, int ticks) {
        this.isBurning = true;

        while (ticks-- > 0) {
            StartCoroutine(FlashRed());
            transform.Find("FlamesParticleEffect").GetComponent<ParticleSystem>().Play();
            this.health -= damage;
            yield return new WaitForSeconds(rate);
        }

        this.isBurning = false;
    }

    private IEnumerator SlowEnumerator(float slow, float duration) {     
        this.agent.speed = this.speed * slow;
        this.isSlowed = true;
        
        ParticleSystem particles = transform.Find("SnowflakesParticleEffect").GetComponent<ParticleSystem>();

        particles.Play();
        yield return new WaitForSeconds(duration);
        particles.Stop();

        this.agent.speed = this.speed;
        this.isSlowed = false;
    }
    
    private void OnParticleCollision(GameObject other) {

        // Whenever the fire ring particles collide with the minions, trigger a burn effect.
        if (other.transform.parent.CompareTag("FireGem")) {
            (int burn, float rate, int ticks) passive = other.GetComponentInParent<FireUpgrade>().GetPassiveParameters();
            this.ApplyBurn(passive.burn, passive.rate, passive.ticks);
        }

        // Whenever the frost volley particles collide with the minions, freeze them of a while.
        if (other.transform.parent.CompareTag("IceGem")) {
            (float slow, float duration) passive = other.GetComponentInParent<IceUpgrade>().GetPassiveParameters();
            this.ApplySlow(0.0f, passive.duration);
        }
    }

    void PlaySoundWalking(){
        System.Random rand = new System.Random();
        AudioManager.Instance.PlaySFX("walking" + rand.Next(1, 7) ); //aurora sound
    }
    
    void PlaySoundHurt(){
        System.Random rand = new System.Random();
        AudioManager.Instance.PlaySFX("minion_hurt_" + rand.Next(1, 3) ); //aurora sound
    }         

    public abstract bool Attack ();
}