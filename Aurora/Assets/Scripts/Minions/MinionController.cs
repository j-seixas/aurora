using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public abstract class MinionController : MonoBehaviour {
    public float lookRadius = 10f;

    public float speed = 10f;
    public float range = 3f;
    public int damage = 1;
    protected NavMeshAgent agent;
    protected Animator anim;

    protected Vector3 target;

    [SerializeField]
    protected int health = 100;

        

    protected void Start() {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        agent.speed =  speed;
        agent.acceleration = speed;
        agent.updateRotation = true;
        agent.updatePosition = true;
    }

    protected void Update() {
        if (health <= 0) {
            // On death, spawn a spirit in its place.
            GameObject spirit = ObjectPooler.SharedInstance.GetPooledObject("Spirit");
            spirit.GetComponent<SpiritController>().PositionSelf(transform);

            // Despawn minion.
            ObjectPooler.SharedInstance.FreePooledObject(gameObject);
        }
    }

    protected void OnDisable() {
        // Reset health so new minions won't have zero health.
        this.health = 100;
    }

    public void ReceiveDamage(int damage) =>
        this.health -= damage;

    public Collider checkForPlayer(){
        Collider[] minionRadius = Physics.OverlapSphere(transform.position, lookRadius);
        foreach (Collider col in minionRadius) {
            if (col.tag == "PlayerBody") return col;
        }
        return null;
    }
   
    public void goToPosition(Vector3 pos){
        agent.SetDestination(pos);
    }

    protected void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,lookRadius);
    }

    public abstract bool Attack();
}
