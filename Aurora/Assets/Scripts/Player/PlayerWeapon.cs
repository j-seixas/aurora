using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour {

    [SerializeField]
    private int attackDamage;

    private void Start() {
        this.attackDamage = 50;
    }

    private void DealDamage(GameObject minion, int damage) =>
        minion.GetComponent<MinionController>().ReceiveDamage(damage);

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "MinionMelee" || other.tag == "MinionRanged") {
            this.DealDamage(other.gameObject, this.attackDamage);
        }
    }

    public void ToggleWeaponCollider(bool isActive) =>
        this.GetComponent<Collider>().enabled = isActive;
}
