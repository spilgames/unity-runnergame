using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class SetTextColor : MonoBehaviour {

	public Color w1,w2,w3;

	// Use this for initialization
	void Start () {
		if(Application.loadedLevelName.Contains("W01")){
			GetComponent<Text>().color = w1;
		}
		if(Application.loadedLevelName.Contains("W02")){
			GetComponent<Text>().color = w2;
		}
		if(Application.loadedLevelName.Contains("W03")){
			GetComponent<Text>().color = w3;
		}
	}
	

}
