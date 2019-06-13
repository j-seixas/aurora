﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveFactory : MonoBehaviour {
    public GameObject waveController;
    public List<Settings> settings = new List<Settings>();
    private List<GameObject> waves = new List<GameObject>();

    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private List<GameObject> upgradeObjects = new List<GameObject>();
    private List<GameObject> spawnedObjs = new List<GameObject>();


    [System.Serializable]
    public struct Settings {
        public string name;
        public float remainingTime;
        public List<MinionSettings> minionSettings;

        public Settings(string name, int minMelee, int minRanged, float freqMelee, float freqRanged, float spawnMelee, float spawnRanged, float remainingTime) {
            this.name = name;
            this.remainingTime = remainingTime;
            this.minionSettings = new List<MinionSettings>();
        }
    }

    [System.Serializable]
    public struct MinionSettings {
        public string tag;
        public int minimum;
        public float frequency;
        public int spawnNo;
        public float multiplier;

        public MinionSettings(string tag, int minimum, float frequency, int spawnNo, float multiplier) {
            this.tag = tag;
            this.minimum = minimum;
            this.frequency = frequency;
            this.spawnNo = spawnNo;
            this.multiplier = multiplier;
        }
    }
    
    private void Start() {
        this.waveController.SetActive(false);   // Waves aren't enabled by default.

        this.settings.ForEach(setting => {
            GameObject wave = Instantiate(waveController, Vector3.zero, Quaternion.identity);
            wave.GetComponent<WaveController>().Setup(setting);
            this.waves.Add(wave);
        });
    }

    public void NextWave() {
        // Only ask for the next wave if it's not the last one.
        if (waves.Count == 0) {
            return;
        }
        
        // Despawn any upgrade gems that might be active somewhere on the scene.
        this.spawnPoints[0].GetComponentsInParent<Collider>(true)[0].enabled = false;
        this.DespawnUpgrades(this.spawnedObjs);

        // Enable the next wave.
        this.waves[0].SetActive(true); 

        // Restore shield charges on wave change.
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<LifeUpgrade>().RestoreActiveCharges();
    }

    private void SpawnUpgrades(List<Transform> spawnPoints) {
        for (int i = 0; i < this.spawnPoints.Count; i++)
            spawnedObjs.Add(Instantiate(this.upgradeObjects[i], this.spawnPoints[i].position, Quaternion.identity));
    }

    private void DespawnUpgrades(List<GameObject> upgradeObjs) {
        upgradeObjs.ForEach(up => Destroy(up));
        upgradeObjs.Clear();
    }

    public void EndWave() {
        Destroy(this.waves[0]);
        this.waves.RemoveAt(0); // Remove the cleared wave.
        ObjectPooler.SharedInstance.ClearPool();  // Clear pooled game objects on the previous wave.
        
        if (this.waves.Count != 0) {
            BossController boss = GameObject.FindGameObjectWithTag("Mountain").GetComponent<BossController>();
            StartCoroutine(boss.PlayWaveEndCutscene());
            this.SpawnUpgrades(spawnPoints);
        }
        else {
            DissolveController controller = GameObject.FindGameObjectWithTag("Mountain").GetComponent<DissolveController>();
            controller.StartDissolving();
            controller.PlaySoundEffect();
        }

        if (waves.Count != 0) {
            this.spawnPoints[0].GetComponentsInParent<BoxCollider>(true)[0].enabled = true;
        }
    }

    public void StopTimer() =>
        this.waves[0].GetComponent<WaveController>().StopTimer();

    public bool IsShoppingPhase() =>
        !this.waves.Exists(wave => wave.activeSelf);
}
