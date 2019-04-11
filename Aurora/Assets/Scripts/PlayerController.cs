using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private Vector3 movement;
    private CharacterController controller;
    void Start() {
        this.controller = GetComponent<CharacterController>();
    }

    void Update() {
        this.movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        this.movement = transform.TransformDirection(this.movement) * 5.0f;
        this.controller.Move(this.movement * Time.deltaTime);
    }
}
