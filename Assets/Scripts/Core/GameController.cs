﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
public class GameController : MonoBehaviour {

	//speed that the platforms move

	public static float playingSpeed = 9.2f;
	public static float gameSpeed = 0;

	//the UI controller
	public static MainUIController mainUI;

	//starting positions for retrying the level
	public Transform playerStartPosition;
	public Transform movingStuffStartPosition;
	public GameObject movingStuff;
	public PlayerController player;

	//how far through the level did the player get
	public Transform levelStart;
	public Transform levelEnd;
	public float levelLength;
	public float playerDistance;
	public float playerDistancePercent;

	//coins collected during the playthrough
	public int coinsCollectedThisRun;
	public int maximumCoinsThisLevel;

	List<GameObject> coinsInThisLevel = new List<GameObject>();

	//the different states of a level
	public enum LevelState{
		Playing,
		Died,
		Complete
	}
	public static LevelState levelState;

	public SFXController sfx;

	void Start(){
		Dictionary<string,string> eventDetails = new Dictionary<string, string> ();
		eventDetails.Add ("level",Application.loadedLevelName);
		Spil.TrackEvent ("levelStart", eventDetails);
		sfx = GameObject.Find ("SoundFX").GetComponent<SFXController>();
		SprongData.LoadPlayerData ();
		GameObject[] coins = GameObject.FindGameObjectsWithTag ("Coin");
		for (int i = 0; i < coins.Length; i++) {
			coinsInThisLevel.Add(coins[i]);
		}
		maximumCoinsThisLevel = coins.Length;
		mainUI = GameObject.Find ("MainUI").GetComponent<MainUIController>();
		//if mute selected then mute the sound right away
		if (PlayerPrefs.GetInt ("Mute", 0) == 1) {
			AudioListener.pause = true;
		} else {
			AudioListener.pause = false;
		}
		//try to force 60 fps
		Application.targetFrameRate = 60;
		//start the game playing
		levelState = LevelState.Playing;
		//activate the correct UI panel
		mainUI.UpdateUI ();
		NewGame ();
	}

	public void NewGame(){
		//activate all coins
		for(int i = 0; i < coinsInThisLevel.Count; i ++){
			coinsInThisLevel[i].SetActive(true);
		}
		coinsCollectedThisRun = 0;
		//reset the moving stuff
		movingStuff.transform.position = movingStuffStartPosition.position;
		//reset the player 
		player.ResetPlayer ();
		//set the speed of the moving stuff
		gameSpeed = 9.2f;
		//set the correct UI
		levelState = LevelState.Playing;
		mainUI.UpdateUI ();
	}

	public void PlayAgain(){
		NewGame ();
	}

	//end the game on completion or on player death
	public void EndGame(bool died){
		//if the player died
		if (died) {
			gameSpeed = 0;
			levelLength = levelEnd.position.x - levelStart.position.x;
			playerDistancePercent = ((playerDistance - levelStart.position.x) / levelLength) * 100; 
			levelState = LevelState.Died;
			mainUI.UpdateUI ();
			Dictionary<string,string> eventDetails = new Dictionary<string, string> ();
			eventDetails.Add ("level",Application.loadedLevelName);
			Spil.TrackEvent ("playerDies", eventDetails);
		} else {
			if(Application.loadedLevel < SprongData.levelsUnlocked.Length){
				SprongData.levelsUnlocked[Application.loadedLevel] = true;
			}
//			SprongData.SavePlayerData();
			levelState = LevelState.Complete;
			mainUI.UpdateUI();
			Dictionary<string,string> eventDetails = new Dictionary<string, string> ();
			eventDetails.Add ("level",Application.loadedLevelName);
			Spil.TrackEvent ("levelComplete", eventDetails);
		}
	}

}
