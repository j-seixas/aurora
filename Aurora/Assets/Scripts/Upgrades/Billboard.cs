using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour {

    Transform camTransform;

    Quaternion originalRotation;

    void Start () {
        camTransform = GameObject.Find ("Main Camera").GetComponent<Transform> ();
        originalRotation = transform.rotation;
    }

    void Update () {
        transform.rotation = camTransform.rotation * originalRotation;
    }
}