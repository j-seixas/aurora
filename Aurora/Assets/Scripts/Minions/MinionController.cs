using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class MinionController : MonoBehaviour {
    public float lookRadius = 10f;

    public float speed = 10f;
    public float range = 3f;
    public int damage = 1;

    public float flashAnimDuration;
    public Material hitFlashMaterial;

    protected NavMeshAgent agent;
    protected Animator anim;

    protected Vector3 target;

    [SerializeField]
    protected int health = 100;

    private MeshRenderer renderer;
    private Material originalMat;
    private Color32 originalColor;
    private IEnumerator coroutine;

    protected void Start () {
        agent = GetComponent<NavMeshAgent> ();
        anim = GetComponent<Animator> ();
        agent.speed = speed;
        agent.acceleration = speed;
        agent.updateRotation = true;
        agent.updatePosition = true;
        this.renderer = this.transform.GetChild (0).GetChild (0).GetComponent<MeshRenderer> ();
        this.originalMat = this.renderer.material;
        this.originalColor = this.renderer.material.color;
    }

    protected void Update () {
        if (health <= 0) {
            // On death, spawn a spirit in its place.
            GameObject spirit = ObjectPooler.SharedInstance.GetPooledObject ("Spirit");
            spirit.GetComponent<SpiritController> ().PositionSelf (transform);

            // Despawn minion.
            ObjectPooler.SharedInstance.FreePooledObject (gameObject);
        }
    }

    protected void OnDisable () {
        // Stop coroutine and reset material
        StopCoroutine (this.coroutine);
        this.renderer.material = this.originalMat;
        this.renderer.material.color = this.originalColor;

        // Reset health so new minions won't have zero health.
        this.health = 100;
    }

    public void ReceiveDamage (int damage) {
        this.coroutine = FlashRed ();
        StartCoroutine (this.coroutine);
        this.health -= damage;
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

    protected void OnDrawGizmosSelected () {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere (transform.position, lookRadius);
    }

    IEnumerator FlashRed () {
        this.renderer.material = hitFlashMaterial;
        this.renderer.material.color = Color.red;
        yield return new WaitForSeconds (this.flashAnimDuration / 2);
        this.renderer.material = originalMat;
        this.renderer.material.color = originalColor;
        yield return new WaitForSeconds (this.flashAnimDuration / 2);
    }

    public abstract bool Attack ();
}