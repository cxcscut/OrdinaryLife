using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option2Controller : MonoBehaviour {

	private Rect frame;

	public new Camera camera;

	public GameObject option2_clicked;

	// Use this for initialization
	void Start () {

		frame = new Rect (
			transform.position.x - GetComponent<SpriteRenderer>().bounds.size.x / 2,
			transform.position.y + GetComponent<SpriteRenderer>().bounds.size.y / 2,
			GetComponent<SpriteRenderer>().bounds.size.x,
			-GetComponent<SpriteRenderer>().bounds.size.y		
		);

	}

	void OnPress() {
		GlobalVariables.OptionScores = 15;
		StartCoroutine (GameObject.Find ("Chapter2_background").GetComponent<SceneFadeInOut> ().Fading ("Chapter2_3"));
	}

	// Update is called once per frame
	void Update () {
		
		if (!GlobalVariables.Shot5Finished && GlobalVariables.Shot9Active) {
			Vector3 mouse_pos = camera.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 0.0f));
			Vector3 touch_pos = Input.touchCount > 0 ? 
				camera.ScreenToWorldPoint (new Vector3 (Input.GetTouch (0).position.x, Input.GetTouch (0).position.y, 0.0f)) :
				new Vector3 (-255.0f, -255.0f, 0.0f);

			if (frame.Contains (new Vector2 (mouse_pos.x, mouse_pos.y), true) || frame.Contains (new Vector2 (touch_pos.x, touch_pos.y), true)) {

				if (Input.GetMouseButtonDown (0) || (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began)) {
					GetComponent<SpriteRenderer> ().enabled = false;
					option2_clicked.GetComponent<SpriteRenderer> ().enabled = true;
				}

				if (Input.GetMouseButtonUp (0) || (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Ended)) {
					GetComponent<SpriteRenderer> ().enabled = true;
					option2_clicked.GetComponent<SpriteRenderer> ().enabled = false;

					// Deactivated shot #5
					GlobalVariables.Shot5Finished = true;
					GlobalVariables.Shot9Active = false;
					OnPress ();

				}
			}
		}
	}
}
