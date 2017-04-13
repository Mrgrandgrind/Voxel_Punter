using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTarget : MonoBehaviour {

	public ParticleSystem DamageSpark;

	private GameObject Door;
	private bool ArrowHit;
	private Vector3 EndPoint;

	void Start(){
		Door = GameObject.Find ("DoorBars");
		EndPoint = this.transform.position - new Vector3 (0.12f, 0.0f, 0.0f);
	}

	void Update(){
		if (ArrowHit) {
			this.GetComponent<Renderer>().material.SetColor ("_Color", Color.green);
			this.transform.position = Vector3.Lerp (this.transform.position, EndPoint, Time.deltaTime / 0.4f);
		}
	}

	void OpenDoor(){
		Door.GetComponent<OpenBars> ().Activated = true;
	}



	void OnCollisionEnter(Collision Col){
		if (Col.gameObject.tag == "Arrow") {
			Instantiate (DamageSpark, this.transform.position, this.transform.rotation);
			Destroy (Col.gameObject);
			ArrowHit = true;
			Invoke ("OpenDoor", 0.5f);
		}
	}
}
