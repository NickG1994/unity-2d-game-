using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player_Collectables : Player_Controller {
	//variable for cherries
	[SerializeField] private int cherries = 0;
	[SerializeField] private int gem = 0;
	//cherry text
	[SerializeField] private TextMeshProUGUI cherries_Text;

	[SerializeField] private TextMeshProUGUI gem_Text;

	[SerializeField] protected AudioClip cherries_sounds;
	[SerializeField] private AudioSource soundCollectables;
	[SerializeField] protected AudioClip gemSound;

	private void Start(){
		soundCollectables = GetComponent<AudioSource> ();
		anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator> ();
	}
	
	private void OnTriggerEnter2D (Collider2D collision){
		if (collision.tag == "Cherries") {

			Destroy (collision.gameObject);
			cherries += 1;
			cherries_Text.text = cherries.ToString ();
			soundCollectables.PlayOneShot (cherries_sounds);

		} else if(collision.tag == "Gems"){

			Destroy (collision.gameObject);
			gem += 1;
			gem_Text.text = gem.ToString ();
			soundCollectables.PlayOneShot (gemSound);
		}

	}

	private void OnCollisionEnter2D(Collision2D col){
		if(col.gameObject.tag == "Enemy"){

		}
	}


}
