using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveFactory : MonoBehaviour {
    public GameObject waveController;
    public List<Settings> settings = new List<Settings>();
    private List<GameObject> waves = new List<GameObject>();

    [System.Serializable]
    public struct Settings {
        // Wave name.
        public string name;

        // The minimum no. of minions in the field at all times.
        public int minMelee, minRanged;

        // The spawn base frequency of minions.
        public float freqMelee, freqRanged;

        // The no. of minions spawned per rate.
        public float spawnMelee, spawnRanged;

        // The wave's remaining time. Always decreases.
        public float remainingTime;

        // Struct constructor.
        public Settings(string name, int minMelee, int minRanged, float freqMelee, float freqRanged, float spawnMelee, float spawnRanged, float remainingTime) {
            this.name = name;
            this.minMelee = minMelee; this.minRanged = minRanged;
            this.freqMelee = freqMelee; this.freqRanged = freqRanged;
            this.spawnMelee = spawnMelee; this.spawnRanged = spawnRanged;
            this.remainingTime = remainingTime;
        }
    }
    
    void Start() {
        this.waveController.SetActive(false);   // Waves aren't enabled by default.

        this.settings.ForEach(setting => {
            GameObject wave1 = Instantiate(waveController, Vector3.zero, Quaternion.identity);
            wave1.GetComponent<WaveController>().Setup(setting);
            this.waves.Add(wave1);
        });

        this.waves[0].SetActive(true);
    }

    public void NextWave() {
        this.waves.RemoveAt(0); // Remove the cleared wave.
        ObjectPooler.SharedInstance.ClearPool();  // Clear pooled game objects on the previous wave.

        if (this.waves.Count != 0)
            this.waves[0].SetActive(true);  // Enable the next wave.
        else {
            GameObject.Find("Mountain").GetComponent<DissolveController>().StartDissolving();
        }
    }
}
