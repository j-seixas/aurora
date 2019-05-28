using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour {
    public Camera mainCam;
    public GameObject PlayerBody;

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
    private AudioSource audioSource;

    private int defaultLayer;
    private int dashingLayer;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody>();
        dashTime = 0;
        dashCooldownTime = 0;
        this.audioSource = gameObject.GetComponent<AudioSource>();
        defaultLayer = LayerMask.NameToLayer("Default");
        dashingLayer = LayerMask.NameToLayer("Dashing");
    }

    public void Perform() {
        PlayerController player = GetComponent<PlayerController>();
        int currStamina = player.GetAttribute(GameManager.Attributes.Stamina);

        if (this.isDashing) {
            Debug.Log("Already dashing!");
            return;
        }

        if (currStamina - this.dashCost < 0) {
            Debug.Log("Not enough stamina!");
            return;
        }

        if (this.dashCooldownTime > 0) {
            Debug.Log("Dash still in cooldown!");
            return;
        }

        PlaySoundEffect();  //sound effect
        
        // Drain stamina before action is performed.
        GetComponent<PlayerController>().UpdateAttribute(GameManager.Attributes.Stamina, -this.dashCost);
        this.isDashing = true;
        
        // TODO: Change all this shit.
        SwitchLayerToDashing();
        dashTime = dashDuration;
 
    }

    // Update is called once per frame
    void Update() {
        if(dashCooldownTime > 0) {
            dashCooldownTime -= Time.deltaTime;
        }
        
        if (isDashing && dashTime <= 0) {
            rb.velocity = Vector3.zero;
            isDashing = false;
            SwitchLayerToDefault();
            dashCooldownTime = dashCooldown;
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

    private void SwitchLayerToDashing() {
        PlayerBody.transform.parent.gameObject.layer = dashingLayer;
    }

    private void SwitchLayerToDefault() {
        PlayerBody.transform.parent.gameObject.layer = defaultLayer;
    }

    public bool IsInDashGracePeriod() => this.isInDashGracePeriod;

    public void PlaySoundEffect(){
        audioSource.Play();
    }
}
