using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;
public class TitleUIController : MonoBehaviour {

	//unity ads ids
	public string unityAdsIos;
	public string unityAdsAndroid;

	//the panel animators
	public Animator characterSelectAnimator;
	public Animator settingsPanelAnimator;

	//the character select controller
	public CharacterSelectController characterSelect;


	void Start(){
		InitUnityAds ();
		//load the status of unlocks so far
		SprongData.LoadPlayerData ();
		//make sure that at least the fist character is unlocked
		SprongData.charactersUnlocked [0] = true;
	}

	void Update(){
		if(Input.GetKeyDown(KeyCode.X)){
			PlayerPrefs.DeleteAll();
			Debug.Log("PlayerPrefsCleared");
		}
	}
	
	//init the unity ads sdk
	void InitUnityAds(){
		#if UNITY_ANDROID
		Advertisement.Initialize (unityAdsAndroid);
		#endif
		#if UNITY_IOS
		Advertisement.Initialize (unityAdsIos);
		#endif
	}

	//load the character select panel
	public void LoadCharacterSelectPanel(){
		characterSelectAnimator.SetTrigger ("In");
		characterSelect.InitCharacterSelect ();
	}

	//close it
	public void CloseCharacterSelectPanel(){
		characterSelectAnimator.SetTrigger ("Out");
		characterSelect.CloseCharacterSelect ();
	}

	//load the settings panel
	public void LoadSettingsPanel(){
		settingsPanelAnimator.SetTrigger ("In");
	}

	//start the game
	public void StartGame(){
		Application.LoadLevel ("LevelSelect");
	}


	//quit the game
	public void QuitGame(){
		Application.Quit ();
	}

	// mute the sound
	public void Mute(){
		if (PlayerPrefs.GetInt ("Mute", 0) == 0) {
			AudioListener.pause = true;
			PlayerPrefs.SetInt ("Mute", 1);
		} else {
			AudioListener.pause = false;
			PlayerPrefs.SetInt ("Mute", 0);
		}
	}
}

