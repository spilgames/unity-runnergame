using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D coll){
		if(coll.gameObject.tag == "Player"){
			KillCoin();
		}
	}

	void KillCoin(){
		SprongData.playerCoins ++;
		SprongData.SavePlayerData ();
		gameObject.SetActive (false);
		PoolingController.PlayCoinFX (transform.position);
	}
}
