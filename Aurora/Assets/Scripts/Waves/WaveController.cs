using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Random = UnityEngine.Random;

public class WaveController : MonoBehaviour {

    // The melee and ranged minion game objects.
    public GameObject minionMelee, minionRanged;

    // Wave settings.
    public WaveFactory.Settings settings;

    private Text waveTimeLabel;

    private bool canCount = true, doOnce = false;

    void Awake() {
        this.waveTimeLabel = GameObject.Find("WaveTime").GetComponent<Text>();
        GameObject.Find("WaveCount").GetComponent<Text>().text = this.settings.name;
    }

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

                string type = isMelee ? "MinionMelee" : "MinionRanged";
                GameObject minion = ObjectPooler.SharedInstance.GetPooledObject(type); 
                if (minion != null) {
                    minion.transform.position = position;
                    minion.transform.rotation = Quaternion.identity;
                    minion.SetActive(true);
                }
            
            }
        }
    }


    void Update() {
        if (this.settings.remainingTime - Time.deltaTime > 0.0f && this.canCount) {
            this.settings.remainingTime -= Time.deltaTime;
            this.waveTimeLabel.text = this.settings.remainingTime.ToString("F");
        }
        else if (this.settings.remainingTime - Time.deltaTime <= 0.0f && !this.doOnce) {
            this.canCount = false;
            this.doOnce = true;
            this.waveTimeLabel.text = "0.00";
            this.settings.remainingTime = 0.0f;
            GameObject.Find("WaveFactory").GetComponent<WaveFactory>().NextWave();
            GameObject.Find(gameObject.name).SetActive(false);
        }
    }
}
