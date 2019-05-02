using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveController : MonoBehaviour {
    public float animationSpeed = 0.01f;
    private Material material;
    private bool canDissolve = false;

    void Start() {
        this.material = gameObject.GetComponent<Renderer>().material;
    }

    void Update() {
        if (this.canDissolve) {
            this.material.SetFloat("_AnimationFrame", this.material.GetFloat("_AnimationFrame") + this.animationSpeed);
        }
        if (this.material.GetFloat("_AnimationFrame") >= 1) {
            this.canDissolve = false;
            this.gameObject.SetActive(false);
        }
        
    }

    public void StartDissolving() {
        this.canDissolve = true;
    }
}
