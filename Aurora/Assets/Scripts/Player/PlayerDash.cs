using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour {
    public Camera mainCam;
    public GameObject PlayerBody;

    [Header("Stamina")]
    [SerializeField] private int maxStamina = 100;
    [SerializeField] private int stamina = 0;
    [SerializeField] private int staminaRegen = 5;
    [SerializeField] private float regenTime = 0.25f;

    [Header("Dash Variables")]
    [SerializeField] private float dashForce = 50;
    [SerializeField] private float dashDuration = 0.1f;
    [SerializeField] private float dashCooldown = 2f;
    [SerializeField] private float dashGracePeriod = 0.5f;
    [SerializeField] private int dashCost = 50;
    private bool isInDashGracePeriod = false;

    private Rigidbody rb;
    private bool isDashing = false;
    private float dashTime;
    private float dashCooldownTime;
    private BoxCollider bodyCollider;
    private HUDUpdater hud;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody>();
        dashTime = 0;
        dashCooldownTime = 0;
        bodyCollider = PlayerBody.GetComponent<BoxCollider>();
        hud = GameObject.Find("HUDCanvas").GetComponent<HUDUpdater>();
        hud.UpdateSlider("StaminaUI", -50 + stamina); // Change -50 to 0 (Fix HUD)
        InvokeRepeating("RegenStamina", 1f, regenTime);
    }

    // Update is called once per frame
    void Update() {
        if(!isDashing) {
            if (dashCooldownTime > 0) {

                dashCooldownTime -= Time.deltaTime;
            }
            else if(Input.GetButton("Dash") && stamina >= dashCost) {
                Debug.Log("FODASSE");
                stamina -= dashCost;
                hud.UpdateSlider("StaminaUI", -dashCost);
                isDashing = true;
                DeactivateBodyCollider();
                dashTime = dashDuration;
                dashCooldownTime = dashCooldown;
            }
        }
        
        if (isDashing && dashTime <= 0) {
            rb.velocity = Vector3.zero;
            isDashing = false;
            ActivateBodyCollider();
        }

        if (isDashing) {
            if (!IsInvoking("CountDashGracePeriod")) {
                this.isInDashGracePeriod = true;
                Invoke("CountDashGracePeriod", this.dashGracePeriod);
            }

            dashTime -= Time.deltaTime;
            rb.velocity = rb.transform.forward * dashForce;
        }
    }

    private void CountDashGracePeriod() =>
        this.isInDashGracePeriod = false;

    private void DeactivateBodyCollider() {
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionY;
        bodyCollider.enabled = false;
    }

    private void ActivateBodyCollider() {
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        bodyCollider.enabled = true;
    }

    private void RegenStamina() {
        if(stamina < maxStamina) {
            stamina = stamina + staminaRegen > 100 ? 100 : stamina + staminaRegen;
            hud.UpdateSlider("StaminaUI" , staminaRegen);
        }
    }

    public bool IsInDashGracePeriod() => this.isInDashGracePeriod;
}
