using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Advertisements;
public class CharacterSelectController : MonoBehaviour {
	
	//the animal buttons 
	public Image[] buttons;

	//the unlock character buttons
	public GameObject[] unlockButtons;
	int characterToUnlock;

	//the character spotlights
	public Image[] spotlights;

	//the confirm panels
	public GameObject[] confirmPanels;

	//black out color for locked animals
	public Color blackOut;
	public Color fullColor;

	//the rewarded video button
	public GameObject rewardedButton;

	//the reward success panel
	public GameObject RewardedSuccessPanel;

	//the coins text
	public Text coinsText;

	//celebration particles
	public ParticleSystem celebrationFX;

	//notification stuff
	public Text characternotificationNumber;
	public GameObject characternotificationObject;

	void Start(){
		CheckForCharacterNotification ();
	}

	// Use this for initialization
	public void InitCharacterSelect () {
		CheckForCharacterNotification ();
		SetSpotlight ();
		coinsText.text = SprongData.playerCoins.ToString ();
		//update the character images accordingly
		UpdateButtonImages ();
		//check to see if there are any ads avalible
		StartCoroutine ("CheckForRewardedAd");
	}

	//reset stuff 
	public void CloseCharacterSelect(){
		CheckForCharacterNotification ();
		rewardedButton.SetActive(false);
		StopCoroutine ("CheckForRewardedAd");
	}

	//set the correct spotlight
	public void SetSpotlight(){
		for (int i = 0; i < spotlights.Length; i++) {
			if(i == SprongData.characterSelected){
				spotlights[i].color = Color.white;
			}else{
				spotlights[i].color = new Color(1,1,1,0);
			}
		}
	}

	//cheats
	void Update(){
		coinsText.text = SprongData.playerCoins.ToString ();
		if (Input.GetKeyDown (KeyCode.C)) {
			SprongData.playerCoins += 500;
			SprongData.SavePlayerData();

		}

	}

	//handle the notification thing
	void CheckForCharacterNotification(){
		int amount = 0;
		for (int i = 0; i < SprongData.charactersUnlocked.Length; i ++) {
			if (!SprongData.charactersUnlocked [i] && SprongData.playerCoins >= i * 100) {
				characternotificationObject.SetActive (true);
				amount ++;
			}
		}
		if (amount == 0) {
			characternotificationObject.SetActive (false);
		} else {
			characternotificationNumber.text = amount.ToString ();
		}
	}
	//check for a rewarded video
	IEnumerator CheckForRewardedAd(){
		yield return new WaitForSeconds (1);
		if (Advertisement.isReady ("rewardedVideoZone")) {
			rewardedButton.SetActive (true);
		} else {
			StartCoroutine("CheckForRewardedAd");
		}
	}

	//update the buttons if they are unlocked
	void UpdateButtonImages(){
		for (int i = 0; i < SprongData.charactersUnlocked.Length; i ++) {
			if(SprongData.charactersUnlocked[i]){
				buttons[i].color = fullColor;
				unlockButtons[i].SetActive(false);
			}else{
				buttons[i].color = blackOut;
				unlockButtons[i].SetActive(true);
			}
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
	public void UnlockCharacter(int characterNumber){
		int cost = characterNumber * 100;
		//if the player has enough coins
		if (SprongData.playerCoins >= cost) {
			characterToUnlock = characterNumber;
			ShowConfirm();
		}
	}

	//show the confirm purchase prompt
	void ShowConfirm(){
		for (int i = 0; i < confirmPanels.Length; i++) {
			confirmPanels[i].SetActive(false);
		}
		confirmPanels [characterToUnlock].SetActive (true);
	}

	//close the prompts
	public void CloseConfirm(){
		for (int i = 0; i < confirmPanels.Length; i ++) {
			confirmPanels[i].SetActive(false);
		}
	}

	//confirm and unlock the character
	public void ConfirmUnlock(){
		int cost = characterToUnlock * 100;
		//deduct the coins
		SprongData.playerCoins -= cost;
		//unlock the character
		SprongData.charactersUnlocked[characterToUnlock] = true;
		//save the change
		SprongData.characterSelected = characterToUnlock;
		SprongData.SavePlayerData();
		//close the panel
		UpdateButtonImages();
		celebrationFX.Play();
		CloseConfirm ();
		SetSpotlight ();
	}
	
	
	
	//select a character
	public void SelectCharacter(int character){
		if (SprongData.charactersUnlocked [character]) {
			SprongData.characterSelected = character;
			SprongData.SavePlayerData ();
			SetSpotlight ();
		}
	}

	//reward the player after watching an AD
	public void RewardPlayer(){
		RewardedSuccessPanel.SetActive(true);
		SprongData.playerCoins += 500;
		StartCoroutine ("CheckForRewardedAd");
		SprongData.SavePlayerData ();
	}

	public void CloseRewardedSuccessPanel(){
		RewardedSuccessPanel.SetActive(false);
	}
}
