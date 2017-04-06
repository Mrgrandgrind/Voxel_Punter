using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using VRTK.Examples;

public class Force : MonoBehaviour {

	public GameObject Hook;
	public GameObject vrtkController;
	public bool Held;
	public Color PointingColour;
	public Color SelectColour;

	private SteamVR_Controller.Device controller;
	private LineRenderer lRender;
	private Vector3[] positions;
	private GameObject grabbableObject;
	private Grab_Item grabbable;
	private Renderer HookRend;
	private bool grabbing;
	private bool Locked;
	private bool first;
	private float lerpTime;
	private int ObjectSelect;
	private Vector3 LastHandPos;
	private Vector3 ObjectPos;

	private List<GameObject> Overlaps = new List<GameObject>();

	// Use this for initialization
	void Start () {
		SteamVR_TrackedObject trackedObj = GetComponent<SteamVR_TrackedObject> ();
		controller = SteamVR_Controller.Input ((int)trackedObj.index);
		lRender = GetComponent<LineRenderer> ();
		HookRend = Hook.GetComponent<Renderer> ();
		positions = new Vector3[2];
		lerpTime = 0.05f;
		Locked = false;
	}

	// Update is called once per frame
	void Update () {

		//if (controller.GetPressDown (SteamVR_Controller.ButtonMask.ApplicationMenu)) { // Force Grab
		//	vrtkController.GetComponent<VRTK_InteractGrab>().AttemptGrab();
		//}

		if (!grabbing){
			if (!grabbable)
				return;
		}
		 /// Controller events ///////////////////////////
		if (grabbing == true && Held == false) {
			HookShot ();
		} else if (controller.GetPressDown (SteamVR_Controller.ButtonMask.Grip) && Held == false) { // Force Grab
			HookRend.material.SetColor ("_Color", Color.red);
		} else if (controller.GetPressUp (SteamVR_Controller.ButtonMask.Grip) && Held == false) {
			HookRend.material.SetColor ("_Color", Color.green);
			grabbing = true;
			ObjectPos = transform.position;
		}

		/////////////////////////////////////////////////
	}

	void HookShot(){
		// Object is now in players hand
		if (grabbable.inHand == true) {
			GrabPulledObject(); // Grabs object with controller
			return;
		}

		// 
		if (Locked == true) {
			grabbable.Move (true,transform.position);
		}

		// If hook has hit the aimed at object
		if (Hook.GetComponent<HookOverlaps>().HitGameObject == grabbableObject) {
			Locked = true;
			grabbable.Grab(true);
		}
			
		ObjectPos = Vector3.MoveTowards (ObjectPos, grabbableObject.transform.position, Time.deltaTime / lerpTime);
		DisplayLine (true, ObjectPos);
	}

	public void Ungrabbed(){
		if (!first) {
			first = true;
			return;
		}
		HookRend.material.SetColor ("_Color", Color.white);
		Held = false;
		UnGrabPulledObject ();
	}


	void UnGrabPulledObject(){
		grabbable.Move (false, grabbable.transform.position);
	}

	void GrabPulledObject(){
		DisplayLine (false, transform.position);
		grabbing = false;
		Locked = false;
		grabbable.Grab(false);
		Held = true;
		vrtkController.GetComponent<VRTK_InteractGrab>().AttemptGrab();
	}

	void DisplayLine(bool display, Vector3 endpoint){
		lRender.enabled = display;
		positions [0] = transform.position;
		positions [1] = endpoint;
		lRender.SetPositions (positions);
		Hook.transform.position = endpoint;
	}


	public void ButtonUp(){
		ObjectSelect++;
		CurrentSelection ();
	}

	public void ButtonDown(){
		ObjectSelect--;
		CurrentSelection ();
	}


	void OnTriggerEnter(Collider col){
		if (col.gameObject.GetComponent<Grab_Item> () == true && !grabbing && !Held) {
			Overlaps.Add (col.gameObject); // adds overlap to list
			CurrentSelection();
		}
	}

	void OnTriggerExit(Collider col){
		if (col.gameObject.GetComponent<Grab_Item> () == true) {
			Overlaps.Remove(col.gameObject);
			col.gameObject.GetComponent<VRTK_InteractableObject> ().ToggleHighlight (false);
			if (Overlaps.Count == 0 && !grabbing && !Held) {
				grabbable = null;
			}
		}
	}

	void CurrentSelection(){

		for (int i = 0; i < Overlaps.Count; i++) {
			Overlaps[i].GetComponent<VRTK_InteractableObject> ().touchHighlightColor = PointingColour; // Highlights Overlap
			Overlaps[i].GetComponent<VRTK_InteractableObject> ().ToggleHighlight (true); // Highlights Overlap
		}

		// Reset to start
		if (ObjectSelect + (Overlaps.Count - 1) > Overlaps.Count - 1) {
			ObjectSelect = - Overlaps.Count + 1;
		}

		// Reset to end
		else if (ObjectSelect + (Overlaps.Count - 1) < 0) {
			ObjectSelect = 0;
		}

		int Selection = ObjectSelect + (Overlaps.Count - 1);

		grabbableObject = Overlaps[Selection]; // sets overlap to grabbale object
		grabbableObject.GetComponent<VRTK_InteractableObject> ().touchHighlightColor = SelectColour; // Highlights Overlap
		grabbableObject.GetComponent<VRTK_InteractableObject> ().ToggleHighlight (true); // Highlights Overlap

		if (!grabbing && !Held)
			grabbable = grabbableObject.GetComponent<Grab_Item> ();
	}
}

