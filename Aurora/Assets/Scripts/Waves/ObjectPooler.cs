using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour {
    public static ObjectPooler SharedInstance;
    
    private List<GameObject> pooled = new List<GameObject>();
    public GameObject objectToPool;
    public int amountToPoll;

    // Initialize a singleton.
    void Awake() {
        SharedInstance = this;
    }

    void Start() {
        for (int i = 0; i < this.amountToPoll; i++) {
            GameObject obj = Instantiate(this.objectToPool);
            obj.SetActive(false);
            this.pooled.Add(obj);
        }
    }

    void Update() {
        
    }
}
