using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpiritController : MonoBehaviour {
    private Transform player;
    private bool playerInRange = false;
    private Text spiritCountLabel;

    void Start() {
        this.player = GameObject.FindGameObjectWithTag("Player").transform;
        spiritCountLabel = GameObject.Find("SpiritCount").GetComponent<Text>();
    }

    void Update() {
        if (playerInRange) {
            float distance = Vector3.Distance(transform.position, player.transform.position);

            // Collect spirit.
            if (distance >= 0.0f && distance < 0.50f) {
                int spiritCount;
                
                int.TryParse(spiritCountLabel.text, out spiritCount);
                spiritCountLabel.text = (spiritCount + 1).ToString();

                Destroy(gameObject);
            }
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Time.deltaTime * 100.0f * 0.6f / distance);
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player")
            playerInRange = true;
    }
}
