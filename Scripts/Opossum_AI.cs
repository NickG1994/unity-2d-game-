using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opossum_AI : Enemy {
	
	public int EnemeySpeed;
	public Vector2 xDirection = new Vector2(1,0);
	public float hitDistance = .5f;

	[SerializeField] private Transform GroundDetection;
	[SerializeField] private Transform Player;
	[SerializeField] private Rigidbody2D rbPlayer;
	// Use this for initialization
	protected override void Start () {
		base.Start ();
		rbEnemy = GetComponent<Rigidbody2D> ();
		Player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform>();
		rbPlayer = GameObject.FindGameObjectWithTag ("Player").GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	 void Update (){ 
		/*
		if (Vector2.Distance (transform.position, Player.position) < 5) {
			float speed = 4 * Time.deltaTime;


			if (rbEnemy.velocity.x > 0) {
				transform.localScale = new Vector2 (1, 1);
			} else if(rbEnemy.velocity.x < 0){
				transform.localScale = new Vector2 (-1,1);
			}
			transform.position = Vector2.MoveTowards (transform.position, Player.position, speed);
		} else {
			//Debug.Log ("Not Close");
		}
		*/
		Debug.DrawRay (GroundDetection.position,xDirection * 2f);
		RaycastHit2D hit = Physics2D.Raycast (GroundDetection.position, xDirection,  hitDistance);

		rbEnemy.velocity = new Vector2 (EnemeySpeed, 0);

		if(hit == true){
			if (hit.collider.CompareTag ("Ground") || hit.collider.CompareTag ("Enemy")) {
				Flip ();
				xDirection *= -1;
				EnemeySpeed *= -1;
				//print ("Collider hit Props");
			} else if(hit.collider.CompareTag ("Player")){
				if (Player.gameObject.transform.position.x > transform.position.x) {

					//left
					rbPlayer.velocity = new Vector2 (-15, rbPlayer.velocity.y);
					//print ("To my left");

				} else {
					//right
					rbPlayer.velocity = new Vector2 (15, rbPlayer.velocity.y);
					//print ("To my right :" + hurtForce);
				}
			}
		}



	}

	void Flip(){
		Vector2 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}


}
