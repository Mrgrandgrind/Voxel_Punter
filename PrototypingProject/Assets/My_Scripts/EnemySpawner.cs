using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public EventType EventSelected;
	public Transform[] EnemySpawnPoints;
	public GameObject prefab;
	public enum EventType {TriggerEnter,TriggerExit};
	public bool TriggerEvent;
	public GameObject ClosedChest;
	public GameObject OpenChest;

	private bool DoneOnce;
	private bool[] DeadEnemies;

	void Start(){
		DeadEnemies = new bool[EnemySpawnPoints.Length];
	}

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
		GameObject[] EnemySpawn = new GameObject[EnemySpawnPoints.Length];

		EnemySpawn[0] = Instantiate (prefab, EnemySpawnPoints [0].position, EnemySpawnPoints [0].rotation);
		EnemySpawn[0].GetComponent<Turret> ().TriggerSpawnerEvent = TriggerEvent;

		EnemySpawn[1] = Instantiate (prefab, EnemySpawnPoints [1].position, EnemySpawnPoints [1].rotation);
		EnemySpawn[1].GetComponent<Turret> ().TriggerSpawnerEvent = TriggerEvent;
		
		DoneOnce = true;
		//Destroy (gameObject, 0.1f); //immediately destros itself afterwards
	}

	public void EnemyDied(){
	}
}