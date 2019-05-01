using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour {
    public static ObjectPooler SharedInstance;

    public List<ObjectPoolItem> itemsToPool;

    [System.Serializable]
    public struct ObjectPoolItem {
        public int amountToPool;
        public GameObject objectToPool;
        public bool shouldExpand;
    }

    private List<GameObject> pool;
    
    // Initialize a singleton.
    void Awake () {
        SharedInstance = this;
    }

    void Start () {
        pool = new List<GameObject> ();
        foreach (ObjectPoolItem item in itemsToPool) {
            for (int i = 0; i < item.amountToPool; i++) {
                GameObject obj = Instantiate (item.objectToPool) as GameObject;
                obj.SetActive (false);
                pool.Add (obj);
            }
        }
    }

    public GameObject GetPooledObject (string tag) {
        for (int i = 0; i < pool.Count; i++) {
            if (!pool[i].activeInHierarchy && pool[i].tag == tag) {
                return pool[i];
            }
        }

        foreach (ObjectPoolItem item in itemsToPool) {
            if (item.objectToPool.tag == tag && item.shouldExpand) {
                GameObject obj = (GameObject) Instantiate(item.objectToPool);
                obj.SetActive(false);
                pool.Add(obj);
                return obj;
            }
        }
        return null;
    }

    public void ClearPool() {
        this.pool.ForEach(obj => obj.SetActive(false));
    }

}