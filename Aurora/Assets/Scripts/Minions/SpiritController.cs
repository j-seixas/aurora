using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpiritController : MonoBehaviour {
    private Transform player;
    private Text spiritCountLabel;

    private float isCollectableCountdown = 2.0f;

    void Start() {
        this.player = GameObject.FindGameObjectWithTag("Player").transform;
        spiritCountLabel = GameObject.Find("SpiritCount").GetComponent<Text>();
    }

    private void OnDisable() {
        this.isCollectableCountdown = 2.0f;  // Reset the collectable countdown because of object pool reutilization.
    }

    void Update() {
        Debug.Log(this.isCollectableCountdown);
        if (this.isCollectableCountdown > 0.0f) {
            this.isCollectableCountdown -= Time.deltaTime;
        }

        if (this.isCollectableCountdown <= 0.0f) {
            float distance = Vector3.Distance(transform.position, player.transform.position);

            // Collect spirit.
            if (distance >= 0.0f && distance < 0.50f) {
                int spiritCount;
                
                int.TryParse(spiritCountLabel.text, out spiritCount);
                spiritCountLabel.text = (spiritCount + 1).ToString();

                this.isCollectableCountdown = 2.0f;
                this.gameObject.SetActive(false);
            }
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Time.deltaTime * 100.0f * 1.5f / distance);
        }
    }

    public void PositionSelf(Transform minion) {
        this.transform.position = minion.position;
    }
}
