using UnityEngine;
using System.Collections;

public class SFXController : MonoBehaviour {

	public AudioSource audioSource;

	void Start(){
		DontDestroyOnLoad (gameObject);
	}

	public void PlayCoinSound(){
		audioSource.Play ();
	}
}
