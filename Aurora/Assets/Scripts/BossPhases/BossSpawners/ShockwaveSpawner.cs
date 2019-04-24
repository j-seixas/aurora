using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockwaveSpawner : MonoBehaviour {
    public float spawnRate = 5.0f, despawnRange = 60.0f, travelSpeed = 0.2f;
    public GameObject shockwave;
    private SphereCollider collider;
    private List<GameObject> shockwaves = new List<GameObject>();
    void Start() {
        InvokeRepeating("Spawner", 0.0f, spawnRate);
    }

    void Spawner() {
        shockwaves.Add(Instantiate(shockwave, gameObject.transform.position, Quaternion.identity));
    }

    void Update() {
        for (int i = this.shockwaves.Count - 1; i >= 0; i--) {

            // If the shockwave travels for too long, despawn it.
            if (this.shockwaves[i].transform.localScale.x >= this.despawnRange * 2) {
                Destroy(this.shockwaves[i]);
                this.shockwaves.Remove(this.shockwaves[i]);
            }

            // Else, increase the wave's travel speed by a fixed amount.
            this.shockwaves[i].transform.localScale += new Vector3(travelSpeed, travelSpeed, travelSpeed);
        }
    }
}
