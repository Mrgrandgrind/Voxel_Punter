using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret: MonoBehaviour {
	

	public GameObject Bullet_Emitter;

	public GameObject Bullet;

	public float Bullet_Forward_Force;

	public Transform player;

	int health = 3;

	bool InRange = false;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Camera (eye)").transform;
		InvokeRepeating ("Shoot", 0.0f, 1.5f);
	}
	
	// Update is called once per frame
	void Update () {

		if (Vector3.Distance (player.position, this.transform.position) < 10) {
	
			InRange = true;
			Vector3 direction = player.position - this.transform.position;
			this.transform.rotation = Quaternion.Slerp (this.transform.rotation, Quaternion.LookRotation (direction), 0.007f);
		} 

		else {
			InRange = false;
		}
	}

	public void Shoot(){
		if (InRange == true) {
			GameObject Temporary_Bullet_Handler;

			Temporary_Bullet_Handler = Instantiate (Bullet, Bullet_Emitter.transform.position, this.transform.rotation) as GameObject;

			Projectile shotProjectile = Temporary_Bullet_Handler.GetComponent<Projectile> ();

			shotProjectile.force = transform.forward * Bullet_Forward_Force;

			//Rigidbody Temporary_RigidBody;

			//Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody> ();

			//Temporary_RigidBody.AddForce (transform.forward * Bullet_Forward_Force);

			Destroy (Temporary_Bullet_Handler, 10.0f);
		}
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

