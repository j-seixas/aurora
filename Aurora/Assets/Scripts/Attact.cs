using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attact : MonoBehaviour
{
    public int AttackDamage;
    public Animator animator;

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
}
