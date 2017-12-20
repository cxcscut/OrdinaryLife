using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Chapter2Stage1Controller : MonoBehaviour {

	private int click_num = 0;

	public GameObject shot1;
	public GameObject shot2;
	public GameObject shot3;
	public GameObject shot4;
	public GameObject shot5_happy;
	public GameObject shot6_sad;
	public GameObject shot4_highlighter;

	void InteractiveCallback(int shot)
	{
		switch (shot) {
		case 1:
			// Shot #1
			shot1.GetComponent<SpriteRenderer>().enabled =true;
			break;
		case 2:
			// Shot #2
			shot2.GetComponent<SpriteRenderer>().enabled = true;
			break;
		case 3:
			// Shot #3
			shot3.GetComponent<SpriteRenderer>().enabled = true;
			break;
		case 4:
			// Shot #4
			shot4.GetComponent<SpriteRenderer> ().enabled = true;
			GlobalVariables.Shot4Active = true;

			// Display highlighter
			shot4_highlighter.GetComponent<SpriteRenderer> ().enabled = true;
			shot4_highlighter.GetComponent<Animator> ().enabled = true;
			break;
		default :
			break;
		}

	}

	// @params : void
	// @return : void
	// @brif : Go to next stage when shot #5 display after finished MenuGame
	void GotoNextStage()
	{
		// Go to next stage
		SceneManager.LoadScene ("Chapter2_2");
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

		if (Input.touchCount > 0 || Input.GetMouseButtonDown (0)) {
			InteractiveCallback (++click_num);
		}

		if (GlobalVariables.Shot4Finished) {
			// display happy shot #5 or sad shot #5

			if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
				GotoNextStage ();
		}
	}
}
