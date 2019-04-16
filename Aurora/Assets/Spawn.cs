using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    // public PlayerHealth playerHealth
    public GameObject objectToSpawn;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObject", spawnTime, spawnTime);
    }

    // Spawns the desired object
    void SpawnObject()
    {
        // TODO: Remove the comments when implemented player health to stop spawning when player dies
        // if(playerHealth.health > 0) {
            int spawnPoint = Random.Range(0, spawnPoints.Length);
            Instantiate(objectToSpawn, spawnPoints[spawnPoint].position, spawnPoints[spawnPoint].rotation);
        // }
    }
}
