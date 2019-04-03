using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public int essenceCount = 0;
    private Rigidbody rb;
    
    private void Start() {
        this.rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement * 50);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Essence")) {
            Destroy(other.gameObject); 
            essenceCount++;
        }  
    }

}
