using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DissolveController : MonoBehaviour {
    public float animationSpeed = 0.01f;
    public Camera cinematicCamera, playerCamera;

    public Material dissolveMaterial;

    private bool canDissolve = false, isInCinemaMode = false, hasAppliedMaterial = false;
    private AudioSource audioSource;

    void Start() {
        //this.material = gameObject.GetComponent<Renderer>().material;
        this.audioSource = gameObject.GetComponent<AudioSource>();
    }

    void Update() {
        if (this.canDissolve) {

            if (!this.hasAppliedMaterial) {
                GetComponent<BossController>().SwitchMountainMaterial(this.dissolveMaterial);
                this.hasAppliedMaterial = true;
            }
            
            if (!this.isInCinemaMode) {
                BossController boss = gameObject.GetComponent<BossController>();
                StartCoroutine(boss.PlayLevelEndCutscene());
                this.isInCinemaMode = true;
            }

            this.dissolveMaterial.SetFloat("_AnimationFrame", this.dissolveMaterial.GetFloat("_AnimationFrame") + this.animationSpeed);
        }
        if (this.dissolveMaterial.GetFloat("_AnimationFrame") >= 1) {
            this.canDissolve = false;

            // Reset the animation frame because for some reason Unity saves this.
            this.dissolveMaterial.SetFloat("_AnimationFrame", 0);
            
            this.gameObject.SetActive(false);
        }     
    }

    public void StartDissolving() {
        this.canDissolve = true;
    }

    public void PlaySoundEffect(){
        audioSource.Play();
    }

    public void PlayVictoryMusic(){
        AudioManager.Instance.PlayMusic("Victory");
    }
}
