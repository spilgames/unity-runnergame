using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class LevelInfoPanelController : MonoBehaviour {

	//the 3 stars of the level
	public Image[] starImages;

	void OnEnable(){
		SetupPanel ();
	}

	//fill in the stars and stuff
	void SetupPanel(){
		int level = SprongData.level;
		for(int i = 0; i < starImages.Length; i++){
			starImages[i].color = Color.black;
		}
		for(int i = 0; i < PlayerPrefs.GetInt ("stars" + level, 0); i ++){
			starImages[i].color = Color.yellow;
		}
	}

	// load the character select
	public void LoadCharacterSelect(){
		Application.LoadLevel (PlayerPrefs.GetInt ("level", 1));
	}

}
