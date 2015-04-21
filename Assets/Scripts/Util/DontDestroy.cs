using UnityEngine;
using System.Collections;

public class DontDestroy : MonoBehaviour {

	int levelsLoaded;

	void Awake(){
		DontDestroyOnLoad (gameObject);
	}

	void OnLevelWasLoaded(int level) {
		levelsLoaded++;

		if (level == 0 && levelsLoaded > 1) {
			Destroy(gameObject);
		}
	}
}
