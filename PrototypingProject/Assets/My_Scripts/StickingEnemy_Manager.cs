using System.Collections;
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

	void OnTriggerEnter(Collider col){

		// If Overlap contains Chase script
		if (col.GetComponent<Chase>() != null) {
			
			// Distance variables
			float[] dist = new float[6];

			// Set Minimum 
			float min = Vector3.Distance (StickLocationActors [0].transform.position, col.transform.position);
			int MinSlot = 0;

			// Gets distance between all the stick locations, outputs the shortest one
			for (int i = 1; i < 6; i++) {
				dist[i] = Vector3.Distance (StickLocationActors [i].transform.position, col.transform.position);
				if (dist [i] < min) {
					min = dist [i];
					MinSlot = i;
				}
			}

			// If Stick location isn't occupied, Occupy it
			if (LocationsUsed [MinSlot] == false) {
				StickEnemyToPlayer (col, MinSlot);
				return;
			} 

			// If Shortest location IS occupied, loop through all until an empty one is found
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
		
	// Stick Enemy to player in empty location
	void StickEnemyToPlayer(Component col ,int EnemyNum){
		col.gameObject.GetComponent<Collider> ().isTrigger = true;
		col.GetComponent<Rigidbody> ().isKinematic = true;

		// Tell chaser to stick to chosen location
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


