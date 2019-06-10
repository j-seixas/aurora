using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitEffect : MonoBehaviour {
    private GameObject player;
    public float orbitDistance = 10f, orbitDegreesPerSec=180f;

    public Vector3 relativeDistance = Vector3.zero;

    private void StepOrbit() {
        if(player != null)
         {
             transform.position = player.transform.position + relativeDistance;
             transform.RotateAround(player.transform.position, Vector3.up, orbitDegreesPerSec * Time.deltaTime);
            // Reset relative position after rotate
             relativeDistance = transform.position - player.transform.position;
         }
    }

    private void Start() {
        this.player = GameObject.FindGameObjectWithTag("PlayerBody");
        relativeDistance = transform.position - player.transform.position;
    }

    private void FixedUpdate() {
        this.StepOrbit();
    }
}
