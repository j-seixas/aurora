using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockwaveSpawner : MonoBehaviour {
    public float spawnRate = 5.0f;
    public GameObject shockwave;
    private List<GameObject> shockwaves = new List<GameObject>();

    private void OnDisable() {
        CancelInvoke();
    }
    
    void Start() { 
        InvokeRepeating("Spawner", 0.0f, spawnRate);
    }

    void Spawner() {
        shockwaves.Add(Instantiate(shockwave, new Vector3(0, 0.3f, 0), Quaternion.identity));
    }

    void Update() {
    }
}
