using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/*11/6/19 Please change jump function from fixedupdate to update function to fix the blur when jumpning*/

public class Player_Controller : MonoBehaviour {
	//Start() variables
	[SerializeField] public Rigidbody2D rbPlayer;
	protected Animator anim;
	protected CircleCollider2D collide_ground;

	//Animation 
	[SerializeField] public enum state{idle, running, jumping, falling, hurt};
	[SerializeField] public state playerState = state.idle;

	//INSPECTOR VARIABLES
	//for layer ground to use for collision animation plays
	[SerializeField] private LayerMask ground;
	[SerializeField] private LayerMask props;
	//player speed
	[SerializeField]  public float speed;
	//player jump force
	[SerializeField]  public float jmpForce;
	//player hurt
	[SerializeField] public float hurtForce = 10f;

	//Audio 
	[SerializeField] private AudioSource footStep;
	[SerializeField] AudioClip jumping_sounds;
	[SerializeField] AudioClip hurt_Sounds;
	[SerializeField] private AudioSource soundAffects;

	//double jump boolean
	[SerializeField] protected bool canDoubleJump;
 	
	[SerializeField] public float horizontal;
	[SerializeField] protected Vector2 position;

	private void Awake(){
		transform.position = new Vector2 (0,0);
		transform.localScale = new Vector2 (1, 1);

	}

	// Use this for initialization
		private void Start(){
		rbPlayer = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		collide_ground = GetComponent<CircleCollider2D> ();
	}
	/*Run once,zero, or several times per second depending on number of physics
	  frames per seconds are set in the time, settings,
	  and how fast/slow the framerate is*/
	private void FixedUpdate () {

		if(playerState != state.hurt){
			//function for player movement
			Movement ();
		}

		//switch animation state.
		state_Switch ();

		//change state based on defined enumerator.
		anim.SetInteger ("State", (int)playerState);
	
	}
	// Update is called once per frame
	private void Update(){
			Jump ();
	}

	private void FootStep(){
			footStep.Play ();
	}


	private void FootStepStop(){
		footStep.Stop ();

	}

	private void idle(){
		playerState = state.idle;
	}

	private void OnCollisionEnter2D(Collision2D other){
		Enemy enemy = other.gameObject.GetComponent<Enemy> ();

		if (other.gameObject.tag == "Enemy") {

			if (playerState == state.falling) {

				enemy.isJumpedOn ();
				Jump ();

			} else {
				//Debug.Log(state.hurt);
				playerState = state.hurt;
				soundAffects.PlayOneShot (hurt_Sounds);
				if (other.gameObject.transform.position.x > transform.position.x) {
					//left
					rbPlayer.velocity = new Vector2 (-hurtForce, rbPlayer.velocity.y);

				} else {
					//right
					rbPlayer.velocity = new Vector2 (hurtForce, rbPlayer.velocity.y);
				}

			}

		} 

	}


	//Move functions move character from user input.
	private void Movement(){
		
	    horizontal = Input.GetAxis("Horizontal");
		//Debug.Log(rbPlayer.velocity.x);
	    position = transform.position;

		if (horizontal > 0) {
			transform.localScale = new Vector2 (1, 1);
			
		} else if (horizontal < 0) {
			transform.localScale = new Vector2 (-1, 1);
		}
		/*
		else if (Mathf.Abs (rbPlayer.velocity.x) == 0) {
			//Debug.Log ("Stop Moving");
		} 
		*/

		position.x = position.x + speed * horizontal * Time.deltaTime;
		transform.position = position;
	}

	public void Jump(){
		if (Input.GetButtonDown ("Jump")) {
			if(collide_ground.IsTouchingLayers(ground)){
				soundAffects.PlayOneShot (jumping_sounds);
				canDoubleJump = true;
				rbPlayer.velocity = new Vector2 (rbPlayer.velocity.x, jmpForce);
				playerState = state.jumping;
			}
			else{
				if (canDoubleJump) {
					soundAffects.PlayOneShot (jumping_sounds);
					canDoubleJump = false;
					rbPlayer.velocity = new Vector2 (rbPlayer.velocity.x, jmpForce);
					playerState = state.jumping;
				} 
			}
		}

	}


	//this function uses the enum state to switch state to switch animation according to the velocity of the player or direction
	private void state_Switch(){
		//Debug.Log ("Velocity: " + rbPlayer.velocity.y);
		//Debug.Log ("state  switch: " + playerState);
		//Player idle
		if (Mathf.Abs (horizontal) == 0 && Mathf.Abs (Input.GetAxis ("Horizontal")) == 0 && collide_ground.IsTouchingLayers (ground)) {
			//slope
			if (Mathf.Abs (rbPlayer.velocity.x) > 0 && Mathf.Abs (horizontal) == 0 && collide_ground.IsTouchingLayers (ground)) {
				playerState = state.running;
			} else {
				playerState = state.idle;
			}
		}
		//Player running
		else if (Mathf.Abs (horizontal) > 0 && Mathf.Abs (Input.GetAxis ("Horizontal")) > 0 && collide_ground.IsTouchingLayers (ground)) {
			playerState = state.running;
		}
		//player jummping
		else if (rbPlayer.velocity.y > 0 && playerState == state.idle || playerState == state.running) {
			playerState = state.jumping;
		}
		else if(playerState == state.hurt){
			playerState = state.hurt;
		}

		//player falling
		else if (rbPlayer.velocity.y < 0 && !collide_ground.IsTouchingLayers (ground)) {
			playerState = state.falling;
		} 

	}
}
