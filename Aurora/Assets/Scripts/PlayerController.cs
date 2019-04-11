using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private Rigidbody rb;
    private float speed = 7.0f;

    void Start() {
        this.rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        if (Input.GetKey(KeyCode.W)) {
            this.rb.MovePosition(transform.position + transform.forward * Time.deltaTime * this.speed);  
        }
        if (Input.GetKey(KeyCode.S)) {
            this.rb.MovePosition(transform.position - transform.forward * Time.deltaTime * this.speed);  
        }

        //this.rb.MovePosition(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * Time.deltaTime);
    }
}
