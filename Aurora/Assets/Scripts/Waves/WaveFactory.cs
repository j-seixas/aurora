using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveFactory : MonoBehaviour {
    public GameObject waveController;
    private List<GameObject> waves = new List<GameObject>();

    public struct Settings {
        // The minimum no. of minions in the field at all times.
        public int minMelee, minRanged;

        // The spawn base frequency of minions.
        public float freqMelee, freqRanged;

        // The no. of minions spawned per rate.
        public float spawnMelee, spawnRanged;

        // The wave's remaining time. Always decreases.
        public float remainingTime;

        // Struct constructor.
        public Settings(int minMelee, int minRanged, float freqMelee, float freqRanged, float spawnMelee, float spawnRanged, float remainingTime) {
            this.minMelee = minMelee; this.minRanged = minRanged;
            this.freqMelee = freqMelee; this.freqRanged = freqRanged;
            this.spawnMelee = spawnMelee; this.spawnRanged = spawnRanged;
            this.remainingTime = remainingTime;
        }
    }
    
    void Start() {
        GameObject wave1 = Instantiate(waveController, Vector3.zero, Quaternion.identity);
        wave1.GetComponent<WaveController>().Setup(new Settings(5, 5, 5, 5, 5, 5, 60));
        this.waves.Add(wave1);
    }
}
