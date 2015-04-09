using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;
using UnityEngine.UI;
public class GameController : MonoBehaviour {

	//unity ads ids
	public string unityAdsIos;
	public string unityAdsAndroid;

	//speed that the platforms move
	public static float gameSpeed = 0;

	//coins
	public static int playerCoins;
	//the coins the player has
	public static Text coinsText;

	//the UI controller
	public static MainUIController mainUI;
	//character controll
	public static bool[] charactersUnlocked = new bool[24];
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

	void Update(){
		if (Input.GetKeyDown (KeyCode.C)) {
			playerCoins += 1000;
		}
	}

	void Start(){
		coinsText = GameObject.Find ("CoinsText").GetComponent<Text> ();
		if (Advertisement.isSupported) {
			Advertisement.allowPrecache = true;
#if UNITY_ANDROID
		Advertisement.Initialize (unityAdsAndroid);
#endif
#if UNITY_IOS
		Advertisement.Initialize (unityAdsIos);
#endif
		} else {
			Debug.Log("Platform not supported");
		}
		Load ();
		UpdateCoinsText ();
		Application.targetFrameRate = 60;
		mainUI = GameObject.Find ("MainUI").GetComponent<MainUIController> ();
		levelState = LevelState.Character;
		mainUI.UpdateUI ();
	}

	public void NewGame(int character){
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

	public static void UpdateCoinsText(){
		coinsText.text = playerCoins.ToString();
	}


	public static void Save(){
		PlayerPrefs.SetInt ("coins",playerCoins);
		for (int i = 0; i < charactersUnlocked.Length; i++) {
			if(charactersUnlocked[i]){
				PlayerPrefs.SetInt("charactersUnlocked" + i.ToString(),1);
			}else{
				PlayerPrefs.SetInt("charactersUnlocked" + i.ToString(),0);
			}
		}
	}

	public static void Load(){
		playerCoins = PlayerPrefs.GetInt ("coins",0);
		for (int i = 0; i < charactersUnlocked.Length; i++) {
			if(PlayerPrefs.GetInt("charactersUnlocked" + i.ToString(),0) == 1){
				charactersUnlocked[i] = true;
			}
		}
	}

	public void ReloadScene(){
		Application.LoadLevel (Application.loadedLevel);
	}














}
