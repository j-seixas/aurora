using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeEffect : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "MinionMelee" || other.tag == "MinionRanged") {
            Debug.Log("something!");
        }
    }
}
