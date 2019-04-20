using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawnerController : ObstacleController {
    public float spawnRate = 5.0f, firingAngle = 65.0f, gravity = 40.0f;
    public GameObject projectile, bullseye;
    private GameObject player;
    void Start() {
        this.player = GameObject.FindWithTag("Player");
        InvokeRepeating("ProjectileSpawner", 0.0f, spawnRate);
    }

    void Update() {   
    }

    void ProjectileSpawner() {
        StartCoroutine("LaunchProjectile");
    }

    IEnumerator LaunchProjectile() {
        yield return new WaitForSeconds(0f);

        // Instantiate projectile and bullseye objects.
        Vector3 target = player.transform.position;
        GameObject proj = Instantiate(projectile, gameObject.transform.position, Quaternion.identity);
        GameObject bull = Instantiate(bullseye, target - new Vector3(0.0f, 0.99f, 0.0f), Quaternion.identity);
        
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

        Destroy(bull, 0.0f);
        Destroy(proj, 1.0f);
    }
}
