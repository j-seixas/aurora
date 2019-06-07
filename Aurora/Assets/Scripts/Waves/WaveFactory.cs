using System.Collections;
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
        this.DespawnUpgrades(this.spawnedObjs);
        this.waves[0].SetActive(true);  // Enable the next wave.
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
        
        if (this.waves.Count != 0) this.SpawnUpgrades(spawnPoints);
        else{
            DissolveController controller = GameObject.FindGameObjectWithTag("Mountain").GetComponent<DissolveController>();
            controller.StartDissolving();
            controller.PlaySoundEffect();
        } 
    }

    public bool IsShoppingPhase() =>
        !this.waves.Exists(wave => wave.activeSelf);
}
