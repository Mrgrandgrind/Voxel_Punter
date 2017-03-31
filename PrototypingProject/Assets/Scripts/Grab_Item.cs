using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab_Item : MonoBehaviour {

	//public bool GripOverride;

	private Rigidbody rBody;
	private float moveScale;



	// Use this for initialization
	void Start () {
		rBody = GetComponent<Rigidbody>();
	}

	public void Grab(bool ShouldGrab){
		rBody.isKinematic = ShouldGrab;
	}

	public void Move(Vector3 CurrentHandPosition){
		rBody.position = Vector3.MoveTowards (rBody.position, CurrentHandPosition, Time.deltaTime / 0.2f);
	}
}
