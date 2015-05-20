using UnityEngine;
using System.Collections;

public class DisableAfterParticleFX : MonoBehaviour {

	// Use this for initialization
	void OnEnable () {
		Invoke ("Deactivate", GetComponent<ParticleSystem>().duration);
	}

	void Deactivate(){
		gameObject.SetActive (false);
	}

	void OnDisable(){
		CancelInvoke ("Deactivate");
	}
	

}
