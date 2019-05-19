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
    private List<Upgrade> upgrades = new List<Upgrade>();
    [SerializeField] private int active = -1;

    void Start() {
        this.rb = GetComponent<Rigidbody>();
        
        foreach (GameObject upgrade in GameObject.FindGameObjectsWithTag("Upgrade")) {
            upgrades.Add(upgrade.GetComponent<Upgrade>());
        }
    }

    private void SwitchLevel(int index) {
        upgrades[index].SetActive(true);
        upgrades[this.active].SetActive(false);
        this.active = index;
    }

    void Update() {
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

    public void UnlockUpgrade(string name) {
        for (int i = 0; i < this.upgrades.Count; i++) {
            if (this.upgrades[i].name + "(Clone)" == name) { 
                this.upgrades[i].LevelUpUpgrade();
                this.SwitchLevel(i);
                break;
            }
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
