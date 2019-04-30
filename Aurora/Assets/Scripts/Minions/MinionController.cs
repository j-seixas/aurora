using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class MinionController : MonoBehaviour
{
    public float lookRadius = 10f;

    public float speed = 10f;
    private NavMeshAgent agent;
    private Animator anim;

    private Vector3 target;
    // Start is called before the first frame update
    void Start(){
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        agent.velocity = agent.velocity * speed;
    }

    // Update is called once per frame
    void Update(){
        
    }

    public Collider checkForPlayer(){
        Collider[] minionRadius = Physics.OverlapSphere(transform.position,lookRadius);
        foreach (Collider col in minionRadius){
            if(col.tag == "Player"){
                return col;
            }
        }
        return null;
    }
    public void goToPosition(Vector3 pos){
        agent.SetDestination(pos);
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,lookRadius);
    }
}
