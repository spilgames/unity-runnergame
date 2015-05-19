using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class LazyCoinScript : MonoBehaviour {

	Text coinText;

	// Use this for initialization
	void Start () {
		coinText = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		coinText.text = SprongData.playerCoins.ToString ();
	}
}
