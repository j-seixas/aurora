using UnityEngine;

public class EyeController : MonoBehaviour {
    public Transform target;

    public Vector3 rotationOffset;

    void Update () {
        transform.LookAt (target);
        transform.Rotate(rotationOffset);
    }
}