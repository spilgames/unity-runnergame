using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	//speed that the platforms move
	public static float gameSpeed = 9.2f;

	//the UI controller
	public static MainUIController mainUI;

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
		gameSpeed = 9.2f;
		Application.targetFrameRate = 60;
		mainUI = GameObject.Find ("MainUI").GetComponent<MainUIController> ();
		levelState = LevelState.Playing;
		mainUI.UpdateUI ();
	}



	//end the game on completion or on player death
	public void EndGame(bool died){
		gameSpeed = 0;
		if (died) {
			levelLength = levelEnd.position.x - levelStart.position.x;
			playerDistancePercent = ((playerDistance - levelStart.position.x) / levelLength) * 100; 
			levelState = LevelState.Died;
			mainUI.UpdateUI ();
		} else {
			levelState = LevelState.Complete;
			mainUI.UpdateUI();
		}
	}
}
