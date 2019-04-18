using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    private Transform player;
    private Vector3 offset;

    void Start() {
        this.player = GameObject.FindWithTag("Player").transform;
        this.offset = player.position - transform.position;
    }

    void Update() {
        transform.position = player.position - this.offset;
    }
}
