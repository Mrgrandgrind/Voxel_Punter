using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHud : MonoBehaviour {

	public GameObject[] HeartLocation;
	public GameObject FullHeart;
	public GameObject EmptyHeart;
	public int HealthRemaining;

	private GameObject[] HUD = new GameObject[3];
	public bool Damaged;

	// Use this for initialization
	void Start () {
		HealthRemaining = 3;
		for (int i = 0; i < 3; i++) {
			HUD[i] = Instantiate (FullHeart, HeartLocation [i].transform.position, HeartLocation [i].transform.rotation);
			HUD[i].transform.localScale = HeartLocation [i].transform.localScale;
			HUD[i].transform.parent = this.transform;
			HUD [i].SetActive (false);
		}
	}
		
	public void DisplayHud(bool state){
		if (Damaged)
			state = true;

		if (state == true) {
			for (int i = 0; i < HealthRemaining; i++) {
				HUD [i].SetActive (state);
			}
		} else {

			for (int i = 0; i < 3; i++) {
				HUD [i].SetActive (state);
			}
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
