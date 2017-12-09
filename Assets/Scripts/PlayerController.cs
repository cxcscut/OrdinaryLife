using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerController : MonoBehaviour {

	public Animator animator;

	public float speed;

	private bool facingRight;

	void TurnAround()
	{
		Vector3 theScale = transform.localScale;

		theScale.x *= -1;

		transform.localScale = theScale;
	}

	void Start()
	{
		facingRight = false;

		animator = GetComponent<Animator> ();

	}

	void FixedUpdate()
	{
		
	}

	// Update is called once per frame
	void Update () {

		float moveHorizontal = Input.GetAxis ("Horizontal");
			
		if (moveHorizontal > 0.0f) {
			animator.SetInteger ("State",1);
			if (!facingRight) {
				facingRight = true;
				TurnAround ();
			}
		} 
		else if(moveHorizontal < 0.0f){
			animator.SetInteger ("State",1);
			if (facingRight) {
				facingRight = false;
				TurnAround ();
			}
		}
		else
			animator.SetInteger ("State",0);
		
		Vector2 move_vector = new Vector2 (moveHorizontal,0.0f);

		transform.Translate(move_vector * speed);
	}


}

