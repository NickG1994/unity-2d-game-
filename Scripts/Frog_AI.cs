using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog_AI : Enemy {

	[SerializeField]private bool turn = true;

	[SerializeField]private float leftCap;
	[SerializeField]private float RightCap;

	[SerializeField] private LayerMask ground;
	[SerializeField] private Collider2D col;

	[SerializeField]private float jmpLength;
	[SerializeField]private float jumpHeight;

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		col = GetComponent<Collider2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		//transition jump -> fall
		if(anim.GetBool("isJumping")){
			if(rbEnemy.velocity.y < 0.1f){
				anim.SetBool ("isJumping", false);
				anim.SetBool ("isFalling", true);
			}
		}

		//transition fall -> idle
		if(anim.GetBool("isFalling") && col.IsTouchingLayers(ground)){
			rbEnemy.velocity = new Vector2 (0,0);
			anim.SetBool ("isFalling", false);
			anim.SetBool ("isJumping", false);

		}

	}


	private void frog_move(){
		
		if (turn) {
			if (transform.position.x > leftCap) {
				
				if (transform.localScale.x != 1) {
					transform.localScale = new Vector2 (1, 1);
				} 

				if (col.IsTouchingLayers (ground)) {
					
					rbEnemy.velocity = new Vector2 (-jmpLength, jumpHeight);
					anim.SetBool ("isJumping", true);

				} else {
					//print ("inside stop");
					rbEnemy.velocity = new Vector2 (0, 0);	
				}
			} else {
				turn = false;
			}

		} else {
			
			if (transform.position.x < RightCap) {
				if (transform.localScale.x != -1) {
					
					transform.localScale = new Vector2 (-1, 1);

				} 

				if (col.IsTouchingLayers (ground)) {
					
					rbEnemy.velocity = new Vector2 (jmpLength, jumpHeight);
					anim.SetBool ("isJumping", true);
				
				}
				
			} else {
				turn = true;
			}
		}

	}

	/*
	private void OnCollisionEnter2D(Collision2D coll){
		
		if (coll.gameObject.CompareTag ("Player") && turn) {

			print ("inside enemy collider " + turn);
			//frog_move ();
			rbEnemy.velocity = new Vector2 (-jmpLength, jumpHeight);
			anim.SetBool ("isJumping", true);
			turn = true;


		}else if(coll.gameObject.CompareTag ("Player") && !turn){
			//print ("inside enemy else: " + turn);
			//frog_move ();
			rbEnemy.velocity = new Vector2 (jmpLength, jumpHeight);
			anim.SetBool ("isJumping", true);
			turn = false;
		}



	}
	*/

}
