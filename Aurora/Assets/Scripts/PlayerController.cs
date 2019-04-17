using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float speed;
    
    private Rigidbody rb;

    void Start() {
        this.rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        this.rb.MovePosition(transform.position + direction.normalized * Time.deltaTime * this.speed);
        
        if (direction != Vector3.zero)
            this.rb.MoveRotation(Quaternion.LookRotation(direction));
    }
}
