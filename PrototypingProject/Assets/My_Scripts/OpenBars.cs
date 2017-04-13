using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBars : MonoBehaviour {

	public bool Activated;
	private Vector3 EndPoint;

	void Start(){
		EndPoint = this.transform.position - new Vector3 (0.0f, 2.2f, 0.0f);
	}

	// Update is called once per frame
	void Update () {
		if (Activated) {
			this.transform.position = Vector3.Lerp (this.transform.position, EndPoint, Time.deltaTime / 0.4f);
		}
	}
}
