using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GameOverPanelController : MonoBehaviour {

	public GameController gameController;

	//the text that fills the game over panel
	public Text gameOverInfo;
	public Text coinsEarnedText;

	void OnEnable(){
		gameOverInfo.text = gameController.playerDistancePercent.ToString ("F0") + "%";
		int coinsEarned = Mathf.FloorToInt (gameController.playerDistancePercent / 3);
		StartCoroutine (AddUpCoins(coinsEarned));
	}

	IEnumerator AddUpCoins(int earned){
		for (int i = 0; i< earned; i++) {
			GameController.playerCoins ++;
			GameController.UpdateCoinsText ();
			coinsEarnedText.text = (i +1).ToString ();
			yield return new WaitForSeconds(0.05f);
		}
		GameController.Save ();
	}

	//reload the level
	public void Retry(){
		Application.LoadLevel (Application.loadedLevel);
	}

	public void MainMenu(){
		Application.LoadLevel (0);
	}
	

}
