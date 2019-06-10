using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour {


    private ProjectileSpawner projectile;

    private GameObject player;

    private int level = 1;

    private bool coroutineRunning = false;

    void Start() {
        //shockwave = GetComponentsInChildren<ShockwaveSpawner>(true)[0];
        projectile = GetComponentsInChildren<ProjectileSpawner>(true)[0];
        player = GameObject.FindWithTag("Player");

    }


    void Update()
    {
        if(!projectile.isBeingThrown()){
            if(!coroutineRunning) StartCoroutine("HandleProjectile");
        }
    }

    IEnumerator HandleProjectile(){
        coroutineRunning = true;
        yield return new WaitForSeconds(Random.Range(projectile.spawnRate,projectile.spawnRate+2f));
        projectile.Spawner();
        coroutineRunning = false;
    }
}
