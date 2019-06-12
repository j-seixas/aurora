using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour {
    public Material redFlashMaterial;

    private ProjectileSpawner projectile;

    private GameObject player;

    private int level = 1;

    private bool coroutineRunning = false;

    private GameObject cameraPlayer, cameraLevelEnd, cameraWaveEnd;

    void Start() {
        //shockwave = GetComponentsInChildren<ShockwaveSpawner>(true)[0];
        projectile = GetComponentsInChildren<ProjectileSpawner>(true)[0];
        player = GameObject.FindWithTag("Player");

        // Initialize cameras which point to the mountain and the player.
        this.cameraPlayer = GameObject.Find("Main Camera");
        this.cameraLevelEnd = GameObject.Find("LevelEndCamera");
        this.cameraWaveEnd = GameObject.Find("WaveEndCamera");
    }

    private List<Material> SaveCurrentMountainMaterial() {
        List<Material> materials = new List<Material>();

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("MountainBody")) {
            materials.Add(obj.GetComponent<Renderer>().material);
        }

        return materials;
    }

    private void RestoreOldMountainMaterial(List<Material> old) {
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("MountainBody")) {
            obj.GetComponent<Renderer>().material = old[0];
            old.RemoveAt(0);
        }
    }

    // Switches every mountain component's material to a single one.
    public void SwitchMountainMaterial(Material material) {
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("MountainBody")) {
            obj.GetComponent<Renderer>().material = material;
        }
    }

    private IEnumerator FlashRed () {
        List<Material> materials = this.SaveCurrentMountainMaterial();
        this.SwitchMountainMaterial(this.redFlashMaterial);

        float flashAnimationDuration = 0.5f;
        yield return new WaitForSeconds(flashAnimationDuration / 2);

        this.RestoreOldMountainMaterial(materials);
        yield return new WaitForSeconds(flashAnimationDuration / 2);
    }


    public IEnumerator PlayLevelEndCutscene() {
        Animator animator = this.cameraLevelEnd.gameObject.GetComponent<Animator>();

        this.cameraPlayer.GetComponent<Camera>().enabled = false;
        this.cameraLevelEnd.GetComponent<Camera>().enabled = true;
        animator.SetBool("PlayCinematic", true);

        yield return new WaitForSeconds(8.0f);

        this.cameraPlayer.GetComponent<Camera>().enabled = true;
        this.cameraLevelEnd.GetComponent<Camera>().enabled = false;
        animator.SetBool("PlayCinematic", false);
    }

    public IEnumerator PlayWaveEndCutscene() {
        ParticleSystem particles = GameObject.FindGameObjectWithTag("Mountain").GetComponentInChildren<ParticleSystem>();

        this.cameraPlayer.GetComponent<Camera>().enabled = false;
        this.cameraLevelEnd.GetComponent<Camera>().enabled = false;
        this.cameraWaveEnd.GetComponent<Camera>().enabled = true;

        particles.Play();
        this.cameraWaveEnd.GetComponent<Animator>().SetBool("PlayCinematic", true);
        
        yield return new WaitForSeconds(2.0f);
        StartCoroutine(this.FlashRed());

        yield return new WaitForSeconds(5.0f);
        this.cameraWaveEnd.GetComponent<Animator>().SetBool("PlayCinematic", false);
        
        this.cameraWaveEnd.GetComponent<Camera>().enabled = false;
        this.cameraLevelEnd.GetComponent<Camera>().enabled = true;
        this.cameraPlayer.GetComponent<Camera>().enabled = true;
    }


    void Update() {
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
