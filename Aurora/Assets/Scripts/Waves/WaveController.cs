﻿using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Random = UnityEngine.Random;

public class WaveController : MonoBehaviour {
    public GameObject minionMelee, minionRanged;
    public WaveFactory.Settings settings;
    private Text waveTimeLabel;

    void Awake() {
        this.waveTimeLabel = GameObject.Find("WaveTime").GetComponent<Text>();
        GameObject.Find("WaveName").GetComponent<Text>().text = this.settings.name;
    }

    void Start() {
        settings.minionSettings.ForEach(minion => StartCoroutine(Spawn(minion)));
    }

    public void Setup(WaveFactory.Settings settings) {
        this.settings = settings;
    }

    public void StepSpawn(WaveFactory.MinionSettings minion, float toSpawn) {
        for (int i = 0; i < toSpawn; i++) {
            Vector3 position = new Vector3(Random.Range(-20f, 20f), 0.75f, Random.Range(-20f, 20f));
            GameObject minionObj = ObjectPooler.SharedInstance.GetPooledObject(minion.tag);

            if (minionObj != null) {
                minionObj.transform.position = position;
                minionObj.transform.rotation = Quaternion.identity;
            }
        }
    }

    private IEnumerator Spawn(WaveFactory.MinionSettings minion) {
        while (true) {
            this.StepSpawn(minion, minion.spawnNo);
            minion.frequency *= 1 / minion.multiplier;
            yield return new WaitForSeconds(minion.frequency);
        }
    }


    void Update() {

        // If the minion count doesn't reach the minimum, spawn the remaining ones.
        this.settings.minionSettings.ForEach(minion => {
            int toSpawn = minion.minimum - ObjectPooler.SharedInstance.GetActiveObjectCount(minion.tag);
            this.StepSpawn(minion, toSpawn);
        });

        if (settings.remainingTime - Time.deltaTime > 0.0f) {
            settings.remainingTime -= Time.deltaTime;
            this.waveTimeLabel.text = settings.remainingTime.ToString("F");
        }
        else if (settings.remainingTime - Time.deltaTime <= 0.0f) {
            this.waveTimeLabel.text = "0.00";
            settings.remainingTime = 0.0f;
            GameObject.Find("WaveFactory").GetComponent<WaveFactory>().EndWave();
        }
    }
}
