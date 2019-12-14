using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player_Health : MonoBehaviour {
	[SerializeField] private int life;
	[SerializeField] private TextMeshProUGUI lives;

	[SerializeField] private int Health;
	[SerializeField] private int numOfHealth;

	[SerializeField] private Image[] hearts;
	[SerializeField] private Sprite fullHeart;
	[SerializeField] private Sprite emptyHearts;

	void Start(){
		//life = 5;
		lives.text = life.ToString ();
	}

	void Update (){
		PlayerHealth ();
	}

	private void PlayerHealth(){
		for (int i = 0; i < hearts.Length; i++) {

			if (i < Health) {
				hearts [i].sprite = fullHeart;
			} else {
				hearts [i].sprite = emptyHearts;
			}

			if (i < numOfHealth) {
				hearts[i].enabled = true;
			} else {
				hearts[i].enabled = false;
			}


		}
	}

	private void OnCollisionEnter2D(Collision2D other){
		if(other.gameObject.tag == "Enemy"){
			if (numOfHealth > 1) {
				numOfHealth -= 1;
			} else{
				numOfHealth = 5;
				life -= 1;
				lives.text = life.ToString ();
			}
			/*
			if(lives > 0){
				lives -= 1;
				Player_Lives.text = lives.ToString ();
			}
			*/
		}
	}

	private void PlayerDeath(){
		
	}


}
