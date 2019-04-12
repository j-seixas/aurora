using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    private Transform player, pivot;
    private float panSpeed = 10f;
    private Vector3 offset;

    void Start() {
        this.player = GameObject.FindWithTag("Player").transform;
        this.offset = player.position - transform.position;
        
        this.pivot.position = player.position;
        this.pivot.parent = player;
    }

    void Update() {
        transform.position = player.position - this.offset;
        transform.LookAt(player);


        /*
        if (Input.GetAxis("Mouse X") != 0) {
            transform.RotateAround(player.transform.position, Vector3.up * , panSpeed * Time.deltaTime);
        }
        if (Input.GetAxis("Mouse Y") != 0) {
            //print(Input.GetAxis("Mouse Y"));
            transform.RotateAround(player.transform.position, Vector3.down, panSpeed * Time.deltaTime);
        }
        */
    }
}
