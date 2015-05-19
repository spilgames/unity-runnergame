﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GameOverPanelController : MonoBehaviour {

	public GameController gameController;

	//the text that fills the game over panel
	public Text gameOverInfo;


	//the buttons for delayed showing
	public GameObject homeButton;
	public GameObject pickAnimalButton;
	public GameObject retryButton;

	void OnEnable(){
		gameOverInfo.text = gameController.playerDistancePercent.ToString ("F0") + "%";
		int coinsEarned = Mathf.FloorToInt (gameController.playerDistancePercent / 3);
		StartCoroutine (AddUpCoins(coinsEarned));
	}
	void OnDisable(){
		homeButton.SetActive (false);
		pickAnimalButton.SetActive(false);
		retryButton.SetActive (false);
	}

	IEnumerator AddUpCoins(int earned){
		SprongData.playerCoins += earned;
		SprongData.SavePlayerData ();
		yield return new WaitForSeconds(1);
		homeButton.SetActive (true);
		pickAnimalButton.SetActive(true);
		retryButton.SetActive (true);
	}

	//reload the level
	public void Retry(){
		Application.LoadLevel (Application.loadedLevel);
	}

	public void MainMenu(){
		Application.LoadLevel (0);
	}

	public void LoadLevelSelect(){
		Application.LoadLevel (1);
	}

}
