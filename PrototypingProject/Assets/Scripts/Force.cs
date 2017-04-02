using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class Force : MonoBehaviour {

	public GameObject Hook;

	private SteamVR_Controller.Device controller;
	private LineRenderer lRender;
	private Vector3[] positions;
	private GameObject grabbableObject;
	private Grab_Item grabbable;
	private bool grabbing;
	private bool Locked;
	private float lerpTime;
	private Vector3 LastHandPos;
	private Vector3 ObjectPos;

	private List<GameObject> Overlaps = new List<GameObject>();

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
		if (!grabbing) {
			if (!grabbable)
				return;
		}
	
		 /// Controller events ///////////////////////////
		if (controller.GetPressDown (SteamVR_Controller.ButtonMask.Grip)) { // Force Grab
			grabbing = true;
			ObjectPos = transform.position;
		}

		else if (controller.GetPress (SteamVR_Controller.ButtonMask.Grip)) { // Force move

			if (Hook.GetComponent<HookOverlaps>().HitGameObject == grabbableObject) {
				Locked = true;
				grabbable.Grab(true);
			}

			if (Locked == true) {
				grabbable.Move (transform.position);
			}

			ObjectPos = Vector3.MoveTowards (ObjectPos, grabbableObject.transform.position, Time.deltaTime / lerpTime);
			DisplayLine (true, ObjectPos);
		}

		else if (controller.GetPressUp (SteamVR_Controller.ButtonMask.Grip)) { // Release
			DisplayLine (false, transform.position);
			grabbing = false;
			Locked = false;
			grabbable.Grab(false);
		}
		/////////////////////////////////////////////////
	}

	void DisplayLine(bool display, Vector3 endpoint){
		lRender.enabled = display;
		positions [0] = transform.position;
		positions [1] = endpoint;
		lRender.SetPositions (positions);
		Hook.transform.position = endpoint;
	}


	void OnTriggerEnter(Collider col){
		if (col.gameObject.GetComponent<Grab_Item> () == true) {
			Overlaps.Add (col.gameObject); // adds overlap to list

			int MinSlot = 0;
			float min = Vector3.Distance (Overlaps[0].transform.position, transform.position);
			float[] dist = new float[Overlaps.Count];

			for (int i = 0; i < Overlaps.Count; i++){
				dist[i] = Vector3.Distance (Overlaps[i].transform.position, transform.position);
				if (dist [i] < min) {
					min = dist [i];
					MinSlot = i;
				}
			}

			grabbableObject = Overlaps[Overlaps.Count-1]; // sets overlap to grabbale object
			grabbableObject.GetComponent<VRTK_InteractableObject> ().ToggleHighlight (true); // Highlights Overlap
	
			if (!grabbing)
				grabbable = grabbableObject.GetComponent<Grab_Item> ();
		}
	}

	void OnTriggerExit(Collider col){
		if (col.gameObject.GetComponent<Grab_Item> () == true) {
			if (!grabbing) {
				Overlaps.Remove(col.gameObject);
				col.gameObject.GetComponent<VRTK_InteractableObject> ().ToggleHighlight (false);
				grabbable = null;
			}
		}
	}
}
