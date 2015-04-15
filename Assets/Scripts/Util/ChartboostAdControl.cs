using UnityEngine;
using System.Collections;
using ChartboostSDK;
public class ChartboostAdControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Chartboost.cacheInterstitial (CBLocation.GameOver);
	}
	

}
