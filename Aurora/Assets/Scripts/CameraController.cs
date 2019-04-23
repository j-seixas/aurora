using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public GameObject target;
    public float sensitivity = 2f;

    private Vector3 offset;
    private float pitch = 0f;   //up/down
    private float yaw = 0f;     //left/right
    private float minPitch = 0f, maxPitch = 85f;

    void Start() {
        this.offset = target.transform.position - transform.position;
    }

    void Update() {
        yaw += sensitivity * Input.GetAxis("Mouse X");
        pitch += sensitivity * Input.GetAxis("Mouse Y");
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        transform.position = target.transform.position - offset.magnitude * transform.forward;
        transform.localEulerAngles = new Vector3(pitch, yaw, 0f);
    }
}