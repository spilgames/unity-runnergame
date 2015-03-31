using UnityEngine;
using System.Collections;

public class DisableLevelSection : MonoBehaviour {

	// Use this for initialization
	void OnEnable () {
		Invoke ("DestroySection", 10f);
	}
	
	void DestroySection(){
		gameObject.SetActive (false);
	}

	void OnDisable(){
		CancelInvoke ();
	}
}
