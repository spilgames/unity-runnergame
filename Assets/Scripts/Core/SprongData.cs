using UnityEngine;
using System.Collections;

public class SprongData : MonoBehaviour {

	//the players coins
	public static int playerCoins;

	//the unlocked characters
	public static bool[] charactersUnlocked = new bool[28];

	//the currently selected character
	public static int characterSelected;

	//save all the player data
	public static void SavePlayerData(){
		PlayerPrefs.SetInt ("coins",playerCoins);
		for (int i = 0; i < charactersUnlocked.Length; i++) {
			if(charactersUnlocked[i]){
				PlayerPrefs.SetInt("charactersUnlocked" + i.ToString(),1);
			}else{
				PlayerPrefs.SetInt("charactersUnlocked" + i.ToString(),0);
			}
		}
		PlayerPrefs.SetInt ("character", characterSelected);
	}

	//load all the player data
	public static void LoadPlayerData(){
		playerCoins = PlayerPrefs.GetInt ("coins", 0);
		for (int i = 0; i < charactersUnlocked.Length; i++) {
			if(PlayerPrefs.GetInt("charactersUnlocked" + i.ToString(),0) == 1){
				charactersUnlocked[i] = true;
			}
		}
		characterSelected = PlayerPrefs.GetInt ("character", 0);
	}
}
