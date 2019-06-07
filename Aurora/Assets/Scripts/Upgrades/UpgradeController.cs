using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeController : MonoBehaviour {

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "PlayerBody") {
            
            foreach (Transform child in other.transform.parent.transform) {
                if (child.tag == tag) child.GetComponent<Upgrade>().PrintStatusPopup();
            }
            
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.tag == "PlayerBody" && Input.GetButtonDown("Accept")) {
            GameObject.Find("WaveFactory").GetComponent<WaveFactory>().NextWave();
            other.gameObject.transform.parent.gameObject.GetComponent<PlayerController>().UnlockUpgrade(tag);
            Destroy(gameObject);
        }
    }
}
