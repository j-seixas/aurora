using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttackController : MonoBehaviour
{

    
    private Rigidbody rb;
    public float speed = 20f;
    public int damage = 15;
    public float range = 20f;

    private Vector3 direction;

    private Vector3 initialPos;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialPos = new Vector3(transform.position.x,transform.position.y,transform.position.z);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(direction == Vector3.zero)
            return;
        transform.Translate(this.direction*speed*Time.deltaTime);
        if(Vector3.Distance(initialPos,this.transform.position) >= range){
            ObjectPooler.SharedInstance.FreePooledObject(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "PlayerBody"){
          PlayerController player = (PlayerController) other.gameObject.GetComponentInParent<PlayerController>();
          player.UpdateAttribute(GameManager.Attributes.Health, -this.damage);
            ObjectPooler.SharedInstance.FreePooledObject(this.gameObject);

        }

        if(other.tag == "Wall"){
            ObjectPooler.SharedInstance.FreePooledObject(this.gameObject);

        }
    }

    public void setDirection(Vector3 direction){
        this.direction = direction;
    }

   
}
