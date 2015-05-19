using UnityEngine;
using System.Collections;

public class TitleUIController : MonoBehaviour {

	public GameObject infoPanel;
	public GameObject optionsPanel;

	public void OpenLevelSelect(){
		Application.LoadLevel ("LevelSelect");
	}

	public void ToggleInfoPanel(){
		if (infoPanel.activeInHierarchy) {
			infoPanel.SetActive (false);
		} else {
			infoPanel.SetActive(true);
		}
	}

	public void ToggleOptionsPanel(){
		if (optionsPanel.activeInHierarchy) {
			optionsPanel.SetActive (false);
		} else {
			optionsPanel.SetActive(true);
		}
	}

	public void QuitGame(){
		Application.Quit ();
	}

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

