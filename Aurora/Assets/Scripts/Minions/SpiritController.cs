using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpiritController : MonoBehaviour {
    private Transform player;
    void Start() {
        this.player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void OnTriggerStay(Collider other) {
        if (other.tag == "Player") {
            float distance = Vector3.Distance(transform.position, other.transform.position);
            
            // Collect spirit.
            if (distance >= 0.0f && distance < 0.50f) {
                int spiritCount;
                Text spiritCountLabel = GameObject.Find("SpiritCount").GetComponent<Text>();
                int.TryParse(spiritCountLabel.text, out spiritCount);
                spiritCountLabel.text = (spiritCount + 1).ToString();

                Destroy(gameObject);
            }
            transform.position = Vector3.MoveTowards(transform.position, other.transform.position, Time.deltaTime * 100.0f * 0.6f/distance);
        }
    }
}
