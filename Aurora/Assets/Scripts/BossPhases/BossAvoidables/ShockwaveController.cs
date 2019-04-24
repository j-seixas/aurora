using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockwaveController : MonoBehaviour {
    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") { print("Shockwave hit!"); }
    }
}
