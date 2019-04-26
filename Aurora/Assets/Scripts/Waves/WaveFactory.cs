using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveFactory : MonoBehaviour {
    public GameObject waveController;
    private List<GameObject> waves = new List<GameObject>();
    
    void Start() {
        GameObject wave1 = Instantiate(waveController, Vector3.zero, Quaternion.identity);
        wave1.GetComponent<WaveController>().Setup(10, 5f, 2, 1, 30f);
        this.waves.Add(wave1);
    }

    void Update() {
        
    }

}
