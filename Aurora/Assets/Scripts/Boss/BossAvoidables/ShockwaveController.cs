﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockwaveController : MonoBehaviour {
    public float speed = 0.1f, maxRadius = 25.0f;
    public int damage = 10;
    private Vector3 spawner;
    private SphereCollider sphCollider;
    private Material material;

    private void Start() {
        this.sphCollider = gameObject.GetComponent<SphereCollider>();
        this.material = gameObject.GetComponent<Renderer>().material;

        this.spawner = GameObject.Find("ShockwaveSpawner").transform.parent.position;
        this.material.SetVector("_RippleLocation", new Vector4(spawner.x, spawner.y, spawner.z, 0));
    }

    private void Update() {
        this.material.SetFloat("_RippleRadius", this.material.GetFloat("_RippleRadius") + this.speed);
        this.sphCollider.radius = this.material.GetFloat("_RippleRadius") / transform.lossyScale.x;

        if (this.sphCollider.radius >= this.maxRadius) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "PlayerBody" && !other.gameObject.transform.parent.gameObject.GetComponent<PlayerDash>().IsInDashGracePeriod()) {
            GameObject player = other.gameObject.transform.parent.gameObject;
            player.GetComponent<PlayerController>().UpdateAttribute(GameManager.Attributes.Health, -this.damage);
        }
    }
}
