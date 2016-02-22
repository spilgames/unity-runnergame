using UnityEngine;
using System.Collections;

public class PreLoad : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Invoke ("LoadTitle",2);
		JSONObject configData = new JSONObject (Spil.GetConfigAll ());
		SprongData.characterCostModifier = (int)configData.GetField ("characterCost").n;
	}

	void LoadTitle(){
		Application.LoadLevel ("Title");
	}
}
