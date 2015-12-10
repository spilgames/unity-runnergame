using UnityEngine;
using System.Collections;

public class PreLoad : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Invoke ("LoadTitle",2);
	}

	void LoadTitle(){
		Application.LoadLevel ("Title");
	}
}
