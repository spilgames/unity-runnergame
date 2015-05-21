using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {

	public SpriteRenderer spriteRenderer;

	void OnTriggerEnter2D(Collider2D coll){
		if(coll.gameObject.tag == "Player"){
			KillCoin();
		}
	}

	void KillCoin(){
		GameDirectory.gameController.coinsCollectedThisRun ++;
		PoolingController.PlayCoinFX (transform.position);
		gameObject.SetActive (false);
	}
}
