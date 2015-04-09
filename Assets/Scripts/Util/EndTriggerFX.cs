using UnityEngine;
using System.Collections;

public class EndTriggerFX : MonoBehaviour {

	public Animator anim;
	public ParticleSystem coinFX;


	public void EndFX( ){
		anim.SetTrigger("Open");
		coinFX.Play();
	}




}
