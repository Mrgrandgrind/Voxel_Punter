using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner_1 : MonoBehaviour {

	public EventType EventSelected;
	public Transform[] EnemySpawnPoints;
	public GameObject prefab;
	public enum EventType {TriggerEnter,TriggerExit};
	public GameObject ClosedChest;
	public GameObject OpenChest;
	public Light Light1;
	public Light Light2;
	public ParticleSystem Torch1;
	public ParticleSystem Torch2;
	public ParticleSystem SpawnCloud;
	public ParticleSystem ChestFire;
	public GameObject SpawnPoint;
	public GameObject Bow;
	public AudioSource Music;

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
			Music.Play ();
		}
	}

	void SpawnEnemies(){
		GameObject[] EnemySpawn = new GameObject[EnemySpawnPoints.Length];
		for (int i = 0; i < EnemySpawnPoints.Length; i++) {
			EnemySpawn[i] = Instantiate (prefab, EnemySpawnPoints [i].position, EnemySpawnPoints [i].rotation);
			EnemySpawn[i].GetComponent<Turret> ().TriggerSpawnerEvent = true;
		}
		DoneOnce = true;
		//Destroy (gameObject, 0.1f); //immediately destros itself afterwards
	}

	public void EnemyDied(){
		if (DeadEnemies [0] == false) {
			DeadEnemies [0] = true;
		} else {
			DeadEnemies [1] = true;
			EndEvent ();
		}
	}

	void EndEvent(){
		Light1.enabled = true;
		Light2.enabled = true;
		Torch1.Play ();
		Torch2.Play ();
		ClosedChest.GetComponent<Rigidbody>().AddForce( new Vector3(0,500.0f,0));
		Invoke ("SpawnChest", 1.2f);
	}

	void SpawnChest(){
		OpenChest.GetComponent<Renderer> ().enabled = true;
		Destroy (ClosedChest);
		ChestFire.Play ();
		SpawnCloud.Play ();
		GameObject Spawn = Instantiate (Bow, SpawnPoint.transform.position,SpawnPoint.transform.rotation);
		Spawn.GetComponent<Rigidbody> ().isKinematic = true;
	}
}