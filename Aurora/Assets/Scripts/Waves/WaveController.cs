using System;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class WaveController : MonoBehaviour {

    // The minimum no. of minions in the field at all times.
    public int minCount;

    // The spawn base frequency of minions.
    public float spawnRateBase;

    // The no. of minions spawned per rate.
    public int spawnPerRate;

    // A multiplier which affects the base spawn rate. Changes with more time elapsed.
    public float spawnRateMultiplier;
    
    // The wave's remaining time. Always decreases.
    public float remainingTime;

    // The melee and ranged minion game objects.
    public GameObject minionMelee, minionRanged;

    void Start() {
        StartCoroutine(SpawnMinion(1, 2f));
    }

    public void Setup(int minCount, float spawnRateBase, int spawnPerRate, float spawnRateMultiplier, float remainingTime) {
        this.minCount = minCount; 
        this.spawnRateBase = spawnRateBase;
        this.spawnPerRate = spawnPerRate;
        this.spawnRateMultiplier = spawnRateMultiplier;
        this.remainingTime = remainingTime;
    }

    private IEnumerator SpawnMinion(int spawnPerRate, float spawnRateBase) {
        while (true) {
            yield return new WaitForSeconds(spawnRateBase);

            for (int i = 0; i < spawnPerRate; i++) {
                Instantiate(this.minionRanged, new Vector3(Random.Range(-20f, 20f), 0.75f, Random.Range(-20f, 20f)), Quaternion.identity);
            }
        }
    }


    void Update() {
        
    }
}
