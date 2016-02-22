using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
public class CharacterSelectController : MonoBehaviour {

	bool wasMuted;

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
		if (SprongData.muteMusic == 1) {
			wasMuted = true;
		}
		rewardedButton.SetActive (true);
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
		if(!wasMuted){
			GameObject.Find("Music").GetComponent<AudioSource>().mute = false;
		}
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
			if (!SprongData.charactersUnlocked [i] && SprongData.playerCoins >= i * SprongData.characterCostModifier) {
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
		GameObject.Find("Music").GetComponent<AudioSource>().mute = true;
		Spil.ShowRewardedVideo ();
	}

	//unlock a character
	public void UnlockCharacter(int characterNumber){
		int cost = characterNumber * SprongData.characterCostModifier;
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
		int cost = characterToUnlock * SprongData.characterCostModifier;

		Dictionary<string,string> eventDetails = new Dictionary<string, string> ();
		eventDetails.Add ("walletValue",SprongData.playerCoins.ToString());
		eventDetails.Add ("itemValue",cost.ToString());
		eventDetails.Add ("source","0");
		eventDetails.Add ("item","inGameCoins");
		eventDetails.Add ("category","0");
		Spil.TrackEvent ("walletUpdate", eventDetails);

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
}
