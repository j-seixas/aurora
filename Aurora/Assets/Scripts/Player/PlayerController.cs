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

    void Start() {
        this.rb = GetComponent<Rigidbody>();
    }

    void Update() {
        if (Input.GetButton("Start") || health <= 0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
        if (Input.GetButtonDown("Ability")) {
            Debug.Log(gameObject.GetComponentInChildren<FreezeUpgrade>());
            gameObject.GetComponentInChildren<FreezeUpgrade>().Run();
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
