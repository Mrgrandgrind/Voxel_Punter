using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public Transform SpawnPoint1;
	public Transform SpawnPoint2;
	public Transform SpawnPoint3;
	public GameObject prefab;

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == "Player") 
		{
			Instantiate (prefab, SpawnPoint1.position, SpawnPoint1.rotation);
			Instantiate (prefab, SpawnPoint2.position, SpawnPoint2.rotation);
			Instantiate (prefab, SpawnPoint3.position, SpawnPoint3.rotation);
			Destroy (gameObject, 0.1f); //immediately destros itself afterwards
		}
	}
}