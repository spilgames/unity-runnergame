using UnityEngine;
using System.Collections;

public class LevelSelectController : MonoBehaviour {

	public void LoadLevel(int level){
		Application.LoadLevel (level);
	}
}
