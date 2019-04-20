using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//based on https://www.mvcode.com/lessons/unity-rpg-camera-controller-jamie
//TODO: delete when delivering
public class CameraController : MonoBehaviour {
    public GameObject target;

    private Vector3 offset;

    private float pitch = 0f;   //up/down
    private float minPitch = -40f, maxPitch = 60f;
    private float yaw = 0f;     //left/right
    private float roll = 0f;    //roll around

    private float sensitivity = 2f;

    void Start() {
        this.offset = target.transform.position - transform.position;
    }

    void Update() {
        Vector3 directedOffset = offset.magnitude * transform.forward;
        directedOffset.y = offset.y;
        transform.position = target.transform.position - directedOffset;

        yaw += sensitivity * Input.GetAxis("Mouse X");
        pitch += sensitivity * Input.GetAxis("Mouse Y");
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
        transform.localEulerAngles = new Vector3(pitch, yaw, roll);
    }
}
