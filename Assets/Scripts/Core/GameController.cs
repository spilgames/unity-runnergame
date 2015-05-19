using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using ChartboostSDK;
public class GameController : MonoBehaviour {

	public GoogleAnalyticsV3 analytics;

	int gamePlaysThisSession;

	//unity ads ids
	public string unityAdsIos;
	public string unityAdsAndroid;

	//the rate me popup
	public GameObject rateUsScreen;

	//speed that the platforms move
	public static float gameSpeed = 0;

	//the coins the player has
	public static Text coinsText;

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
	//the different states of a level
	public enum LevelState{
		Playing,
		Died,
		Complete
	}
	public static LevelState levelState;


	void Start(){
		analytics = GameObject.Find ("GAv3").GetComponent<GoogleAnalyticsV3> ();
		if (PlayerPrefs.GetInt ("Mute", 0) == 1) {
			AudioListener.pause = true;
		} else {
			AudioListener.pause = false;
		}
		coinsText = GameObject.Find ("CoinsText").GetComponent<Text> ();
		Application.targetFrameRate = 60;
		mainUI = GameObject.Find ("MainUI").GetComponent<MainUIController> ();
		levelState = LevelState.Playing;
		mainUI.UpdateUI ();
	}

	public void NewGame(){
		int character = PlayerPrefs.GetInt ("Character",0);
		gamePlaysThisSession ++;
		analytics.LogScreen (Application.loadedLevelName);
		if (charactersUnlocked [character]) {
			characterSelected = character;
			player.anim.SetInteger ("Animal", characterSelected);
			movingStuff.transform.position = movingStuffStartPosition.position;
			player.ResetPlayer ();
			gameSpeed = 9.2f;
			levelState = LevelState.Playing;
			mainUI.UpdateUI ();
		} else {
			mainUI.characterSelect.ShowLockedCharacterScreen(character);
		}
	}
	public void PlayAgain(){
		NewGame ();
	}

	//end the game on completion or on player death
	public void EndGame(bool died){
		gameSpeed = 0;
		if (died) {
			if(gamePlaysThisSession != 0 && gamePlaysThisSession % 6 == 0 && Chartboost.hasInterstitial(CBLocation.GameOver)){
				Chartboost.showInterstitial(CBLocation.GameOver);
			}
			levelLength = levelEnd.position.x - levelStart.position.x;
			playerDistancePercent = ((playerDistance - levelStart.position.x) / levelLength) * 100; 
			levelState = LevelState.Died;
			mainUI.UpdateUI ();
			EventHitBuilder endGameEvent = new EventHitBuilder();
			endGameEvent.SetEventAction("Death");
			endGameEvent.SetEventCategory("Player Event");
			endGameEvent.SetEventLabel(Application.loadedLevelName);
			endGameEvent.SetEventValue(1);
			analytics.LogEvent(endGameEvent);
		} else {

			switch(Application.loadedLevel){
			case 2:
				PlayerPrefs.SetInt("level2",1);
				break;
			case 3:
				PlayerPrefs.SetInt("level3",1);
				break;
			case 4:
				if(PlayerPrefs.GetInt("rate", 0) == 0){
					rateUsScreen.SetActive(true);
					PlayerPrefs.SetInt("rate",1);
				}
				break;
			default:
				break;
			}
			levelState = LevelState.Complete;
			mainUI.UpdateUI();
		}
	}

	public void ReloadScene(){
		Application.LoadLevel (Application.loadedLevel);
	}
}
