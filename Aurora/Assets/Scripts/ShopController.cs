using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    public Material blackSkybox;
    public Cubemap blackCubemap;


    private Material normalSkybox;

    private Cubemap normalCubemap;
    // Start is called before the first frame update
    void Start()
    {
        normalSkybox = RenderSettings.skybox;
        normalCubemap = RenderSettings.customReflection;
    }

    private void Update() {
        if(GetComponent<Collider>().enabled == false){
            RenderSettings.skybox = normalSkybox;
            RenderSettings.customReflection = normalCubemap;
            DynamicGI.UpdateEnvironment();
        }
    }

    private void OnTriggerEnter(Collider other) {
       
        if(other.tag == "PlayerBody"){
            RenderSettings.skybox = blackSkybox;
            RenderSettings.customReflection = blackCubemap;
            DynamicGI.UpdateEnvironment();
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.tag == "PlayerBody"){
            RenderSettings.skybox = normalSkybox;
            RenderSettings.customReflection = normalCubemap;
            DynamicGI.UpdateEnvironment();
        }
    }

    
}
