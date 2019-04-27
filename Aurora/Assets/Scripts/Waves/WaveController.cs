using System;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class WaveController : MonoBehaviour {

    // The melee and ranged minion game objects.
    public GameObject minionMelee, minionRanged;

    // Wave settings.
    public WaveFactory.Settings settings;

    void Start() {
        // TODO: There's probably a more elegant way to do this.
        StartCoroutine(Spawn(true));
        StartCoroutine(Spawn(false));
    }

    public void Setup(WaveFactory.Settings settings) {
        this.settings = settings;
    }

    private IEnumerator Spawn(bool isMelee) {
        while (true) {
            yield return new WaitForSeconds(isMelee ? settings.freqMelee : settings.freqRanged);

            for (int i = 0; i < (isMelee ? settings.spawnMelee : settings.spawnRanged); i++) {
                Vector3 position = new Vector3(Random.Range(-20f, 20f), 0.75f, Random.Range(-20f, 20f));
                Instantiate(isMelee ? this.minionMelee : this.minionRanged, position, Quaternion.identity);
            }
        }
    }

    void Update() {
        if (this.settings.remainingTime - Time.deltaTime < 0) {
            print("Wave completed!");
            GameObject.Find(gameObject.name).SetActive(false);
        }
        else {
            this.settings.remainingTime -= Time.deltaTime;
            print(this.settings.remainingTime);
        }
    }
}
