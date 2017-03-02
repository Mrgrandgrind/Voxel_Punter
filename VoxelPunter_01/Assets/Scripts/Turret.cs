using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret: MonoBehaviour {

	public Transform player;

	public GameObject Bullet_Emitter;

	public GameObject Bullet;

	public AudioSource[] audioClip;

	public float Bullet_Forward_Force;

	bool InRange = false;
	// Use this for initialization
	void Start () {
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

			PlayAudio (Random.Range(0,2));

			Temporary_Bullet_Handler = Instantiate (Bullet, Bullet_Emitter.transform.position, Bullet_Emitter.transform.rotation) as GameObject;

			Rigidbody Temporary_RigidBody;

			Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody> ();

			Temporary_RigidBody.AddForce (transform.forward * Bullet_Forward_Force);

			Destroy (Temporary_Bullet_Handler, 10.0f);
		}
	}


	IEnumerator OnCollisionEnter(Collision col){
		if (col.gameObject.tag == "Arrow"){
			audioClip [2].Play ();
			yield return new WaitForSeconds(0.6f);
			Destroy (gameObject);
		}
	}

	void PlayAudio(int clip){
		audioClip[clip].Play();
	}
}
