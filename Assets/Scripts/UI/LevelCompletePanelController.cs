using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelCompletePanelController : MonoBehaviour {

	public GameController gameController;
	public Text coinsEarnedText;
	public GameObject mainMenuButton;
	public GameObject levelSelectButton;

	void OnEnable(){
		int coinsEarned = Mathf.FloorToInt (gameController.playerDistancePercent / 3);
		StartCoroutine (AddUpCoins(coinsEarned));
	}
	void OnDisable(){
		mainMenuButton.SetActive (false);
		mainMenuButton.SetActive(false);
	}

	IEnumerator AddUpCoins(int earned){
		earned += 100;
		GameController.playerCoins += earned;
		GameController.UpdateCoinsText ();
		coinsEarnedText.text = earned.ToString ();
		GameController.Save ();
		yield return new WaitForSeconds(1);
		mainMenuButton.SetActive (true);
		levelSelectButton.SetActive(true);
	}


	public void MainMenu(){
		Application.LoadLevel (0);
	}
	
	public void LoadLevelSelect(){
		Application.LoadLevel (1);
	}

}
