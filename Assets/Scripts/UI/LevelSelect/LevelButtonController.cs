using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class LevelButtonController : MonoBehaviour {

	public int level;

	public Sprite unlockedSprite;

	public Sprite lockedSprite;

	public Sprite[] starSprites;

	public Image buttonImage;
	public Image starsImage;

	// Use this for initialization
	void Start () {
		WorkOutButtonImage ();
	}


	public void ButtonClicked(){

	}

	void WorkOutButtonImage(){
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
