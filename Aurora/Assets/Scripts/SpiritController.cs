using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpiritController : MonoBehaviour {
    private Rigidbody rb;
    void Start() {
        this.rb = GetComponent<Rigidbody>();
    }

    void OnTriggerExit(Collider other) {
        if (other.tag == "Player") rb.velocity = rb.angularVelocity = Vector3.zero;
    }

    void OnTriggerStay(Collider other) {
        if (other.tag == "Player") {
            Vector3 magnetField = other.transform.position - transform.position;
            float multiplier = (10f - magnetField.magnitude) / 10f;
            rb.AddForce(10f * magnetField * multiplier);

            if (multiplier >= 0.85) {
                int spiritCount;
                Text spiritCountLabel = GameObject.Find("SpiritCount").GetComponent<Text>();
                int.TryParse(spiritCountLabel.text, out spiritCount);
                spiritCountLabel.text = (spiritCount + 1).ToString();

                Destroy(gameObject);
            }
        }
    }
}
