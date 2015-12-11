using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {
	
	void OnTriggerEnter2D(Collider2D coll){
		if(coll.gameObject.tag == "Player"){
			KillCoin();
		}
	}

	void KillCoin(){
		GameDirectory.gameController.coinsCollectedThisRun ++;
		GameDirectory.gameController.sfx.PlayCoinSound ();
		PoolingController.PlayCoinFX (transform.position);
		gameObject.SetActive (false);
	}
}
