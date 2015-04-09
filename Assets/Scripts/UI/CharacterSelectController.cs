using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Advertisements;
public class CharacterSelectController : MonoBehaviour {

	//the animal buttons 
	public Image[] buttons;

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
		GameController.Load ();
		GameController.charactersUnlocked [0] = true;
		UpdateButtonImages ();
		StartCoroutine ("CheckForRewardedAd");
	}

	void OnDisable(){
		rewardedButton.SetActive(false);
		characterLockedPanel.SetActive (false);
		StopCoroutine ("CheckForRewardedAd");
	}

	IEnumerator CheckForRewardedAd(){
		yield return new WaitForSeconds (4);
		if (Advertisement.isReady ("rewardedVideoZone")) {
			rewardedButton.SetActive(true);
		}
	}

	
	//update the buttons if they are unlocked
	void UpdateButtonImages(){
		for (int i = 0; i < GameController.charactersUnlocked.Length; i ++) {
			if(GameController.charactersUnlocked[i]){
				buttons[i].color = fullColor;
			}else{
				buttons[i].color = blackOut;
			}
		}
	}
	
	public void ShowLockedCharacterScreen(int character){
		lockedCharacterNumber = character;
		characterLockedPanel.SetActive (true);
	}

	public void CloseLockedCharactersScreen(){
		characterLockedPanel.SetActive (false);
	}

	public void TriggerRewardedOfferPanel(){
		if (RewardedPanel.activeInHierarchy) {
			RewardedPanel.SetActive (false);
		} else {
			RewardedPanel.SetActive(true);
		}
	}

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

	public void UnlockCharacter(){
		if (GameController.playerCoins >= 200) {
			GameController.playerCoins -= 200;
			GameController.UpdateCoinsText();
			GameController.charactersUnlocked[lockedCharacterNumber] = true;
			UpdateButtonImages();
			GameController.Save();
			CloseLockedCharactersScreen();
		}
	}

	public void RewardPlayer(){
		RewardedSuccessPanel.SetActive(true);
		GameController.playerCoins += 100;
		PlayerPrefs.SetInt ("coins",GameController.playerCoins);
		GameController.UpdateCoinsText ();
		RewardedPanel.SetActive (false);
		rewardedButton.SetActive (false);
		StartCoroutine ("CheckForRewardedAd");
		GameController.Save ();
	}

	public void CloseRewardedSuccessPanel(){
		RewardedSuccessPanel.SetActive(false);
	}


}
