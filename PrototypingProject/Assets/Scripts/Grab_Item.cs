using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab_Item : MonoBehaviour {

	private Rigidbody rBody;
	private float moveScale;


	// Use this for initialization
	void Start () {
		rBody = GetComponent<Rigidbody>();
	}

	public void Grab(bool ShouldGrab){
		rBody.isKinematic = ShouldGrab;
	}

	public void Move(Vector3 CurrentHandPosition, Vector3 LastHandPosition){
		rBody.position = Vector3.Lerp (rBody.position, CurrentHandPosition, Time.deltaTime / 0.1f);
		//rBody.MovePosition (rBody.position + (CurrentHandPosition - LastHandPosition));
	}
}
