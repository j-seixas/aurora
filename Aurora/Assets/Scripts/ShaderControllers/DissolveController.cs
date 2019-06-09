using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveController : MonoBehaviour {
    public float animationSpeed = 0.01f;
    public Camera cinematicCamera, playerCamera;

    private Material material;
    private bool canDissolve = false, isInCinemaMode = false;
    private AudioSource audioSource;

    void Start() {
        this.material = gameObject.GetComponent<Renderer>().material;
        this.audioSource = gameObject.GetComponent<AudioSource>();
    }

    void Update() {
        if (this.canDissolve) {
            
            if (!this.isInCinemaMode) {
                StartCoroutine(SwitchToCinematicCamera());
                this.isInCinemaMode = true;
            }

            this.material.SetFloat("_AnimationFrame", this.material.GetFloat("_AnimationFrame") + this.animationSpeed);
            this.gameObject.GetComponent<Collider>().enabled = false;
        }
        if (this.material.GetFloat("_AnimationFrame") >= 1) {
            this.canDissolve = false;
            this.gameObject.SetActive(false);
        }
        
    }

    private IEnumerator SwitchToCinematicCamera() {
        Animator animator = this.cinematicCamera.gameObject.GetComponent<Animator>();

        this.playerCamera.enabled = false;
        this.cinematicCamera.enabled = true;
        animator.SetBool("PlayCinematic", true);

        yield return new WaitForSeconds(8.0f);

        this.playerCamera.enabled = true;
        this.cinematicCamera.enabled = false;
        animator.SetBool("PlayCinematic", false);
    }

    public void StartDissolving() {
        this.canDissolve = true;
    }

    public void PlaySoundEffect(){
        audioSource.Play();
    }
}
