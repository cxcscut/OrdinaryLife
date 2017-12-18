using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaryTexRolling : MonoBehaviour {

	public const float rolling_speed = 0.025f;

	// Use this for initialization
	void Start () {
		
	}

	void FixedUpdate () {
		if (transform.position.y <= 13.0f) {
			Vector3 move_vector = new Vector3 (0, 1, 0);
			transform.position += move_vector * rolling_speed;
		} else {
			// Entering puzzle stage 1
		}
	}
}
