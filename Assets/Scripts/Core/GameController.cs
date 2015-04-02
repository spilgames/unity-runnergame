using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	//speed that the platforms move
	public static float gameSpeed = 0;

	//the UI controller
	public static MainUIController mainUI;

	public int characterSelected;

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
		Character,
		Playing,
		Died,
		Complete
	}
	public static LevelState levelState;


	void Start(){
		Application.targetFrameRate = 60;
		mainUI = GameObject.Find ("MainUI").GetComponent<MainUIController> ();
		levelState = LevelState.Character;
		mainUI.UpdateUI ();
	}

	public void NewGame(int character){
		characterSelected = character;
		player.anim.SetInteger ("Animal",characterSelected);
		movingStuff.transform.position = movingStuffStartPosition.position;
		player.ResetPlayer ();
		gameSpeed = 9.2f;
		levelState = LevelState.Playing;
		mainUI.UpdateUI ();

	}
	public void PlayAgain(){
		NewGame (characterSelected);
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
