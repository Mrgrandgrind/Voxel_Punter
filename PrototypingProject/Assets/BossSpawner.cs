using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour {

	public GameObject Boss;

	void OnTriggerEnter(Collider Col){
		if (Col.tag == ("Player")){
			Boss.GetComponent<ChaseSpawner> ().Enabled = true;
			Destroy (this.gameObject);
		}

	}
}
