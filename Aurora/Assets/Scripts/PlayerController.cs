using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    
    public Camera mainCam;
    
    private Rigidbody rb;
    private float speed = 10.0f;

    void Start() {
        this.rb = GetComponent<Rigidbody>();
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
