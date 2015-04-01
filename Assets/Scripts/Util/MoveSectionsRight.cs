using UnityEngine;
using System.Collections;

public class MoveSectionsRight : MonoBehaviour {

	void Update()
	{
		transform.position = new Vector3(
			transform.position.x + (GameController.gameSpeed * Time.deltaTime),
			transform.position.y,
			transform.position.z
			);
		
		
		
	}
}
