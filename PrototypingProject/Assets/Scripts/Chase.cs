using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : MonoBehaviour {

	public GameObject player;
	public Rigidbody rb;
	public Vector3 direction;
	public AudioSource death;

	public bool stuck = false;
	//private int health = 3;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		player = GameObject.Find ("Camera (eye)");
	}

	// Update is called once per frame
	void Update () {

		if ((Vector3.Distance (player.transform.position, this.transform.position) < 10) && (stuck == false)) {

			direction = player.transform.position - this.transform.position;
			this.transform.rotation = Quaternion.Slerp (this.transform.rotation, Quaternion.LookRotation (direction), 0.1f);
			if (Vector3.Distance (player.transform.position, this.transform.position) > .35) {
				//this.transform.Translate (0, 0, 0.02f);
				rb.AddForce(direction * 1); 
			} 

			else {
				Debug.LogError ("Attacking!");
			}
		}
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