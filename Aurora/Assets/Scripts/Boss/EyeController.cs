using System.Collections;
using UnityEngine;

public class EyeController : MonoBehaviour {

    public Transform target;

    void Update () {
        transform.LookAt (target);
    }

}