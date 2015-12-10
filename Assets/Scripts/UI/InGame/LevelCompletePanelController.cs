using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelCompletePanelController : MonoBehaviour {

	public GameController gameController;
	public Text coinsEarnedText;
	public GameObject mainMenuButton;
	public GameObject levelSelectButton;
	public GameObject retryButton;
	public Image[] starImages;

	void OnEnable(){
		StartCoroutine (AddUpCoins(gameController.coinsCollectedThisRun));
	}
	void OnDisable(){
		mainMenuButton.SetActive (false);
		retryButton.SetActive(false);
		levelSelectButton.SetActive(false);
		for(int i = 0; i < starImages.Length; i ++){
			starImages[i].gameObject.SetActive(false);
		}
	}

	IEnumerator AddUpCoins(int earned){
		SprongData.playerCoins += earned;
		coinsEarnedText.text = earned.ToString () +"/"+ gameController.maximumCoinsThisLevel.ToString();
		SprongData.SavePlayerData ();
		yield return new WaitForSeconds(1);
		SprongData.SavePlayerData();
		mainMenuButton.SetActive (true);
		levelSelectButton.SetActive(true);
		retryButton.SetActive (true);
		for (int i = 0; i < starImages.Length; i++) {
			starImages[i].color = Color.black;
			starImages[i].gameObject.SetActive(true);
		}

		float coinsCollectedPercent = ((float)earned / (float)gameController.maximumCoinsThisLevel) * 100;

		if (coinsCollectedPercent > 33) {
			starImages[0].color = Color.yellow;
			PlayerPrefs.SetInt ("stars" + SprongData.level, 1);
		}
		
		if (coinsCollectedPercent > 66) {
			starImages[1].color = Color.yellow;
			PlayerPrefs.SetInt ("stars" + SprongData.level, 2);
		}
		if (coinsCollectedPercent >= 100) {
			starImages[2].color = Color.yellow;
			PlayerPrefs.SetInt ("stars" + SprongData.level, 3);
		}
	}


	public void MainMenu(){
		Application.LoadLevel (0);
	}
	
	public void LoadNextLevel(){
		if (Application.loadedLevel < 24) {
			SprongData.level = Application.loadedLevel +1;
			SprongData.SavePlayerData();
			Application.LoadLevel(SprongData.level);

		} else {
			Application.LoadLevel(0);
		}
	}

}
