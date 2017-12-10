using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public Animator animator;

	public float speed;

	private bool facingRight;

	private const int PLAYER_IDLE = 0;
	private const int PLAYER_WALKING = 1;

	private int gameState = PLAYER_IDLE;

	void TurnAround()
	{
		Vector3 theScale = transform.localScale;

		theScale.x *= -1;

		transform.localScale = theScale;
	}

	void Move()
	{
		float move= Input.GetAxis ("Horizontal");

		if (move > 0.0f) {
			if (!facingRight) {
				facingRight = true;
				TurnAround ();
			}
		} else if (move < 0.0f) {
			if (facingRight) {
				facingRight = false;
				TurnAround ();
			}
		}

		Vector2 move_vector = new Vector2 (move, 0.0f);

		transform.Translate (move_vector * speed);
	}

	void SetPlayerState(int state)
	{
		switch (state) {
		case PLAYER_IDLE:
			animator.Play ("Idle");
			break;
		case PLAYER_WALKING:
			animator.Play ("Walking");
			break;
		}

		gameState = state;
	}

	void Start()
	{
		facingRight = false;

		animator = GetComponent<Animator> ();

	}

	void Update()
	{
		if (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.D)) {
			SetPlayerState (PLAYER_WALKING);
		} else
			SetPlayerState (PLAYER_IDLE);
	}

	// Update is called once per frame
	void FixedUpdate () {
		
		switch (gameState) {
		case PLAYER_IDLE:
			break;
		case PLAYER_WALKING:
			Move ();
			break;
		}

	}
		
}

