using UnityEngine;
using System.Collections;

public class SprongData : MonoBehaviour {

	//the players coins
	public static int playerCoins;

	//the unlocked characters
	public static bool[] charactersUnlocked = new bool[28];

	//the unlocked levels
	public static bool[] levelsUnlocked = new bool[24];

	//the currently selected character
	public static int characterSelected;

	//save all the player data
	public static void SavePlayerData(){

		//save the amount of coins that the player has
		PlayerPrefs.SetInt ("coins",playerCoins);

		//save which character the player is using
		PlayerPrefs.SetInt ("character", characterSelected);

		//save which characters have been unlocked
		for (int i = 0; i < charactersUnlocked.Length; i++) {
			if(charactersUnlocked[i]){
				PlayerPrefs.SetInt("charactersUnlocked" + i.ToString(),1);
			}else{
				PlayerPrefs.SetInt("charactersUnlocked" + i.ToString(),0);
			}
		}

		//save which levels have been unlocked
		for (int i = 0; i < levelsUnlocked.Length; i ++) {
			if(levelsUnlocked[i]){
				PlayerPrefs.SetInt("levelsUnlocked" + (i+1).ToString(),1);
			}else{
				PlayerPrefs.SetInt("levelsUnlocked" + (i+1).ToString(),0);
			}
		}

	}

	//load all the player data
	public static void LoadPlayerData(){
		playerCoins = PlayerPrefs.GetInt ("coins", 0);
		characterSelected = PlayerPrefs.GetInt ("character", 0);
		for (int i = 0; i < charactersUnlocked.Length; i++) {
			if(PlayerPrefs.GetInt("charactersUnlocked" + i.ToString(),0) == 1){
				charactersUnlocked[i] = true;
			}
		}
		for (int i = 0; i < levelsUnlocked.Length; i++) {
			if(PlayerPrefs.GetInt("levelsUnlocked" + i.ToString(),0) == 1){
				levelsUnlocked[i] = true;
			}
		}

	}
}
