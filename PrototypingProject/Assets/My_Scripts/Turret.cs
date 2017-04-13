﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret: MonoBehaviour {

	public ParticleSystem SpawnCloud;
	public ParticleSystem DamageSpark;
	public float Bullet_Forward_Force;

	private bool InRange;
	private bool Spawned;
	private int health = 4;
	private float ShootDelay;
	private GameObject Bullet;
	private GameObject Bullet_Emitter;
	private Renderer eye_rend;
	private Renderer body_rend;
	private Transform player;


	// Use this for initialization
	void Start () {
		body_rend = GetComponent<Renderer> ();
		eye_rend = this.gameObject.transform.GetChild(0).gameObject.GetComponent<Renderer> ();
		Bullet_Emitter = this.gameObject.transform.GetChild(1).gameObject;
		Instantiate (SpawnCloud, Bullet_Emitter.transform.position, this.transform.rotation);
		player = GameObject.Find ("Camera (eye)").transform;
		Bullet = Resources.Load ("projectile") as GameObject;
		Invoke ("SpawnDelay", 2);
		ShootDelay = Random.Range (1.0f, 3.0f);
		InvokeRepeating ("Shoot", 0.0f, ShootDelay);
	}

	void SpawnDelay(){
		Spawned = true;
	}

	// Update is called once per frame
	void Update () {
		if (Spawned) {
			if (Vector3.Distance (player.position, this.transform.position) < 8) {
				body_rend.material.SetColor ("_Color", Color.red);
				InRange = true;
				Vector3 direction = player.position - this.transform.position;
				this.transform.rotation = Quaternion.Slerp (this.transform.rotation, Quaternion.LookRotation (direction), 0.015f);
			} else {
				InRange = false;
				body_rend.material.SetColor ("_Color", Color.white);
			}
		}
	}

	public void Shoot(){
		if (InRange && Spawned) {
			StartCoroutine (ChangeMat ());
			return;
		}
	}

	public IEnumerator ChangeMat(){
		eye_rend.material.SetColor ("_Color", Color.red);
		yield return new WaitForSeconds (ShootDelay/2);
		GameObject Temporary_Bullet_Handler;
		Temporary_Bullet_Handler = Instantiate (Bullet, Bullet_Emitter.transform.position, this.transform.rotation) as GameObject;
		Projectile shotProjectile = Temporary_Bullet_Handler.GetComponent<Projectile> ();
		shotProjectile.force = transform.forward * Bullet_Forward_Force;
		Destroy (Temporary_Bullet_Handler, 10.0f);
		eye_rend.material.SetColor ("_Color", Color.green);
	}


	IEnumerator OnCollisionEnter(Collision col){
		if (col.gameObject.tag == "Arrow" || col.gameObject.tag == "projectile"){
			Instantiate (DamageSpark, Bullet_Emitter.transform.position, this.transform.rotation);
			Destroy (col.gameObject);
			health--;
			if (health <= 0){
				yield return new WaitForSeconds(0.6f);
				GameObject Spawner = GameObject.Find("SwordTrigger");
				Spawner.GetComponent<EnemySpawner> ().EnemyDied ();
				DestroyImmediate (gameObject,true);
			}
		}
	}


	IEnumerator OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Arrow" || col.gameObject.tag == "projectile"){
			Instantiate (DamageSpark, Bullet_Emitter.transform.position, this.transform.rotation);
			Destroy (col.gameObject);
			health--;
			if (health <= 0){
				yield return new WaitForSeconds(0.6f);
				GameObject Spawner = GameObject.Find("SwordTrigger");
				Spawner.GetComponent<EnemySpawner>().EnemyDied ();
				DestroyImmediate (gameObject,true);
			}
		}
	}
}

