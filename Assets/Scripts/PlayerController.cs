using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float pivot;

	public float speed;

	private bool facingRight;

	void Start()
	{
		facingRight = true;

	}

	// Update is called once per frame
	void FixedUpdate () {

		float moveHorizontal = Input.GetAxis ("Horizontal");

		if (moveHorizontal > 0.0f) {
			if (!facingRight) {
				facingRight = true;

			}
		} 
		else {
			if (facingRight) {
				facingRight = false;

			}
		}
		
		Vector2 move_vector = new Vector2 (moveHorizontal,0.0f);

		pivot += moveHorizontal;

		transform.Translate(move_vector * speed);
	}
}
