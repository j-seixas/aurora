using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour {

    [SerializeField]
    private int attackDamage=50;

    [SerializeField]
    private int knockbackForce = 15;
   

    public void DealDamage(GameObject minion, int damage){
        if(damage == 0) damage = this.attackDamage;
        MinionController minionController=  minion.GetComponent<MinionController>();
        minionController.ReceiveDamage(damage);
        minionController.SimpleKnockBack(this.knockbackForce);
    }
        

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "MinionMelee" || other.tag == "MinionRanged") {
            this.DealDamage(other.gameObject, this.attackDamage);
        }
    }

    public void ToggleWeaponCollider(bool isActive){

        this.GetComponent<Collider>().enabled = isActive;
        Collider[] colliders = this.GetComponents<Collider>();
        foreach (Collider collider in colliders)
        {
            collider.enabled = isActive;
        }

    }
}
