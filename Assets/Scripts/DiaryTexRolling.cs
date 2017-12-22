using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DiaryTexRolling : MonoBehaviour {

	public Animator animator;
	public Image black;

	// Use this for initialization
	void Start () {
		
	}

	void FixedUpdate () {
		if (transform.position.y <= 13.0f) {
			Vector3 move_vector = new Vector3 (0, 1, 0);
			transform.position += move_vector * GlobalVariables.text_rolling_speed;
		} 
		if (transform.position.y >= 8.0f) {
			if(GlobalVariables.DiaryTextIndex == 1)
				// Entering puzzle stage 1
				StartCoroutine (GameObject.Find ("diary").GetComponent<SceneFadeInOut> ().Fading ("Chapter1_1"));
			else if(GlobalVariables.DiaryTextIndex == 2)
				// Entering Chapter2_1
				StartCoroutine (GameObject.Find ("diary").GetComponent<SceneFadeInOut> ().Fading ("Chapter2_1"));
		}
	}
}
