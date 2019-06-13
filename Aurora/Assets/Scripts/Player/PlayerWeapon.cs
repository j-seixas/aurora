using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour {

    [SerializeField]
    private int attackDamage=50;

    [SerializeField]
    private int knockbackForce = 15;
   

    public void DealDamage(GameObject minion, int damage, bool applyBurn, bool applySlow) {
        if (damage == 0) damage = this.attackDamage;
        MinionController minionController=  minion.GetComponent<MinionController>();
        minionController.ReceiveDamage(damage);
        minionController.SimpleKnockBack(this.knockbackForce);

        if (applyBurn) {
            (int damage, float rate, int ticks) passive = GameObject.FindGameObjectWithTag("FireGem").GetComponent<FireUpgrade>().GetPassiveParameters();
            minionController.ApplyBurn(passive.damage, passive.rate, passive.ticks);
        }

        if (applySlow) {
            (float slow, float duration) passive = GameObject.FindGameObjectWithTag("IceGem").GetComponent<IceUpgrade>().GetPassiveParameters();
            minionController.ApplySlow(passive.slow, passive.duration);
        }
    }
        

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "MinionMelee" || other.tag == "MinionRanged") {
            bool applyBurn = GameObject.FindGameObjectWithTag("FireGem").GetComponent<FireUpgrade>().IsActiveEnabled();
            bool applySlow = GameObject.FindGameObjectWithTag("IceGem").GetComponent<IceUpgrade>().IsActiveEnabled();

            this.DealDamage(other.gameObject, this.attackDamage, applyBurn, applySlow);
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
