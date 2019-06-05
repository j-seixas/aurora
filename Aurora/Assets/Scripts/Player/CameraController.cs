using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    private GameObject target;

    private Vector3 offset;                         //camera-target distance to be kept
    private float pitch = 0f;                       //rotation around X axis
    private float yaw = 0f;                         //rotation around Y axis
    private float minPitch = -5f, maxPitch = 85f;
    private float sensitivity = 2f;                 //input sensitivity multiplier
    private float cameraRotationSpeed = 4f;         //how fast camera rotates, the slower the smoother

    void Start() {
        target = GameObject.FindWithTag("Player");
        offset = target.transform.position - transform.position;
    }

    void Update() {
        yaw += sensitivity * Input.GetAxis("Mouse X");
        pitch += sensitivity * Input.GetAxis("Mouse Y");
    
        // if(Input.GetButtonDown("Recenter Camera")) Recenter();
        
        UpdateCameraTransform();
    }

    void UpdateCameraTransform(){
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch); 
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(new Vector3(pitch, yaw, 0f)), cameraRotationSpeed * Time.deltaTime);
        transform.position = target.transform.position - offset.magnitude * transform.forward - Vector3.up * offset.y;
        //TODO: Slerp vs Lerp, try it at will, probably add to settings?
    }

    void Recenter(){
        pitch = target.transform.localEulerAngles.x;
        yaw = target.transform.localEulerAngles.y;
        //TODO: Try recenter automatically (UNCOMMENT LINE BELOW), probably add to settings?
        //transform.rotation = Quaternion.Euler(new Vector3(pitch, yaw, 0f));
    }
}