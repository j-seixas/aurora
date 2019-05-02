using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour {
    public static ObjectPooler SharedInstance;

    [System.Serializable]
    public class ObjectPoolItem {
        public GameObject objectToPool;
        public int amountToPool;
        public bool shouldExpand;

        private int active = 0;

        public int GetActive(){ return this.active; }
        public void Reset() { this.active = 0;}
        public void IncActive(){ this.active++; }
        public void DecActive(){ this.active--; }
    }

    public List<ObjectPoolItem> itemsToPool;

    private Hashtable itemsToPoolState;
    private List<GameObject> pool;
    
    // Initialize a singleton.
    void Awake () {
        SharedInstance = this;
    }

    void Start () {
        itemsToPoolState = new Hashtable();
        pool = new List<GameObject> ();
        foreach (ObjectPoolItem item in itemsToPool) {
            itemsToPoolState.Add(item.objectToPool.tag, item);
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
                ((ObjectPoolItem)itemsToPoolState[tag]).IncActive();
                return pool[i];
            }
        }
        foreach (ObjectPoolItem item in itemsToPool) {
            if (item.objectToPool.tag == tag) {
                if (item.shouldExpand) {
                    GameObject obj = (GameObject) Instantiate (item.objectToPool);
                    obj.SetActive (false);
                    pool.Add (obj);
                    ((ObjectPoolItem)itemsToPoolState[tag]).IncActive();
                    return obj;
                }
            }
        }
        return null;
    }

    public void ClearPool() {
        this.pool.ForEach(obj => obj.SetActive(false));
        foreach (DictionaryEntry pair in itemsToPoolState){
            ((ObjectPoolItem)pair.Value).Reset();
        }
    }
    
    public void FreePooledObject(GameObject obj){
        obj.SetActive(false);
        ((ObjectPoolItem)itemsToPoolState[tag]).DecActive();
    }

    public int GetActiveObjectCount(string tag){
        return ((ObjectPoolItem)itemsToPoolState[tag]).GetActive();
    }

}