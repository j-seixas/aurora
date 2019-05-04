using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveFactory : MonoBehaviour {
    public GameObject waveController, upgradeObj;
    public List<Settings> settings = new List<Settings>();
    private List<GameObject> waves = new List<GameObject>();

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

        public MinionSettings(string tag, int minimum, float frequency, int spawnNo) {
            this.tag = tag;
            this.minimum = minimum;
            this.frequency = frequency;
            this.spawnNo = spawnNo;
        }
    }
    
    void Start() {
        this.waveController.SetActive(false);   // Waves aren't enabled by default.

        this.settings.ForEach(setting => {
            GameObject wave = Instantiate(waveController, Vector3.zero, Quaternion.identity);
            wave.GetComponent<WaveController>().Setup(setting);
            this.waves.Add(wave);
        });

        this.waves[0].SetActive(true);
    }

    public void NextWave() {
        if (this.waves.Count != 0)
            this.waves[0].SetActive(true);  // Enable the next wave.
        else
            GameObject.FindGameObjectWithTag("Mountain").GetComponent<DissolveController>().StartDissolving();
    }

    public void EndWave() {
        Destroy(this.waves[0]);
        this.waves.RemoveAt(0); // Remove the cleared wave.
        ObjectPooler.SharedInstance.ClearPool();  // Clear pooled game objects on the previous wave.

        Instantiate(upgradeObj, new Vector3(0, 2, 15), Quaternion.identity);
    }
}
