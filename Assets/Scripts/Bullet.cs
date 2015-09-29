﻿using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	[HideInInspector]
	public Vector2 forceVec;
	float distanceTravelled = 0;
	Vector3 lastPosition;
	float maxDistance = 7.5F;

	int damage;

	int hitCount; // Used to prevent bullet from hitting multiple enemies
	bool wasPaused;

	// Use this for initialization
	void Start () {
		wasPaused = false;
		hitCount = 0;
		lastPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(GameManager.curGameState == GameState.Paused){
			gameObject.rigidbody2D.velocity = Vector3.zero;
			gameObject.rigidbody2D.angularVelocity = 0;
			wasPaused = true;
			return;
		}

		if (wasPaused) {
			gameObject.rigidbody2D.AddForce (forceVec);	
			wasPaused = false;
		}

		distanceTravelled += Vector3.Distance(transform.position, lastPosition);
		lastPosition = transform.position;

		if (distanceTravelled >= maxDistance) {
			Destroy(gameObject);		
		}
	}

	public void setDamage(int val){
		damage = val;
	}

	void OnTriggerEnter2D(Collider2D c){
		if(c.gameObject.tag == "Enemy"){
			hitCount++; // Increment on every call, incase multiple 
			if (hitCount == 1 && c.gameObject.tag == "Enemy") { // Might have to change this to tag - projectile
				c.gameObject.GetComponent<Enemy>().takeDamage(damage);
			}
			Destroy (gameObject);
		}
	}
}