using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class LevelSelectController : MonoBehaviour {

	public Sprite avalibleSprite;
	public Sprite unAvalibleSprite;

	int[] levelUnlockStatus = new int[3];
	public Image[] buttons;

	void Start(){

		PlayerPrefs.SetInt ("level1",1);

		for(int i = 0;i < levelUnlockStatus.Length; i++){
			levelUnlockStatus[i] = PlayerPrefs.GetInt("level" + (i + 1).ToString(),0);
			if(levelUnlockStatus[i] == 1){
				buttons[i].sprite = avalibleSprite;
			}else{
				buttons[i].sprite = unAvalibleSprite;
			}
		}

	}


	public void LoadLevel(int level){
		if (levelUnlockStatus [level - 2] == 1) {
			Application.LoadLevel (level);
		}
	}
}
