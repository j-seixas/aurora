using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
   
    public Animator animator;
    // Start is called before the first frame update

    private void Update() {
        if(GetComponent<Collider>().enabled == false){
           animator.SetBool("IsShopping",false);
        }
    }

    private void OnTriggerEnter(Collider other) {
       
        if(other.tag == "PlayerBody"){
           animator.SetBool("IsShopping",true);
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.tag == "PlayerBody"){
          animator.SetBool("IsShopping",false);
        }
    }

    
}
