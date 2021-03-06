﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	//the game controller
	public GameController gameController;
	//what physics layer is the ground
	public LayerMask groundLayers;
	//is the player grounded
	public bool grounded;
	//grab the players rigidbody
	public Rigidbody2D playerBody;
	//is the player dead
	bool dead = false;
	//grab the animator
	public Animator anim;
	//the players collider
	public CircleCollider2D playerCollider;

	//complex jump shit
	public float downforce;
	public float minimumjumpduration;
	public float maximumJumpDuration;
	public float JumpSpeed;
	public bool isJumping = false;
	public float jumpStart = -1;
	private bool isKeyUpHappened = false;

	public AudioSource hitSound;
	public AudioSource jumpSound;

	//initialise the player
	void Start(){
		playerCollider.enabled = true;
		anim.SetInteger ("Animal",SprongData.characterSelected);

	}


	
	void Update(){
		if (dead) {
			return;
		}


		//check for ground
		grounded = (Physics2D.OverlapCircle (new Vector2 (transform.position.x, transform.position.y - 0.9f), 0.09f, groundLayers)
		            ||
		            Physics2D.OverlapCircle (new Vector2 (transform.position.x-0.9f, transform.position.y - 0.9f), 0.09f, groundLayers)
		            );
		//tell the animator
		anim.SetBool ("Grounded", grounded);

		//if the minimum jump duration has passed
		if (jumpStart + minimumjumpduration <= Time.time && !grounded) {
			//if the player has released the button, push the player down
			if (isKeyUpHappened) {
				playerBody.AddForce (new Vector2 (0, -downforce),ForceMode2D.Impulse);
			}
			//otherwise push the player down after max jump duration
			else if (jumpStart + maximumJumpDuration <= Time.time){
				playerBody.AddForce (new Vector2 (0, -downforce),ForceMode2D.Impulse);
			}
		}

		if(Input.GetKeyDown(KeyCode.Space)){
			Jump();
		}

		if (Input.GetKeyUp (KeyCode.Space)) {
			JumpButtonUp();
		}
		if (!dead && transform.position.x < gameController.playerStartPosition.position.x - 0.2f ) {
			Die(false);
		}
	}

	public void Jump(){
		if (grounded) {
			if (SprongData.muteSFX == 0) {
				jumpSound.Play();
			}
			playerBody.AddForce (Vector3.up * JumpSpeed,ForceMode2D.Impulse);
			isJumping = true;
			isKeyUpHappened = false;
			jumpStart = Time.time;
		}
	}
	public void JumpButtonUp(){
		if (isJumping) {
			isKeyUpHappened = true;
		}
	}

	//reset the player
	public void ResetPlayer(){
		anim.SetInteger ("Animal",SprongData.characterSelected);
		anim.SetTrigger ("Reset");
		transform.rotation = gameController.playerStartPosition.rotation;
		playerBody.fixedAngle = true;
		playerBody.drag = 0;
		playerBody.gravityScale = 1;
		playerCollider.enabled = true;
		playerBody.velocity = Vector2.zero;
		dead = false;
		transform.position = gameController.playerStartPosition.position;
		anim.SetBool ("Death",false);
	}

	//detect obsticles and die
	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.tag == "Obsticle" && !dead) {
			if(coll.gameObject.name == "sea"){
				Die (true);
			}else{
				Die (false);
			}
		}
		if (coll.gameObject.name == "EndTrigger") {
			gameController.EndGame(false);
			dead = true;
			playerBody.gravityScale = 0;
			playerBody.fixedAngle = false;
			playerBody.AddTorque (-800);
			playerBody.AddForce (new Vector2 (15, 50), ForceMode2D.Impulse);
//			coll.gameObject.GetComponent<EndTriggerFX>().EndFX();
		}
	}

	//die
	void Die(bool water){
		dead = true;
		anim.SetBool ("Death",true);
		playerBody.fixedAngle = false;
		if (SprongData.muteSFX == 0) {
			hitSound.Play ();
		}
		if (!water) {
			playerBody.velocity = Vector2.zero;
			if (grounded) {
				playerBody.AddForce (new Vector2 (15, 50), ForceMode2D.Impulse);
			} else {
				playerBody.AddForce (new Vector2 (15, 20), ForceMode2D.Impulse);
			}
		}
		playerBody.AddTorque (-200);
		playerBody.drag = 1;
		playerCollider.enabled = false;
		gameController.playerDistance = transform.position.x;
		gameController.EndGame (true);
	}

}
