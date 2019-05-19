using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    
    public Camera mainCam;
    
    private Rigidbody rb;
    private float speed = 10.0f;
    
    [SerializeField] private int health = 100;
    [SerializeField] private int stamina = 100;
    
    [Header("Upgrade")]
    public List<Upgrade> upgrades = new List<Upgrade>();
    [SerializeField] private int active = -1;

    void Start() {
        this.rb = GetComponent<Rigidbody>();
    }

    public void AddHealth(int increment) {
        if (this.health + increment > 100) this.health = 100;
        else this.health += increment;
    }

    private void SwitchLevel(int index) {
        // Deactivate previous upgrade only if there's only active.
        if (this.active != -1)
            upgrades[this.active].SetActive(false);

        upgrades[index].SetActive(true);
        this.active = index;
        Debug.Log("Selected " + upgrades[index].tag + " upgrade.");
    }

    void Update() {
        GameObject.Find("HUDCanvas").GetComponent<HUDUpdater>().UpdateSlider("HealthUI", this.health);

        if (Input.GetButton("Start") || health <= 0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        if (Input.GetButtonDown("QuickSwitchLeft")) {
            for (int i = active - 1; i >= 0; i--) {
                if (upgrades[i].GetLevel() > 0) { this.SwitchLevel(i); break; }
            }
        }
        
        if (Input.GetButtonDown("QuickSwitchRight")) {
            for (int i = active + 1; i < upgrades.Count; i++) {
                if (upgrades[i].GetLevel() > 0) { this.SwitchLevel(i); break; }
            }
        }
        
        if (Input.GetButtonDown("Ability")) {
            this.upgrades[this.active].Active();
        }
    }

    public void UnlockUpgrade(string upTag) {
        for (int i = 0; i < this.upgrades.Count; i++) {
            if (this.upgrades[i].tag == upTag) { this.upgrades[i].LevelUp(); break; }
        }
    }

    void FixedUpdate() {
        HandlePlayerMovement();
    }

    public void ReceiveDamage(int damage) {
        this.health -= damage;
        GameObject.Find("HUDCanvas").GetComponent<HUDUpdater>().UpdateSlider("HealthUI", -damage);
    }

    void HandlePlayerMovement(){
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        direction = Quaternion.AngleAxis(mainCam.transform.localEulerAngles.y, Vector3.up) * direction; //take into account camera yaw/direction
        
        this.rb.MovePosition(transform.position + direction * Time.deltaTime * this.speed);
        
        if (direction != Vector3.zero)
            this.rb.MoveRotation(Quaternion.LookRotation(direction));
    }
}
