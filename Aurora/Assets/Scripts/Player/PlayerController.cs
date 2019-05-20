using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    
    public Camera mainCam;
    
    private Rigidbody rb;
    private float speed = 10.0f;
    
    [SerializeField]
    private int health = 100;

    private int maxHealth;

    [SerializeField]
    private float healthRegenTime = 1f;

    [SerializeField] private int addHealth = 5;

    void Start() {
        this.rb = GetComponent<Rigidbody>();
        this.maxHealth = this.health;
        InvokeRepeating("healthRegen", 1f, healthRegenTime);
    }

    void Update() {
        if (Input.GetButton("Start") || health <= 0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void FixedUpdate() {
        HandlePlayerMovement();
    }

    public void ReceiveDamage(int damage) {
        this.health -= damage;
        if(this.health < 0)
            this.health = 0;
        GameObject.Find("HUDCanvas").GetComponent<HUDUpdater>().UpdateSlider("HealthUI", -damage);
    }

    void HandlePlayerMovement(){
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        direction = Quaternion.AngleAxis(mainCam.transform.localEulerAngles.y, Vector3.up) * direction; //take into account camera yaw/direction
        
        this.rb.MovePosition(transform.position + direction * Time.deltaTime * this.speed);
        
        if (direction != Vector3.zero)
            this.rb.MoveRotation(Quaternion.LookRotation(direction));
    }

    void healthRegen(){
        if(health < maxHealth) {
            health = health + addHealth > maxHealth ? maxHealth : health + addHealth;
            GameObject.Find("HUDCanvas").GetComponent<HUDUpdater>().UpdateSlider("HealthUI" , addHealth);
        }
    }
}
