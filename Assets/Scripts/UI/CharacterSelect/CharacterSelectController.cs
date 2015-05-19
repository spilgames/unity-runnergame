using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Advertisements;
public class CharacterSelectController : MonoBehaviour {

	//unity ads ids
	public string unityAdsIos;
	public string unityAdsAndroid;

	//the animal buttons 
	public Image[] buttons;

	//the GAV3 prefab
	public GoogleAnalyticsV3 analytics;

	//black out color for locked animals
	public Color blackOut;
	public Color fullColor;

	//the rewarded video button
	public GameObject rewardedButton;

	//the characterlockedpanel
	public GameObject characterLockedPanel;
	public GameObject RewardedPanel;
	public GameObject RewardedSuccessPanel;

	//what character did they try to use?
	int lockedCharacterNumber;
	
	// Use this for initialization
	void OnEnable () {
		#if UNITY_ANDROID
		Advertisement.Initialize (unityAdsAndroid);
		#endif
		#if UNITY_IOS
		Advertisement.Initialize (unityAdsIos);
		#endif
		//load the status of unlocks so far
		SprongData.LoadPlayerData ();
		//make sure that at least the fist character is unlocked
		SprongData.charactersUnlocked [0] = true;
		//update the character images accordingly
		UpdateButtonImages ();
		//check to see if there are any ads avalible
		StartCoroutine ("CheckForRewardedAd");
	}

	//reset stuff 
	void OnDisable(){
		rewardedButton.SetActive(false);
		characterLockedPanel.SetActive (false);
		StopCoroutine ("CheckForRewardedAd");
	}

	//cheats
	void Update(){
		if (Input.GetKeyDown (KeyCode.C)) {
			SprongData.playerCoins += 400;
			SprongData.SavePlayerData();
		}
	}

	//check for a rewarded video
	IEnumerator CheckForRewardedAd(){
		yield return new WaitForSeconds (4);
		if (Advertisement.isReady ("rewardedVideoZone")) {
			rewardedButton.SetActive(true);
		}
	}

	//update the buttons if they are unlocked
	void UpdateButtonImages(){
		for (int i = 0; i < SprongData.charactersUnlocked.Length; i ++) {
			if(SprongData.charactersUnlocked[i]){
				buttons[i].color = fullColor;
			}else{
				buttons[i].color = blackOut;
			}
		}
	}

	//if a character is locked, show the locked character screen
	public void ShowLockedCharacterScreen(int character){
		lockedCharacterNumber = character;
		characterLockedPanel.SetActive (true);
	}

	//close it after
	public void CloseLockedCharactersScreen(){
		characterLockedPanel.SetActive (false);
	}

	//offer a reward for watching an AD
	public void TriggerRewardedOfferPanel(){
		if (RewardedPanel.activeInHierarchy) {
			RewardedPanel.SetActive (false);
		} else {
			RewardedPanel.SetActive(true);
		}
	}

	//play the AD
	public void WatchVideo(){
		Advertisement.Show("rewardedVideoZone", new ShowOptions {
			pause = true,
			resultCallback = result => {
				if(result == ShowResult.Finished){
					RewardPlayer();
				}
			}
		});
	}

	//unlock a character
	public void UnlockCharacter(){
		//if the player has enough coins
		if (SprongData.playerCoins >= 400) {
			//deduct the coins
			SprongData.playerCoins -= 400;
			//unlock the character
			SprongData.charactersUnlocked[lockedCharacterNumber] = true;
			//save the change
			SprongData.SavePlayerData();
			//close the panel
			CloseLockedCharactersScreen();
			UpdateButtonImages();
		}
	}

	//select the character, and if unlocked start the game with that character
	public void StartGame(int character){
		if (SprongData.charactersUnlocked [character]) {
			SprongData.characterSelected = character;
			SprongData.SavePlayerData();
			Application.LoadLevel (PlayerPrefs.GetInt ("level", 1));
		} else {
			//if not unlocked, show the unlock character screen
			ShowLockedCharacterScreen(character);
		}
	}

	//reward the player after watching an AD
	public void RewardPlayer(){
		RewardedSuccessPanel.SetActive(true);
		SprongData.playerCoins += 300;
		RewardedPanel.SetActive (false);
		rewardedButton.SetActive (false);
		StartCoroutine ("CheckForRewardedAd");
		SprongData.SavePlayerData ();
	}

	public void CloseRewardedSuccessPanel(){
		RewardedSuccessPanel.SetActive(false);
	}
}
