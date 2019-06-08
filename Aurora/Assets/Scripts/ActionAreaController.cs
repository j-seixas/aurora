using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionAreaController : MonoBehaviour {

    // This level's wave factory.
    public GameObject waveFactory;

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "PlayerBody") {
            waveFactory.SetActive(true);
            Destroy(gameObject);  // After the action area has been triggered, we don't want to accidentally trigger it again.
        }
    }
}
