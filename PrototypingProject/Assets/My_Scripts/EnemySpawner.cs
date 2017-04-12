using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public EventType EventSelected;
	public Transform[] EnemySpawnPoints;
	public GameObject prefab;

	public enum EventType {TriggerEnter,TriggerExit};
	private bool DoneOnce;

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == "Player" && DoneOnce == false && EventSelected == EventType.TriggerEnter) {
			SpawnEnemies ();
		}
	}

	void OnTriggerExit (Collider other)
	{
		if (other.gameObject.tag == "Sword" && DoneOnce == false && EventSelected == EventType.TriggerExit) {
			SpawnEnemies ();
		}
	}

	void SpawnEnemies(){
		for (int i = 0; i < EnemySpawnPoints.Length; i++) {
			Instantiate (prefab, EnemySpawnPoints [i].position, EnemySpawnPoints [i].rotation);
		}
		DoneOnce = true;
		Destroy (gameObject, 0.1f); //immediately destros itself afterwards
	}
}