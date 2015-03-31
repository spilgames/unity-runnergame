using UnityEngine;
using System.Collections;

public class GameDirectory : MonoBehaviour {

	public static GameController gameController;





	void Start(){

		gameController = GameObject.Find ("GameController").GetComponent<GameController> ();

	}
}
