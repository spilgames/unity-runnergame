using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;
using UnityEngine.UI;
public class TitleUIController : MonoBehaviour {

	//unity ads ids
	public string unityAdsIos;
	public string unityAdsAndroid;

	//the panel animators
	public Animator characterSelectAnimator;
	public Animator settingsPanelAnimator;
	public Animator mainPanelAnim;
	bool settingsOpen;

	//the character select controller
	public CharacterSelectController characterSelect;

	//the mute buttons
	public Image muteMusicButton;
	public Image muteSfxButton;
	public Sprite[] musicSprites;
	public Sprite[] sfxSprites;
	public AudioSource musicSource;

	//the levelslectUIcontroller
	public LevelSelectController levelSelectController;

	void Awake(){
		SprongData.LoadPlayerData ();
		if (SprongData.muteMusic == 1) {
			musicSource.mute = true;
			muteMusicButton.sprite = musicSprites [1];
		} else {
			musicSource.Play();
		}
		if(SprongData.muteSFX == 1){
			muteSfxButton.sprite = sfxSprites[1];
		}
	}

	void Start(){
		InitUnityAds ();
		//load the status of unlocks so far

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
		mainPanelAnim.SetTrigger ("Out");
		characterSelect.InitCharacterSelect ();
		StopCoroutine ("CharacterIn");
		StartCoroutine ("CharacterIn");
	}
	IEnumerator CharacterIn(){
		yield return new WaitForSeconds (0.3f);
		characterSelectAnimator.SetTrigger ("In");
	}

	//close it
	public void CloseCharacterSelectPanel(){
		characterSelectAnimator.SetTrigger ("Out");
		characterSelect.CloseCharacterSelect ();
		StopCoroutine ("CharacterOut");
		StartCoroutine ("CharacterOut");
	}
	IEnumerator CharacterOut(){
		yield return new WaitForSeconds (0.4f);
		mainPanelAnim.SetTrigger ("In");
	}

	//load the settings panel
	public void LoadSettingsPanel(){
		if (settingsOpen) {
			settingsPanelAnimator.SetTrigger ("Out");
			settingsOpen = false;
		}else{
			settingsPanelAnimator.SetTrigger ("In");
			settingsOpen = true;
		}
	}


	//start the game
	public void LoadLevelSelect(){
		StopCoroutine ("FadeOutMainLoad");
		StartCoroutine ("FadeOutMainLoad");
	}

	IEnumerator FadeOutMainLoad(){
		mainPanelAnim.SetTrigger ("Out");
		yield return new WaitForSeconds (0.3f);
		levelSelectController.SlideInPanels ();
	}


	//quit the game
	public void QuitGame(){
		Application.Quit ();
	}

//	// mute the sound
//	public void MuteMusic(){
//		if (SprongData.muteMusic == 0) {
//			musicSource.mute = true;
//			muteMusicButton.sprite = musicSprites[1];
//			SprongData.muteMusic = 1;
//			SprongData.SavePlayerData();
//		} else {
//			musicSource.mute = false;
//			musicSource.Play();
//			muteMusicButton.sprite = musicSprites[0];
//			SprongData.muteMusic = 0;
//			SprongData.SavePlayerData();
//		}
//	}
	public void MuteSFX(){
		if (SprongData.muteMusic == 0) {
			musicSource.mute = true;
			muteMusicButton.sprite = musicSprites[1];
			SprongData.muteMusic = 1;
			SprongData.SavePlayerData();
		} else {
			musicSource.mute = false;
			musicSource.Play();
			muteMusicButton.sprite = musicSprites[0];
			SprongData.muteMusic = 0;
			SprongData.SavePlayerData();
		}
		if (SprongData.muteSFX == 0) {
			muteSfxButton.sprite = sfxSprites[1];
			SprongData.muteSFX = 1;
			SprongData.SavePlayerData();
		} else {
			muteSfxButton.sprite = sfxSprites[0];
			SprongData.muteSFX = 0;
			SprongData.SavePlayerData();
		}
	}
}

