using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour {
    public int AttackDamage;

    void Start() {
    }

    void Update() {    
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("Weapon collision");
    }

    void ToggleWeaponCollider(bool isActive) {
        this.GetComponent<Collider>().enabled = isActive;
    }
}
