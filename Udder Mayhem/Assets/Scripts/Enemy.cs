using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

    NavMeshAgent agent;

    public float health = 100;

    public GameObject target;

    private float damage = 25;
    private float damageTime;
    private float damageRate = 0.5f;

    //Effects
    public GameObject deathEffect;

	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();

        // Player reference exception catch
        try {
            target = GameObject.FindGameObjectWithTag("Player");
        }
        catch {
            target = null;
        }
    }
	
	// Update is called once per frame
	void Update () {
        Move();
	}

    private void Move() {
        if (target) {
            agent.destination = target.transform.position;
        }
    }

    //Public method for taking damage and dying
    public void takeDamage(float dmg) {
        health -= dmg;

        if (health <= 0) {
            Destroy(this.gameObject);
            GameManager.instance.score += 1;
            Instantiate(deathEffect, transform.position, transform.rotation);
        }
    }

    private void OnTriggerStay(Collider otherObject) {

        if (otherObject.transform.tag == "Player" && Time.time > damageTime) {
            otherObject.GetComponent<Player>().takeDamage(damage);
            damageTime = Time.time + damageRate;
        }
    }
}
