using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class LevelSelectController : MonoBehaviour {

	public enum LevelSelectStates
	{
		WorldSelect,
		World1,
		World2,
		World3
	}
	public LevelSelectStates levelSelectState;

	//the ui panels
	public GameObject worldsPanel;
	public GameObject world1LevelListPanel;
	public GameObject world2LevelListPanel;
	public GameObject world3LevelListPanel;


	void Start(){
		SprongData.LoadPlayerData ();
		levelSelectState = LevelSelectStates.WorldSelect;
		UpdateUI ();
	}


	//update the UI according to the state
	void UpdateUI(){
		if(levelSelectState == LevelSelectStates.WorldSelect){
			worldsPanel.SetActive(true);
		}else{
			worldsPanel.SetActive(false);
		}
		if(levelSelectState == LevelSelectStates.World1){
			world1LevelListPanel.SetActive(true);
		}else{
			world1LevelListPanel.SetActive(false);
		}
		if(levelSelectState == LevelSelectStates.World2){
			world2LevelListPanel.SetActive(true);
		}else{
			world2LevelListPanel.SetActive(false);
		}
		if(levelSelectState == LevelSelectStates.World3){
			world3LevelListPanel.SetActive(true);
		}else{
			world3LevelListPanel.SetActive(false);
		}
	}

	//open or cloase the different leve llists
	public void ToggleLevelSelect(int level){
		switch (level) {
		case 1:
			if(world1LevelListPanel.activeInHierarchy){
				levelSelectState = LevelSelectStates.WorldSelect;
				UpdateUI();
			}else{
				levelSelectState = LevelSelectStates.World1;
				UpdateUI();
			}
			break;
		case 2:
			if(world2LevelListPanel.activeInHierarchy){
				levelSelectState = LevelSelectStates.WorldSelect;
				UpdateUI();
			}else{
				levelSelectState = LevelSelectStates.World2;
				UpdateUI();
			}
			break;
		case 3:
			if(world3LevelListPanel.activeInHierarchy){
				levelSelectState = LevelSelectStates.WorldSelect;
				UpdateUI();
			}else{
				levelSelectState = LevelSelectStates.World3;
				UpdateUI();
			}
			break;
		default:
			break;
		}
	}


}
