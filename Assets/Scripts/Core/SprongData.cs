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

	//what level is the player in
	public static int level;

	//sound options
	public static int muteMusic;
	public static int muteSFX;

	//save all the player data
	public static void SavePlayerData(){
		//save the sound options
		PlayerPrefs.SetInt ("muteMusic", muteMusic);
		PlayerPrefs.SetInt ("muteSFX", muteSFX);

		//save the level selected
		PlayerPrefs.SetInt ("level",level);

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
				PlayerPrefs.SetInt("levelsUnlocked" + i.ToString(),1);
			}else{
				PlayerPrefs.SetInt("levelsUnlocked" + i.ToString(),0);
			}
		}
		Debug.Log ("Saved DATA at level: " + Application.loadedLevelName );

	}

	//load all the player data
	public static void LoadPlayerData(){

		//save the sound options
		muteMusic = PlayerPrefs.GetInt ("muteMusic",0);
		muteSFX = PlayerPrefs.GetInt ("muteSFX",0);

		level = PlayerPrefs.GetInt ("level",0);
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
				Debug.Log("UNLOCKED: " + i);
			}
		}

		Debug.Log ("LOADED DATA at level: " + Application.loadedLevelName);


	}
}
