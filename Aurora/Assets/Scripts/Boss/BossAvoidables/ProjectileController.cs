using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {

    public int damage = 10;
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "PlayerBody"){
            GameObject player = other.gameObject.transform.parent.gameObject;
            player.GetComponent<PlayerController>().UpdateAttribute(GameManager.Attributes.Health, -this.damage);
        }else if (other.tag == "MinionMelee" || other.tag == "MinionRanged") {
           other.GetComponent<MinionController>().ReceiveDamage(this.damage);
        }
    }
}
