using UnityEngine;
using System.Collections;

public class TreePassFX : MonoBehaviour {

	public ParticleSystem treeFX;
	
	void Start(){
		
	}
	
	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.tag == "Player") {
			treeFX.Play();
		}
	}
}
