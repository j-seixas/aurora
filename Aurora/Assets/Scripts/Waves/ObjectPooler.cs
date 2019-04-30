using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour {
    public static ObjectPooler SharedInstance;
    
    public GameObject objectToPool;
    public int amountToPoll;

    private List<GameObject> pooled;
    
    // Initialize a singleton.
    void Awake() {
        SharedInstance = this;
    }

    void Start() {
        pooled = new List<GameObject>();
        for (int i = 0; i < amountToPoll; i++) {
            GameObject obj = Instantiate(objectToPool) as GameObject;
            obj.SetActive(false);
            pooled.Add(obj);
        }
    }

    public GameObject GetPooledObject() {
        for (int i = 0; i < pooled.Count; i++) {
            print(pooled[i]);
            if (!pooled[i].activeInHierarchy) {
                return pooled[i];
            }
        }
        return null;
    }

}
