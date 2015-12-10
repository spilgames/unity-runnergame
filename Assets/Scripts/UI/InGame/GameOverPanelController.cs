using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GameOverPanelController : MonoBehaviour {

	public GameController gameController;

	public Animator characterSelectAnim;

	//the text that fills the game over panel
	public Text gameOverInfo;

	bool characterSelectIn;


	//the buttons for delayed showing
	public GameObject homeButton;
	public GameObject retryButton;
	public GameObject characterSelectButton;

	void OnEnable(){
		gameOverInfo.text = gameController.playerDistancePercent.ToString ("F0") + "%";
		int coinsEarned = Mathf.FloorToInt (gameController.playerDistancePercent / 3);
		StartCoroutine (AddUpCoins(coinsEarned));
	}
	void OnDisable(){
		homeButton.SetActive (false);
		retryButton.SetActive (false);
		characterSelectButton.SetActive (false);
	}

	IEnumerator AddUpCoins(int earned){
		yield return new WaitForSeconds(1);
		homeButton.SetActive (true);
		retryButton.SetActive (true);
		characterSelectButton.SetActive (true);
	}

	//reload the level
	public void Retry(){
		Application.LoadLevel (Application.loadedLevel);
	}

	public void MainMenu(){
		Application.LoadLevel ("Title");
	}

	public void LoadLevelSelect(){
		Application.LoadLevel (1);
	}

	public void TriggerCharacterSelect(){
		Debug.Log ("Trigger");
		if (!characterSelectIn) {
			characterSelectAnim.gameObject.GetComponent<CharacterSelectController>().InitCharacterSelect();
			characterSelectIn = true;
			characterSelectAnim.SetTrigger ("In");
		} else {
			characterSelectIn = false;
			characterSelectAnim.SetTrigger ("Out");
		}
	}


}
