﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK.Examples;

public class StickingEnemy_Manager : MonoBehaviour {

	public GameObject[] StickLocationActors;
	public bool[] LocationsUsed = new bool[6];
	private GameObject PlayerHead;


	// Use this for initialization
	void Start () {
		PlayerHead = GameObject.Find ("Camera (eye)");
	}
		/*
	var max = intArray[0];
	for (int i = 1; i < 6; i++) {
		if (intArray[i] > max) {
			max = intArray[i];
		}
	}
	return max;
*/

	void OnTriggerEnter(Collider col){
		/*
		if (col.GetComponent<Chase>() != null) {
			for (int i = 0; i < 6; i++) {
				if (LocationsUsed [i] == false) {
					Debug.LogError (i);
					StickEnemyToPlayer (col, i);
					return;
				}
			}
		}
		*/

		if (col.GetComponent<Chase>() != null) {
			float[] dist = new float[6];
			float min = Vector3.Distance (StickLocationActors [0].transform.position, col.transform.position);
			int MinSlot = 0;

			for (int i = 1; i < 6; i++) {
				dist[i] = Vector3.Distance (StickLocationActors [i].transform.position, col.transform.position);
				if (dist [i] < min) {
					min = dist [i];
					MinSlot = i;
				}
			}

			if (LocationsUsed [MinSlot] == false) {
				StickEnemyToPlayer (col, MinSlot);
				return;
			} 

			else{
				for (int i = 0; i < 6; i++) {
					if (LocationsUsed [i] == false) {
						StickEnemyToPlayer (col, i);
						return;
					} 
				}
			}
		}

	}
		
	void StickEnemyToPlayer(Component col ,int EnemyNum){

		//col.transform.position = StickLocationActors [EnemyNum].transform.position;
		//col.transform.rotation = Quaternion.LookRotation (PlayerHead.transform.position - col.transform.position);
		//col.transform.parent = PlayerHead.transform;
		col.gameObject.GetComponent<Collider> ().isTrigger = true;
		col.GetComponent<Rigidbody> ().isKinematic = true;
		col.GetComponent<Chase> ().StickLocation = StickLocationActors [EnemyNum].transform.position;
		col.GetComponent<Chase> ().stuck = true;
		LocationsUsed [EnemyNum] = true;
		col.GetComponent<Chase>().FaceLocation = EnemyNum;
	}

	void OnTriggerExit (Collider col){
		if (col.GetComponent<Chase> () != null) {
			LocationsUsed [col.GetComponent<Chase> ().FaceLocation] = false;
			col.GetComponent<Chase> ().parented = false;
		}
	}
}

	/*
	if (LocationsUsed [0] == false) {
		StickEnemyToPlayer (col, 0);
	} else if (LocationsUsed [1] == false) {
		StickEnemyToPlayer (col, 1);
	} else if (LocationsUsed [2] == false) {
		StickEnemyToPlayer (col, 2);
	} else if (LocationsUsed [3] == false) {
		StickEnemyToPlayer (col, 3);
	} else if (LocationsUsed [4] == false) {
		StickEnemyToPlayer (col, 4);
	} else if (LocationsUsed [5] == false) {
		StickEnemyToPlayer (col, 5);
	}
	*/

	/*while (SpotFilled == false) {
	SpotNum = Random.Range (0, 6);
	if (LocationsUsed [SpotNum] == false) {
		StickEnemyToPlayer (col, SpotNum);
		SpotFilled = true;
	}
}*/


