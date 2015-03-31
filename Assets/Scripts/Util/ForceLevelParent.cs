using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class ForceLevelParent : MonoBehaviour {

	// Use this for initialization
	void Start () {
#if UNITY_EDITOR
		transform.parent = GameObject.Find ("Level").transform;
#endif
	}

}
