using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GameController : MonoBehaviour {
	
	public GoogleAnalyticsV3 analytics;

	//speed that the platforms move
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

	//the different states of a level
	public enum LevelState{
		Playing,
		Died,
		Complete
	}
	public static LevelState levelState;


	void Start(){
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
	}

	public void NewGame(){
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
		//stop the moving stuff
		gameSpeed = 0;
		//if the player died
		if (died) {
			levelLength = levelEnd.position.x - levelStart.position.x;
			playerDistancePercent = ((playerDistance - levelStart.position.x) / levelLength) * 100; 
			levelState = LevelState.Died;
			mainUI.UpdateUI ();
		} else {
			switch(Application.loadedLevel){
			case 2:
				PlayerPrefs.SetInt("level2",1);
				break;
			case 3:
				PlayerPrefs.SetInt("level3",1);
				break;
			case 4:
				PlayerPrefs.SetInt("rate",1);
				break;
			default:
				break;
			}
			levelState = LevelState.Complete;
			mainUI.UpdateUI();
		}
	}
}
