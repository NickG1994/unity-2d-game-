using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : MonoBehaviour {
	private Vector2 velocity;

	public float smoothTimeX;
	public float smoothTimeY;

	public GameObject Player;

	public bool bounds;

	public Vector3 minCameraPos;

	public Vector3 MaxCameraPos;

	// Use this for initialization
	void Start () {
		//Player = GameObject.FindGameObjectWithTag ("Player");
	}

	// Update is called once per frame
	void FixedUpdate () {
		//Camera movement target
		float xPosition = Mathf.SmoothDamp (transform.position.x, Player.transform.position.x, ref velocity.x , smoothTimeX );
		float yPosition = Mathf.SmoothDamp (transform.position.y, Player.transform.position.y, ref velocity.y , smoothTimeY );
		transform.position = new Vector3 (xPosition, yPosition, transform.position.z);

		if(bounds){
			transform.position = new Vector3 (Mathf.Clamp(transform.position.x, minCameraPos.x,MaxCameraPos.x),
				Mathf.Clamp(transform.position.y, minCameraPos.y,MaxCameraPos.y), 
				Mathf.Clamp(transform.position.z, minCameraPos.z, MaxCameraPos.z));
		}

	}

}
