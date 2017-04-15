using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turret_eye : MonoBehaviour {

	public Turret TurretScript;

	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Arrow") {
			TurretScript.EyeShot ();
			Destroy (col.gameObject);
		}
	}
}
