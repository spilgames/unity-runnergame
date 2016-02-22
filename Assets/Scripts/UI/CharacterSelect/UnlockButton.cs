using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class UnlockButton : MonoBehaviour {


	public int characterNumber;

	Text costText;

	void Start(){
		costText = GetComponentInChildren<Text> ();
		costText.text = (characterNumber * SprongData.characterCostModifier).ToString ();
	}




	
	// Update is called once per frame
	void Update () {
	
	}
}
