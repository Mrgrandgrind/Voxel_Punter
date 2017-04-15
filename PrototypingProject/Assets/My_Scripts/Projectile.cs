using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK.Examples;


public class Projectile : MonoBehaviour {

	public Vector3 force;

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Rigidbody> ().AddForce (force);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider col){

		if (col.gameObject.tag == "Sword") {
			float SwordForce;
			SwordForce = col.gameObject.GetComponent<Sword> ().OverlapForce;
			gameObject.GetComponent<Rigidbody> ().AddForce (-force * (SwordForce/140));
		}
	}


}
