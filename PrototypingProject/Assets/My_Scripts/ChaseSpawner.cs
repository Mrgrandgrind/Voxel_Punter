using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseSpawner : MonoBehaviour {

	public ParticleSystem Explosion;
	public ParticleSystem Electric;
	public ParticleSystem SpawnSmoke;
	public GameObject Spawnpoint;
	public GameObject Chaser;
	public float MovementSpeed;
	public int SpawnedNum;

	private Vector3 StartPosition;
	private Vector3 EndPosition;
	private Vector3 TargetPosition;

	// Use this for initialization
	void Start () {
		StartPosition = this.transform.position;
		EndPosition = this.transform.position - new Vector3 (6.8f, 0, 0);
		TargetPosition = EndPosition;
		InvokeRepeating ("SpawnChaser", 0.0f, 2.0f);
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = Vector3.Lerp (this.transform.position, TargetPosition, Time.deltaTime / MovementSpeed);

		if (this.transform.position.x <= (EndPosition.x + 0.5f ))
			TargetPosition = StartPosition;
		else if (this.transform.position.x >= (StartPosition.x - 0.5f))
			TargetPosition = EndPosition;
	}

	void SpawnChaser(){
		if (SpawnedNum == 6)
			return;
		
		SpawnedNum++;
		StartCoroutine ("Spawn");
	}

	IEnumerator Spawn(){
		Electric.Play ();
		yield return new WaitForSeconds (0.5f);
		Electric.Stop ();
		Instantiate (SpawnSmoke, Spawnpoint.transform.position, Spawnpoint.transform.rotation);
		GameObject SpawnedChaser = Instantiate (Chaser, Spawnpoint.transform.position, Spawnpoint.transform.rotation);
		SpawnedChaser.GetComponent<Chase>().Spawner = this;
	}

	public void EyeHit(){
		Instantiate (Explosion, this.transform.position, this.transform.rotation);
		Destroy(this.gameObject);
	}
}
