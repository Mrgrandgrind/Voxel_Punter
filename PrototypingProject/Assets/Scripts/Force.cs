using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Force : MonoBehaviour {

	private SteamVR_Controller.Device controller;
	private LineRenderer lRender;
	private Vector3[] positions;
	private GameObject grabbableObject;
	private Grab_Item grabbable;
	private bool grabbed;
	private bool Locked;
	private float lerpTime;

	private Vector3 LastHandPos;
	private Vector3 ObjectPos;

	// Use this for initialization
	void Start () {
		SteamVR_TrackedObject trackedObj = GetComponent<SteamVR_TrackedObject> ();
		controller = SteamVR_Controller.Input ((int)trackedObj.index);
		lRender = GetComponent<LineRenderer> ();
		positions = new Vector3[2];
		lerpTime = 0.05f;
		Locked = false;
	}

	// Update is called once per frame
	void Update () {

		if (!grabbed) {
			if (!grabbable)
				return;
		}
	
		if (controller.GetPressDown (SteamVR_Controller.ButtonMask.Trigger)) { // Force Grab
			grabbed = true;
			ObjectPos = transform.position;
		}

		else if (controller.GetPress (SteamVR_Controller.ButtonMask.Trigger)) { // Force move

			if (ObjectPos == grabbableObject.transform.position) {
				Locked = true;
				grabbable.Grab(true);
			}

			if (Locked == true) {
				grabbable.Move (transform.position);
			}

			ObjectPos = Vector3.MoveTowards (ObjectPos, grabbableObject.transform.position, Time.deltaTime / lerpTime);
			DisplayLine (true, ObjectPos);
		}

		else if (controller.GetPressUp (SteamVR_Controller.ButtonMask.Trigger)) { // Release
			DisplayLine (false, transform.position);
			grabbed = false;
			Locked = false;
			grabbable.Grab(false);
		}
	}

	void DisplayLine(bool display, Vector3 endpoint){
		lRender.enabled = display;
		positions [0] = transform.position;
		positions [1] = endpoint;
		lRender.SetPositions (positions);
	}


	void OnTriggerEnter(Collider col){
		if (col.gameObject.GetComponent<Grab_Item> () == true) {
			grabbableObject = col.gameObject;
			if (!grabbed)
			grabbable = grabbableObject.GetComponent<Grab_Item> ();
		}
	}

	void OnTriggerExit(Collider col){
		if (col.gameObject.GetComponent<Grab_Item> () == true) {
			if (!grabbed)
			grabbable = null;
		}
	}
}
