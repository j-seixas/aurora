using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionAreaController : MonoBehaviour {

    // This level's wave factory.
    public GameObject waveFactory,projectileSpawner,shockwaveSpawner;

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "PlayerBody") {
            waveFactory.SetActive(true);
            projectileSpawner.SetActive(true);
            shockwaveSpawner.SetActive(true);
            Destroy(gameObject);  // After the action area has been triggered, we don't want to accidentally trigger it again.
        }
    }
}
