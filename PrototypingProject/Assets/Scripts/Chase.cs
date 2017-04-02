using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : MonoBehaviour {

	public GameObject player;
	public Rigidbody rb;
	public Vector3 direction;
	public int FaceLocation;
	public Vector3 StickLocation;
	public bool stuck = false;
	//private int health = 3;
	private float timeDelay = 0.1f;
	public bool parented;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		player = GameObject.Find ("Camera (eye)");
	}

	// Update is called once per frame
	void Update () {
		direction = player.transform.position - this.transform.position;

		if ((Vector3.Distance (player.transform.position, this.transform.position) < 10) && (stuck == false)) {
			direction = player.transform.position - this.transform.position;
			this.transform.rotation = Quaternion.Slerp (this.transform.rotation, Quaternion.LookRotation (direction), 0.1f);
			if (Vector3.Distance (player.transform.position, this.transform.position) > 0.35f) {
				rb.AddForce(direction * 1); 
			}
		}else if (stuck == true) {
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (direction), timeDelay);
			if (parented == false) {
				StartCoroutine (ParentToPlayer (timeDelay));
				transform.position = Vector3.Lerp (transform.position, StickLocation, Time.deltaTime / timeDelay);
			}
		}
	}

	IEnumerator ParentToPlayer (float time){
		yield return new WaitForSeconds (time*2);
		transform.parent = player.transform;
		parented = true;
	}

	void OnTriggerEnter (Collider col){
		if (col.tag == "Controller" || col.tag == "Sword") {
			GetComponent<Rigidbody> ().isKinematic = false;
			GetComponent<Collider> ().isTrigger = false;
			transform.parent = null;
			GetComponent<Rigidbody> ().AddForce (0, 0, 200);
			stuck = false;
		}
			//Destroy (gameObject);
		//}
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