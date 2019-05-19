using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitEffect : MonoBehaviour {
    private GameObject player;
    public float orbitDistance, orbitDegreesPerSec;

    private void StepOrbit(float time) {
        transform.position = player.transform.position + (transform.position - player.transform.position).normalized * orbitDistance;
        transform.RotateAround(player.transform.position, Vector3.up, orbitDegreesPerSec * Time.deltaTime);
    }

    private void Start() {
        this.player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update() {
        this.StepOrbit(Time.deltaTime);
    }
}
