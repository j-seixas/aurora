using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") print("Player hit!");
    }
}
