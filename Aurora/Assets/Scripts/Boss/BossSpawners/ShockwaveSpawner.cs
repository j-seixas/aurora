using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockwaveSpawner : MonoBehaviour {
    public float spawnRate = 5.0f;
    public GameObject shockwave;
    private List<GameObject> shockwaves = new List<GameObject>();

    private WaveFactory waveFactory;

    private void OnDisable() {
        CancelInvoke();
    }
    
    void Start() { 
        waveFactory = GameObject.FindGameObjectWithTag("WaveFactory").GetComponent<WaveFactory>();
        InvokeRepeating("Spawner", 0.0f, spawnRate);
    }

    void Spawner() {
        if(waveFactory != null && waveFactory.IsShoppingPhase()){
             return;
        }
        shockwaves.Add(Instantiate(shockwave, transform.position, Quaternion.identity));
    }

    void Update() {
        
    }
}
