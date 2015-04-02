using UnityEngine;
using System.Collections;

public class WaterSplash : MonoBehaviour {

	public ParticleSystem waterSplashFX;

	void Start(){

	}

	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.tag == "Player") {
			Vector3 pos = coll.gameObject.transform.position;
			waterSplashFX.transform.position =  new Vector2(pos.x,pos.y -0.3f);
			waterSplashFX.Play();
		}
	}

}
