using UnityEngine;
using System.Collections;

public class TriggerLevelFlip : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.tag == "Player") {
			Camera.main.gameObject.GetComponent<CameraRotate>().rotate = true;
			coll.gameObject.transform.localScale = new Vector3(coll.gameObject.transform.localScale.x * -1,1,1);
		}
	}
}
