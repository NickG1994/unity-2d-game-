using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	protected Animator anim;
	[SerializeField] protected Rigidbody2D rbEnemy;

	[SerializeField] AudioClip death_Sound;
	[SerializeField] AudioSource enemySounds;

	// Use this for initialization
	protected virtual void Start () {
		anim = GetComponent<Animator> ();
		rbEnemy = GetComponent<Rigidbody2D> ();
		enemySounds = GetComponent<AudioSource> ();
	}


	public void isJumpedOn(){
		Debug.Log ("Frogs is dead");
		rbEnemy.velocity = new Vector2 (0,0);
		anim.SetTrigger ("Death");
		rbEnemy.bodyType = RigidbodyType2D.Static;
		GetComponent<Collider2D> ().enabled = false;
		enemySounds.PlayOneShot (death_Sound);
	}

	public void Death(){
		Destroy (this.gameObject);
	}

}
