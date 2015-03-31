using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class FitToGrid : MonoBehaviour {

	// Use this for initialization
	void Start () {
		#if !UNITY_EDITOR
		this.enabled = false;
		#endif
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 pos = transform.position;
		
		pos.x = Mathf.Round( pos.x * 2 ) / 2;
		pos.y = Mathf.Round( pos.y * 2 ) / 2;
		pos.z = Mathf.Round( pos.z * 2 ) / 2;
		
		transform.position = pos;

	}
}
