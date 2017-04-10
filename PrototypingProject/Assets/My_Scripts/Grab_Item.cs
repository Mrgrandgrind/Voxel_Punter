using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab_Item : MonoBehaviour {

	public bool inHand;

	private Rigidbody rBody;
	private float moveScale;



	// Use this for initialization
	void Start () {
		rBody = GetComponent<Rigidbody>();
	}

	public void Grab(bool ShouldGrab){
		rBody.isKinematic = ShouldGrab;
	}

	public void Move(bool moving, Vector3 CurrentHandPosition){
		if (moving == true) {
			rBody.position = Vector3.MoveTowards (rBody.position, CurrentHandPosition, Time.deltaTime / 0.1f);
			if (rBody.position == CurrentHandPosition)
				inHand = true;
		} else {
			inHand = false;
		}
	}
}
