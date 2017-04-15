using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEye : MonoBehaviour {

	public ChaseSpawner Spawner;

	void OnCollisionEnter(Collision col){
		if ((col.gameObject.tag == "Arrow")) {
			Debug.Log ("arrowHIT");
			Spawner.EyeHit ();
			Destroy (col.gameObject);
		}
	}
}
