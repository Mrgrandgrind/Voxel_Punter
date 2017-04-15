using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK.Examples;

public class Chase : MonoBehaviour {

	private GameObject player;
	private Rigidbody rb;
	private Vector3 direction;
	private float timeDelay = 0.05f;
	private Renderer rend;
	private Vector3 HitForce;
	private bool dead;
	private bool StartPause;
	private bool DoOnce;

	public ChaseSpawner Spawner;
	public ParticleSystem Spark;
	public bool stuck = false;
	public bool parented;
	public int FaceLocation;
	public Vector3 StickLocation;

	// Use this for initialization
	void Start () {
		HitForce = new Vector3(0, 0, -200);
		rb = GetComponent<Rigidbody>();
		player = GameObject.Find ("Camera (eye)");
		rend = GetComponent<Renderer> ();
		Invoke ("StartDelay", 1.0f);
	}

	void StartDelay(){
		StartPause = true;
	}
	// Update is called once per frame
	void Update () {
		if (!StartPause)
			return;
		
		direction = player.transform.position - this.transform.position;
		if (((Vector3.Distance (player.transform.position, this.transform.position) < 8) && (!stuck)) && (!dead)) {
			rend.material.SetColor ("_Color", Color.blue);
			direction = player.transform.position - this.transform.position;
			this.transform.rotation = Quaternion.Slerp (this.transform.rotation, Quaternion.LookRotation (direction), 0.1f);
			if (Vector3.Distance (player.transform.position, this.transform.position) > 0.35f) {
				rb.AddForce(direction * 2); 
			}
		}else if (stuck == true) {
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (direction), timeDelay);
			if (parented == false) {
				transform.parent = player.transform;
				StartCoroutine (ParentToPlayer (timeDelay));
				transform.position = Vector3.Lerp (transform.position, StickLocation, Time.deltaTime / timeDelay);
			}
		}
		else
			rend.material.SetColor ("_Color", Color.white);
	}

	void OnTriggerEnter (Collider col){
		if (col.tag == "Controller" || col.tag == "Sword") {
			GetComponent<Rigidbody> ().isKinematic = false;
			//GetComponent<Collider> ().isTrigger = false;
			transform.parent = null;
			stuck = false;

			if (col.tag == "Controller")
				GetComponent<Rigidbody> ().AddRelativeForce (0, 0, -550);

			else if (col.tag == "Sword") {
			float SwordForce = col.gameObject.GetComponent<Sword> ().OverlapForce;
			gameObject.GetComponent<Rigidbody> ().AddRelativeForce (HitForce * (SwordForce / 70));
			dead = true;
			Invoke ("Destroy", 1);
			}
		}
	}
		

	IEnumerator ParentToPlayer (float time){
		yield return new WaitForSeconds (time*6);
		parented = true;
	}

	void Destroy(){
		dead = true;
		if (!DoOnce) {
			Spawner.SpawnedNum--;
			DoOnce = true;
		}
		Instantiate (Spark, this.transform.position, this.transform.rotation);
		Destroy (gameObject);
	}


	/*
	IEnumerator OnCollisionEnter(Collision col){
		
		if (col.gameObject.tag == "MainCamera") {
			this.transform.parent =  GameObject.Find ("Camera (eye)").transform;
			rb.isKinematic = true;
		}

		if ((col.gameObject.tag == "Sword") || (col.gameObject.tag == "Arrow")) {

			//stuck = true;
			//this.transform.parent =  GameObject.Find ("Camera (eye)").transform;
			rb.velocity = Vector3.zero;
			rb.AddForce (direction * -5, ForceMode.VelocityChange); 
			health--;
			if (health <= 0) {
				death.Play ();
				yield return new WaitForSeconds (0.55f);
				Destroy (gameObject);
			}
		} 
	}
	*/

	/*
	void OnTriggerEnter(Collider col){

		if (col.gameObject.tag == "MainCamera") {
			this.transform.parent = GameObject.Find ("Camera (eye)").transform;
			rb.isKinematic = true;
			stuck = true;
		}

	}
	*/

}