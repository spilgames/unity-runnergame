using UnityEngine;
using System.Collections;

public class DrawWaterLine : MonoBehaviour {

	public float waterOffset;

	public float topOfScreenOffset;
	
	// Update is called once per frame
	void OnDrawGizmos(){
		Gizmos.color = Color.cyan;
		Gizmos.DrawLine (new Vector2(transform.position.x, transform.position.y + waterOffset),new Vector2(transform.position.x + 200, transform.position.y + waterOffset));

		Gizmos.color = Color.red;
		Gizmos.DrawLine (new Vector2(transform.position.x, transform.position.y + topOfScreenOffset),new Vector2(transform.position.x + 200, transform.position.y + topOfScreenOffset));
	}
}
