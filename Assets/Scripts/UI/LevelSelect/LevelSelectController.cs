using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class LevelSelectController : MonoBehaviour {

	//the panel animators
	public Animator[] panelAnims;


	//the ui panels
	public GameObject levelInfoPanel;


	bool backToMain;


	void OnEnable(){
		backToMain = false;
		SprongData.LoadPlayerData ();
	}

	//open the level info panel
	public void ToggleLevelInfo(){
		if (levelInfoPanel.activeInHierarchy) {
			levelInfoPanel.SetActive (false);
		} else {
			levelInfoPanel.SetActive(true);
		}
	}

	public void BackToTitleScreen(){
		StopCoroutine ("BackToMainLevel");
		StartCoroutine ("BackToMainLevel");
	}

	IEnumerator BackToMainLevel(){
		SlideOutPanels ();
		yield return new WaitForSeconds (0.2f);
		GetComponent<TitleUIController> ().mainPanelAnim.SetTrigger ("In");
	}


	//slide the panels out
	public void SlideOutPanels(){
		for(int i = 0; i < panelAnims.Length; i ++){
			panelAnims[i].SetTrigger("Out");
		}
	}

	//slide the panels in
	public void SlideInPanels(){
		for(int i = 0; i < panelAnims.Length; i ++){
			panelAnims[i].SetTrigger("In");
		}
	}



}
