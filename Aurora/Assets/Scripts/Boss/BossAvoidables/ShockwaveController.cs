using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockwaveController : MonoBehaviour {
    public float speed = 0.1f, maxRadius = 25.0f;
    public int damage = 10;

    private ParticleSystem particles;
    private List<ParticleSystem.Particle> triggered = new List<ParticleSystem.Particle>();
    private PlayerController playerController;

    private void Start() {
        this.playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        this.particles = GetComponent<ParticleSystem>();
    }

    private void Update() {

    }

    private void OnParticleTrigger() {
        this.particles.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, this.triggered);
        
        foreach (ParticleSystem.Particle particle in this.triggered) {
            this.playerController.UpdateAttribute(GameManager.Attributes.Health, -this.damage);
        }
    }

}
