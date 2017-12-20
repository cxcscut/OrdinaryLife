using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapter1StageController : MonoBehaviour {

	private int click_num = 1;

	public GameObject shot2;
	public GameObject shot3;
	public GameObject shot4;
	public GameObject shot5;

	void InteractiveCallback(int shot)
	{
		if (!GlobalVariables.LightGameFinished)
			return;
		
		switch (shot) {
		case 2:
			// Shot #2
			shot2.GetComponent<SpriteRenderer>().enabled = true;
			break;
		case 3:
			// Shot #3
			shot3.GetComponent<SpriteRenderer>().enabled =true;
			break;
		case 4:
			// Shot #4
			shot4.GetComponent<SpriteRenderer>().enabled =true;
			break;
		case 5:
			// Shot #5
			shot5.GetComponent<SpriteRenderer> ().enabled = true;
			GlobalVariables.WechatGameActive = true;
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
			if(GlobalVariables.LightGameFinished)
				InteractiveCallback (++click_num);
		}
	}
}
