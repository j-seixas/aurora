using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    private GameObject player;
    private float panSpeed = 10f;
    private Vector3 offset;

    void Start() {
        this.player = GameObject.FindWithTag("Player");
        this.offset = transform.position - this.player.transform.position;
        transform.LookAt(this.player.transform);
    }

    void LateUpdate() {
        Vector3 newPos = this.player.transform.position + this.offset;
        transform.position = Vector3.Slerp(transform.position, newPos, 0.5f);

        if (Input.GetKey(KeyCode.Q)) {
            transform.RotateAround(player.transform.position, Vector3.up, panSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.E)) {
            transform.RotateAround(player.transform.position, Vector3.down, panSpeed * Time.deltaTime);
        }
    }
}
