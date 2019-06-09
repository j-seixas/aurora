using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{

    private ShockwaveSpawner shockwave;

    private ProjectileSpawner projectile;

    private GameObject player;

    private int level = 1;

    private bool coroutineRunning = false;
    // Start is called before the first frame update
    void Start()
    {
        shockwave = GetComponentsInChildren<ShockwaveSpawner>(true)[0];
        projectile = GetComponentsInChildren<ProjectileSpawner>(true)[0];
        player = GameObject.FindWithTag("Player");

    }

    // Update is called once per frame
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
