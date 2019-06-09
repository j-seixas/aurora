using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour {
    public float spawnRate = 5.0f, firingAngle = 65.0f, gravity = 40.0f;
    public GameObject projectile;
    private GameObject player;

    private WaveFactory waveFactory;

    private bool isOnAir = false;
    void Start() {
        this.player = GameObject.FindWithTag("Player");
        waveFactory = GameObject.FindGameObjectWithTag("WaveFactory").GetComponent<WaveFactory>();
        InvokeRepeating("Spawner", 0.0f, spawnRate);
    }

    void Update() {

    }

    void Spawner() {
        if((waveFactory != null && waveFactory.IsShoppingPhase()) || this.isOnAir){
            return;
        }
        StartCoroutine("LaunchProjectile");
    }

    private void OnDisable() {
        CancelInvoke();
    }

    IEnumerator LaunchProjectile() {

        

        this.isOnAir = true;
        // Instantiate projectile and bullseye objects.
        Vector3 target = player.transform.position;
        GameObject proj = Instantiate(projectile, gameObject.transform.position, Quaternion.identity);
        

        
        // Calculate distance to target.
        float distance = Vector3.Distance(proj.transform.position, target);

        // Calculate velocity needed to throw object to target at specified angle.
        float velocity = distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

        // Extract the X and Y component of the velocity.
        float Vx = Mathf.Sqrt(velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad), Vy = Mathf.Sqrt(velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

        // Calculate flight time.
        float duration = distance / Vx, elapsedTime = 0.0f;

        // Rotate projectile to face the target.
        proj.transform.rotation = Quaternion.LookRotation(target - proj.transform.position);

        while (elapsedTime < duration) {
            proj.transform.Translate(0, (Vy - (gravity * elapsedTime)) * Time.deltaTime, Vx * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        this.isOnAir = false;
        Destroy(proj, 1.0f);
    }
}
