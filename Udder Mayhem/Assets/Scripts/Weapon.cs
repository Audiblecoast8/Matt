using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    Animation animation;

    // Damage variables
    public float damage = 10.0f;
    public float fireRate = 0.15f;
    public float weaponRange = 50.0f;
    private float fireTime;

    // Spawn effects and target objects;
    public GameObject muzzleFlash;
    public GameObject bulletHit;
    public GameObject muzzle;
    public GameObject target;

    // Audio effects
    public GameObject fireSound;


	// Use this for initialization
	void Start () {
        animation = GetComponent<Animation>();
	}
	
	// Update is called once per frame
	void Update () {
        FireWeapon();
	}

    private void FireWeapon() {
        if(Input.GetMouseButton(0) && Time.time > fireTime) {

            // Fire Effects
            Instantiate(fireSound, muzzle.transform.position, muzzle.transform.rotation);
            Instantiate(muzzleFlash, muzzle.transform.position, muzzle.transform.rotation);
            animation.Play("fire");

            // Raycast Projector
            RaycastHit hit;
            if(Physics.Raycast(muzzle.transform.position, -(muzzle.transform.position - target.transform.position).normalized, out hit, weaponRange)) {
                // Damage Enemy
                if(hit.transform.tag == "Enemy") {
                    hit.transform.GetComponent<Enemy>().takeDamage(damage);

                    // Attach bullet hit effect
                    Instantiate(bulletHit, hit.transform.position, hit.transform.rotation);

                }
            }

            // Setup next time to fire
            fireTime = Time.time + fireRate;
        }
    }
}
