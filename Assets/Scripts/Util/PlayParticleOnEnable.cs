using UnityEngine;
using System.Collections;

public class PlayParticleOnEnable : MonoBehaviour {

	// Use this for initialization
	void OnEnable () {
		GetComponent<ParticleSystem> ().Play ();
	}

}
