using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Pickup : MonoBehaviour {

	public ParticleSystem HealthParticle;

	// Update is called once per frame
	void Update () {
		transform.Rotate (0,80*Time.deltaTime,0);
	}

	public void SpawnParticle(){
		Instantiate (HealthParticle, this.transform.position, this.transform.rotation);
	}
}
