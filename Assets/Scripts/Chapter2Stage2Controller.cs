using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapter2Stage2Controller : MonoBehaviour {

	private int click_num = 0;

	public GameObject shot6;
	public GameObject shot7;
	public GameObject shot8;
	public GameObject shot9;

	public GameObject option1;
	public GameObject option2;
	public GameObject option3;

	void InteractiveCallback(int shot)
	{
		switch (shot) {
		case 1:
			// Shot #6
			shot6.GetComponent<SpriteRenderer>().enabled =true;
			break;
		case 2:
			// Shot #7
			shot7.GetComponent<SpriteRenderer>().enabled = true;
			break;
		case 3:
			// Shot #8
			shot8.GetComponent<SpriteRenderer>().enabled = true;
			break;
		case 4:
			// Shot #9
			shot9.GetComponent<SpriteRenderer> ().enabled = true;
			option1.GetComponent<SpriteRenderer> ().enabled = true;
			option2.GetComponent<SpriteRenderer> ().enabled = true;
			option3.GetComponent<SpriteRenderer> ().enabled = true;

			GlobalVariables.Shot9Active = true;
			break;
		default :
			break;
		}

	}
		
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.touchCount > 0 || Input.GetMouseButtonDown (0)) {
			InteractiveCallback (++click_num);
		}


	}
}
