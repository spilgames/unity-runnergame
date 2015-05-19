using UnityEngine;
using System.Collections;

public class PlayerPrefsSetup : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if(PlayerPrefs.GetInt("setup",0) == 0){
			PlayerPrefs.SetInt("setup",1);
			SprongData.charactersUnlocked[0] = true;
			SprongData.levelsUnlocked[0] = true;
			SprongData.SavePlayerData();
		}
	}

}
