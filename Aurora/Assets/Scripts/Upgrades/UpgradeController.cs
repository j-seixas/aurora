using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeController : MonoBehaviour {

    private GameObject billboard;

    private void Start() {
        foreach (Transform child in transform.parent) {
            if (child.name == "Billboard")
                this.billboard = child.gameObject;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "PlayerBody") {
            foreach (Transform child in other.transform.parent.transform) {
                if (child.tag == tag) {
                    this.billboard.GetComponentInChildren<TextMesh>().text = child.GetComponent<Upgrade>().GetUpgradeStatus();
                    this.billboard.SetActive(true);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "PlayerBody") {
            foreach (Transform child in other.transform.parent.transform) {
                if (child.tag == tag) {
                    this.billboard.SetActive(false);
                }
            }
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.tag == "PlayerBody" && Input.GetButtonDown("Accept")) {
            other.gameObject.transform.parent.gameObject.GetComponent<PlayerController>().UnlockUpgrade(tag);
            Destroy(gameObject);
        }
    }
}
