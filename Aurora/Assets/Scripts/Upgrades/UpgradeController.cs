using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeController : MonoBehaviour {
    private void OnTriggerStay(Collider other) {
        if (other.tag == "Player" && Input.GetButtonDown("Accept")) {
            GameObject.Find("WaveFactory").GetComponent<WaveFactory>().NextWave();
            other.gameObject.transform.parent.gameObject.GetComponent<PlayerController>().UnlockUpgrade(tag);
            Destroy(gameObject);
        }
    }
}
