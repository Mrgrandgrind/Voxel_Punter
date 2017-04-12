using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
public class test : MonoBehaviour {

	public GameObject vrtkContoller;
	private SteamVR_Controller.Device controller;



	void Start(){
		SteamVR_TrackedObject trackedObj = GetComponent<SteamVR_TrackedObject> ();
		controller = SteamVR_Controller.Input ((int)trackedObj.index);
	}

	// Update is called once per frame
	void Update () {
		if (controller.GetPressDown (SteamVR_Controller.ButtonMask.ApplicationMenu)) { // Force Grab
			vrtkContoller.GetComponent<VRTK_ObjectAutoGrab>().grabStart();
		}
	}
}
