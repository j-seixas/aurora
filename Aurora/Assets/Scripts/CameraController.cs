using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public GameObject target;

    private Vector3 offset;                         //camera-target distance to be kept
    private float pitch = 0f;                       //rotation around X axis
    private float yaw = 0f;                         //rotation around Y axis
    private float minPitch = 0f, maxPitch = 85f;
    private float sensitivity = 2f;                 //input sensitivity multiplier
    private float cameraRotationSpeed = 4f;         //how fast camera rotates, the slower the smoother

    void Start() {
        this.offset = target.transform.position - transform.position;
    }

    void Update() {
        yaw += sensitivity * Input.GetAxis("Mouse X");
        pitch += sensitivity * Input.GetAxis("Mouse Y");
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        transform.position = target.transform.position - offset.magnitude * transform.forward;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(pitch, yaw, 0f)), cameraRotationSpeed * Time.deltaTime);
    }
}