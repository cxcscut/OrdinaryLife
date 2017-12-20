using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WechatGameStageController : MonoBehaviour {

	public GameObject LeftChoice,RightChoice;
	public GameObject LeftHighlight,RightHighlight;
	public GameObject LeftShadow,RightShadow;
	public new Camera camera;

	public Rect Left_frame, Right_frame;

	private int progress = 1;
	private bool choice1 =false,choice2 = false,choice3 = false;


	void OnClickedLeftChoice()
	{
		switch (progress) {
		case 1:
			if (!choice1) {
				choice1 = true;
				progress++;
			}
			break;
		case 2:
			if (!choice2) {
				choice2 = true;
				progress++;
			}
			break;
		case 3:
			if (!choice3) {
				choice3 = true;
				progress++;
			}
			break;
		default:
			break;
		}

		LeftChoice.GetComponent<Animator> ().Play ("LeftChoiceSlideOut");
		RightChoice.GetComponent<Animator> ().Play ("RightChoiceSlideout");
	}

	void OnClickedRightChoice()
	{
		switch (progress) {
		case 1:
			if (!choice1) {
				choice1 = true;
				progress++;
			}
			break;
		case 2:
			if (!choice2) {
				choice2 = true;
				progress++;
			}
			break;
		case 3:
			if (!choice3) {
				choice3 = true;
				progress++;
			}
			break;
		default:
			break;
		}

		LeftChoice.GetComponent<Animator> ().Play ("LeftChoiceSlideOut");
		RightChoice.GetComponent<Animator> ().Play ("RightChoiceSlideout");
	}

	void UpdatePos()
	{
		Left_frame = new Rect (
			LeftChoice.transform.position.x - LeftChoice.GetComponent<SpriteRenderer>().bounds.size.x / 2,
			LeftChoice.transform.position.y + LeftChoice.GetComponent<SpriteRenderer>().bounds.size.y / 2,
			LeftChoice.GetComponent<SpriteRenderer>().bounds.size.x,
			-LeftChoice.GetComponent<SpriteRenderer>().bounds.size.y
		);

		Right_frame = new Rect (
			RightChoice.transform.position.x - RightChoice.GetComponent<SpriteRenderer>().bounds.size.x / 2,
			RightChoice.transform.position.y + RightChoice.GetComponent<SpriteRenderer>().bounds.size.y / 2,
			RightChoice.GetComponent<SpriteRenderer>().bounds.size.x,
			-RightChoice.GetComponent<SpriteRenderer>().bounds.size.y
		);
	}

	// Use this for initialization
	void Start () {

		Debug.Log (Left_frame);
	}
	
	// Update is called once per frame
	void Update () {

		UpdatePos ();

		Vector3 mouse_pos = camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,0.0f));
		Vector3 touch_pos = Input.touchCount > 0 ? 
			camera.ScreenToWorldPoint (new Vector3 (Input.GetTouch (0).position.x, Input.GetTouch (0).position.y, 0.0f)) :
			new Vector3 (-255.0f,-255.0f,0.0f);
		
		// Left Choice
		if (Left_frame.Contains (new Vector2 (mouse_pos.x, mouse_pos.y), true)
		    || Left_frame.Contains (new Vector2 (touch_pos.x, touch_pos.y), true)) {
		
			LeftChoice.GetComponent<SpriteRenderer> ().enabled = false;

			// Mouse or touchpad pressing 
			if (Input.GetMouseButtonDown (0) || (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began)) {

				LeftHighlight.GetComponent<SpriteRenderer> ().enabled = false;
				LeftChoice.GetComponent<SpriteRenderer> ().enabled = false;
				LeftShadow.GetComponent<SpriteRenderer> ().enabled = true;

				OnClickedLeftChoice ();
			}
			else
				// Highlighting edge
				LeftHighlight.GetComponent<SpriteRenderer> ().enabled = true;
			
			// Mouse or touchpad releasing
			if(Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Ended))
			{
				LeftChoice.GetComponent<SpriteRenderer> ().enabled = true;
				LeftShadow.GetComponent<SpriteRenderer> ().enabled = false;
			}
		} else {
			LeftHighlight.GetComponent<SpriteRenderer> ().enabled = false;
			LeftChoice.GetComponent<SpriteRenderer> ().enabled = true;
		}

		// Right Choice
		if (Right_frame.Contains (new Vector2 (mouse_pos.x, mouse_pos.y), true)
		    || Right_frame.Contains (new Vector2 (touch_pos.x, touch_pos.y), true)) {

			RightChoice.GetComponent<SpriteRenderer> ().enabled = false;

			// Mouse or touchpad pressing 
			if (Input.GetMouseButtonDown (0) || (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began)) {

				RightHighlight.GetComponent<SpriteRenderer> ().enabled = false;
				RightChoice.GetComponent<SpriteRenderer> ().enabled = false;
				RightShadow.GetComponent<SpriteRenderer> ().enabled = true;

				OnClickedRightChoice ();
			}
			else
				// Highlighting edge
				RightHighlight.GetComponent<SpriteRenderer> ().enabled = true;
			
			// Mouse or touchpad releasing
			if(Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Ended))
			{
				RightChoice.GetComponent<SpriteRenderer> ().enabled = true;
				RightShadow.GetComponent<SpriteRenderer> ().enabled = false;
			}

		} else {
			RightHighlight.GetComponent<SpriteRenderer> ().enabled = false;
			RightChoice.GetComponent<SpriteRenderer> ().enabled = true;
		}

	}
}
