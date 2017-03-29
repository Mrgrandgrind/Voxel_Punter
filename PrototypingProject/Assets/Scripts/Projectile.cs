using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using VRTK.Examples;


public class Projectile : MonoBehaviour {

	public Vector3 force;

	private GameObject PlayerSword;

	// Use this for initialization
	void Start () {
		PlayerSword = GameObject.Find ("Sword");
		gameObject.GetComponent<Rigidbody> ().AddForce (force);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider col){

		if (col.gameObject.tag == "Sword") {
			float SwordForce;
			SwordForce = PlayerSword.GetComponent<Sword> ().OverlapForce;
			gameObject.GetComponent<Rigidbody> ().AddForce (-force * (SwordForce/100));
		}

		if (col.gameObject.tag == "Enemy") {
			Destroy (gameObject);
		}
	}


}
