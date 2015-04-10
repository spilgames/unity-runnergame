using UnityEngine;
using System.Collections;

public class MoveSectionsRight : MonoBehaviour {
	
	void Update()
	{
		Vector3 target = new Vector3(
			transform.position.x + (GameController.gameSpeed * Time.smoothDeltaTime),
			transform.position.y,
			transform.position.z
			);
		
		transform.position = Vector3.Lerp (transform.position,target,1);
		
	}
}
