using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret: MonoBehaviour {

	public GameObject eye;
	public GameObject Bullet_Emitter;
	public float Bullet_Forward_Force;
	public float ShootDelay;

	private Renderer eye_rend;
	private Renderer body_rend;

	private GameObject Bullet;
	private Transform player;
	private int health = 3;
	private bool InRange = false;


	// Use this for initialization
	void Start () {
		body_rend = GetComponent<Renderer> ();
		eye_rend = eye.GetComponent<Renderer> ();
		player = GameObject.Find ("Camera (eye)").transform;
		Bullet = Resources.Load ("projectile") as GameObject;
		InvokeRepeating ("Shoot", 0.0f, ShootDelay);
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance (player.position, this.transform.position) < 8) {
			body_rend.material.SetColor ("_Color", Color.red);
			InRange = true;
			Vector3 direction = player.position - this.transform.position;
			this.transform.rotation = Quaternion.Slerp (this.transform.rotation, Quaternion.LookRotation (direction), 0.007f);
		} 

		else {
			InRange = false;
			body_rend.material.SetColor ("_Color", Color.white);
		}
	}

	public void Shoot(){
		if (InRange == true) {
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
			health--;
			if (health <= 0){
				yield return new WaitForSeconds(0.6f);
				Destroy (gameObject);
			}
		}
	}

	IEnumerator OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Arrow" || col.gameObject.tag == "projectile"){
			health--;
			if (health <= 0){
				yield return new WaitForSeconds(0.6f);
				Destroy (gameObject);
			}
		}
	}

}

