using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class LevelButtonController : MonoBehaviour {

	//which level is this button for
	public int level;

	//the sprite for when the level is unlocked
	public Sprite unlockedSprite;
	//the locked level sprite	
	public Sprite lockedSprite;
	//the sprites for ulockable stars from none to 3
	public Sprite[] starSprites;

	//the image components of the buttons
	public Image buttonImage;
	public Image starsImage;

	// Use this for initialization
	void Start () {
		WorkOutButtonImage ();
	}

	//what to do if the button is clicked
	public void ButtonClicked(){
		if (SprongData.levelsUnlocked [level - 1]) {
			SprongData.level = level;
			SprongData.SavePlayerData();
			GetComponentInParent<LevelSelectController>().ToggleLevelInfo();
		}
	}

	void WorkOutButtonImage(){
		Debug.Log (gameObject.name + "   " + SprongData.levelsUnlocked [level - 1]);
		if (SprongData.levelsUnlocked [level - 1]) {
			buttonImage.sprite = unlockedSprite;
			switch (PlayerPrefs.GetInt ("stars" + level, 0)) {
			case 0:
				starsImage.sprite = starSprites [0];
				break;
			case 1:
				starsImage.sprite = starSprites [1];
				break;
			case 2:
				starsImage.sprite = starSprites [2];
				break;
			case 3:
				starsImage.sprite = starSprites [3];
				break;
			}
		} else {
			buttonImage.sprite = lockedSprite;
			starsImage.enabled = false;
		}
	}
}
