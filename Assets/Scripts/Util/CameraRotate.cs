using UnityEngine;
using System.Collections;

public class CameraRotate : MonoBehaviour {

	public float rotateSpeed;

	public bool rotate = false;

	Vector3 targetRotation;

	float targetZ;

	bool up = true;

	void Update(){
		if (rotate) {
			rotate = false;
			if(up){
				up = false;
				targetRotation = new Vector3(0,0,180);
			}else{
				up = true;
				targetRotation = Vector3.zero;
			}
		}
		float step = rotateSpeed * Time.deltaTime;
		transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(targetRotation), step);
	}
}
