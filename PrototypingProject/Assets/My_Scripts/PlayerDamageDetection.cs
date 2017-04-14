using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDamageDetection : MonoBehaviour {

	public ParticleSystem HitShock;
	public GameObject HealthHud;

	private bool CurrentlyDamaged;
	private int Health = 3;
	private HealthHud Hudreference;
	void Start(){
		Hudreference = HealthHud.GetComponent<HealthHud> ();
	}

	public void AddHealth(){
		if (Health < 3)
			Health++;

		Hudreference.DisplayHud (false);
		Hudreference.HealthRemaining = Health;
		Hudreference.Damaged = true;
		Hudreference.DisplayHud (true);
		Invoke ("UpdatePlayerHealth", 1);
	}

	void OnTriggerEnter(Collider Col){
		if (Col.tag == "projectile" || Col.tag == "Hazzard") {

			if (Col.tag == "projectile") {
				Instantiate (HitShock, Col.transform.position, Col.transform.rotation);
				Destroy (Col.gameObject);
			}

			if (!CurrentlyDamaged) {
				CurrentlyDamaged = true;
				Health--;
				Hudreference.DisplayHud (false);
				Hudreference.HealthRemaining = Health;
				Hudreference.Damaged = true;
				Hudreference.DisplayHud (true);
				Invoke ("UpdatePlayerHealth", 1);
				if (Health == 0)
					SceneManager.LoadScene ("VoxelPunter");
			}
		}
	}

	void UpdatePlayerHealth(){
		Hudreference.Damaged = false;
		Hudreference.DisplayHud (false);
		CurrentlyDamaged = false;

	}
}
