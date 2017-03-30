using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Force : MonoBehaviour {
	private SteamVR_Controller.Device controller;
	private LineRenderer lRender;
	private Vector3[] positions;
	private Grab_Item grabbable;
	private bool grabbed;
	private bool gripped;

	private Vector3 LastHandPos;

	// Use this for initialization
	void Start () {
		SteamVR_TrackedObject trackedObj = GetComponent<SteamVR_TrackedObject> ();
		controller = SteamVR_Controller.Input ((int)trackedObj.index);
		lRender = GetComponent<LineRenderer> ();
		positions = new Vector3[2];
	}
	
	// Update is called once per frame
	void Update () {
		if (!grabbed) {
			grabbable = RaycastForGrabbableObject();
			if (!grabbable)
				return;
		}
			
		Vector3 CurrentHandPos = transform.position;
		/*
		if (controller.GetPressDown (SteamVR_Controller.ButtonMask.Grip)) {
			grabbed = true;
			grabbable.Grab (false);
			DisplayLine (false, transform.position);
		}

		else if (controller.GetPressDown (SteamVR_Controller.ButtonMask.Grip)) {
			grabbed = true;
			grabbable.Grab (false);
			DisplayLine (false, transform.position);
		}

		else if (controller.GetPressUp (SteamVR_Controller.ButtonMask.Grip)){
			grabbed = false;
		}
		*/

		if (controller.GetPressDown (SteamVR_Controller.ButtonMask.Trigger)) { // Force Grab
			grabbed = true;
			grabbable.Grab(true);
			LastHandPos = CurrentHandPos;
			DisplayLine (false, transform.position);
		} 

		else if (controller.GetPress (SteamVR_Controller.ButtonMask.Trigger)) { // Force move
			grabbable.Move(CurrentHandPos, LastHandPos);
		} 

		else if (controller.GetPressUp (SteamVR_Controller.ButtonMask.Trigger)) { // Release
			grabbed = false;
			grabbable.Grab(false);
		}

		LastHandPos = CurrentHandPos;
	}

	private Grab_Item RaycastForGrabbableObject(){
		RaycastHit hit;
		Ray r = new Ray (transform.position, transform.forward);
		Debug.DrawRay (transform.position, transform.forward);

		if (Physics.Raycast (r, out hit, Mathf.Infinity) && hit.collider.gameObject.GetComponent<Grab_Item>() != null) {
			DisplayLine (true, hit.point);
			return hit.collider.gameObject.GetComponent<Grab_Item>();
		}
		else {
			DisplayLine (false, transform.position);
			return null;
		}
	}

	void DisplayLine(bool display, Vector3 endpoint){
		lRender.enabled = display;
		positions [0] = transform.position;
		positions [1] = endpoint;
		lRender.SetPositions (positions);
	}
}
