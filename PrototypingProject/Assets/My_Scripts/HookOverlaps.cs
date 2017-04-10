using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookOverlaps : MonoBehaviour {

	public GameObject HitGameObject;

	void OnTriggerEnter(Collider col){
		if (col.gameObject.GetComponent<Grab_Item> () == true) {
			HitGameObject = col.gameObject;
		}
	}

	void OnTriggerExit(Collider col){
		HitGameObject = null;
	}
}
