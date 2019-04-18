using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attact : MonoBehaviour {
    public Animator animator;
    public GameObject weapon;

    // Start is called before the first frame update
    void Start() {
        this.animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetButtonDown("Fire3")) {
            print("Fire");
            this.animator.Play("Attack");
        }
    }

    void ActivateCollider() {
        this.weapon.GetComponent<Collider>().enabled = true;
    }

    void DeactivateCollider() {
        this.weapon.GetComponent<Collider>().enabled = false;
    }

}
