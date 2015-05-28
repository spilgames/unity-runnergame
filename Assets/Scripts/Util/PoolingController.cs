using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PoolingController : MonoBehaviour {

	int coinFXToPool = 20;
	public GameObject coinFXobject;
	static List<GameObject> coinFXPool = new List<GameObject>();




	void Start(){
		SpawnPool ();
	}

	void SpawnPool(){
		coinFXPool.Clear ();
		for(int i = 0; i < coinFXToPool; i++){
			GameObject newCoinFX = (GameObject)Instantiate(coinFXobject);
			newCoinFX.SetActive(false);
			coinFXPool.Add(newCoinFX);
		}
	}
	

	public static void PlayCoinFX(Vector2 pos){
		for(int i = 0; i < coinFXPool.Count; i ++){
			if(!coinFXPool[i].activeInHierarchy){
				coinFXPool[i].transform.position = pos;
				coinFXPool[i].SetActive(true);
				coinFXPool[i].GetComponent<ParticleSystem>().Play();
				break;
			}
		}
	}


}
