using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    public Camera mainCam;
    public GameObject PlayerBody;

    [SerializeField] private float dashForce = 50;
    [SerializeField] private float dashDuration = 0.1f;
    [SerializeField] private float dashCooldown = 2f;

    private Rigidbody rb;
    private bool isDashing = false;
    private float dashTime;
    private float dashCooldownTime;
    private BoxCollider bodyCollider;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        dashTime = 0;
        dashCooldownTime = 0;
        bodyCollider = PlayerBody.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetButton("Dash"))
        {
            StartCoroutine(Dash());
        }
        */
        if(!isDashing)
        {
            if (dashCooldownTime > 0)
            {

                dashCooldownTime -= Time.deltaTime;
            }
            else if(Input.GetButton("Dash"))
            {
                isDashing = true;
                DeactivateBodyCollider();
                dashTime = dashDuration;
                dashCooldownTime = dashCooldown;
            }
        }
        
        if(isDashing && dashTime <= 0)
        {
            rb.velocity = Vector3.zero;
            isDashing = false;
            ActivateBodyCollider();
        }

        if(isDashing)
        {
 
            dashTime -= Time.deltaTime;
            rb.velocity = rb.transform.forward * dashForce;
        }
    }

    private void DeactivateBodyCollider()
    {
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionY;
        bodyCollider.enabled = false;
    }

    private void ActivateBodyCollider()
    {
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        bodyCollider.enabled = true;
    }

    public IEnumerator Dash()
    {
        rb.AddForce(rb.transform.forward * dashForce, ForceMode.VelocityChange);

        yield return new WaitForSeconds(dashDuration);

        rb.velocity = Vector3.zero;
    }
}
