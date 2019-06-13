using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockwaveController : MonoBehaviour {
    public int damage = 10;

    private ParticleSystem particles;
    private List<ParticleSystem.Particle> triggered = new List<ParticleSystem.Particle>();
    private PlayerController playerController;
    private PlayerDash playerDash;
    private int dashingLayer;

    private void Start() {
        this.playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        this.particles = GetComponent<ParticleSystem>();
        this.dashingLayer = LayerMask.NameToLayer("Dashing");
        this.playerDash = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDash>();
    }

    private void Update() {

    }

    private void OnParticleTrigger() {
        this.particles.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, this.triggered);


        foreach (ParticleSystem.Particle particle in this.triggered) {

            if (this.playerDash.IsInDashGracePeriod() || this.playerDash.IsDashing())
            {
                Debug.Log("DASHING");
                continue;
            }
            this.playerController.UpdateAttribute(GameManager.Attributes.Health, -this.damage);
        }
    }

}
