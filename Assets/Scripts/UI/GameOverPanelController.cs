using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GameOverPanelController : MonoBehaviour {

	public GameController gameController;

	//the text that fills the game over panel
	public Text gameOverInfo;

	void OnEnable(){
		gameOverInfo.text = gameController.playerDistancePercent.ToString ("F0") + "%";
	}

	//reload the level
	public void Retry(){
		Application.LoadLevel (Application.loadedLevel);
	}

	public void MainMenu(){
		Application.LoadLevel (0);
	}
	

}
