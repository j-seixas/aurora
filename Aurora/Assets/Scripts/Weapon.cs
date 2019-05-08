using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public int AttackDamage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        // TODO: Likely change this so it's just minion, right?
        if (other.tag == "MinionMelee" || other.tag == "MinionRanged") {
            ObjectPooler.SharedInstance.FreePooledObject(other.gameObject);
            
            GameObject spirit = ObjectPooler.SharedInstance.GetPooledObject("Spirit");
            spirit.GetComponent<SpiritController>().PositionSelf(other.transform);
        }
    }
}
