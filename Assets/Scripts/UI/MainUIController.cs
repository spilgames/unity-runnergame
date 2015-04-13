using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class MainUIController : MonoBehaviour {



	//the components of the UI
	public GameObject characterSelectPanel;
	public GameObject inGamePanel;
	public GameObject gameOverPanel;
	public GameObject levelCompletePanel;
	public GameObject tutorialPanel;

	public CharacterSelectController characterSelect;

	void Start(){
		if (PlayerPrefs.GetInt ("Tutorial", 0) == 0) {
			tutorialPanel.SetActive(true);
			PlayerPrefs.SetInt("Tutorial",1);
		}
	}


	public void UpdateUI(){
		if (GameController.levelState == GameController.LevelState.Character) {
			characterSelectPanel.SetActive (true);
		} else {
			characterSelectPanel.SetActive (false);
		}
		if (GameController.levelState == GameController.LevelState.Playing) {
			inGamePanel.SetActive (true);
		} else {
			inGamePanel.SetActive (false);
		}
		if (GameController.levelState == GameController.LevelState.Died) {
			gameOverPanel.SetActive (true);
		} else {
			gameOverPanel.SetActive (false);
		}
		if (GameController.levelState == GameController.LevelState.Complete) {
			levelCompletePanel.SetActive (true);
		} else {
			levelCompletePanel.SetActive (false);
		}
	}

	public void CloseTutorialPanel(){
		tutorialPanel.SetActive (false);
	}

}
