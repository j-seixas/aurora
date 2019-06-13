using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionAreaController : MonoBehaviour {

    private Animator bossAnimator;

    private void Start() {
        bossAnimator = GameObject.FindGameObjectWithTag("Mountain").GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "PlayerBody") {
            GameObject.FindGameObjectWithTag("WaveFactory").GetComponent<WaveFactory>().NextWave();
            bossAnimator.SetTrigger("Wake");
            // After the action area has been triggered, don't accidentally trigger it again.
            Destroy(gameObject);
        }
    }
}
