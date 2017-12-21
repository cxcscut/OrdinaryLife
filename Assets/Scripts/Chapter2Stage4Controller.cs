using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapter2Stage4Controller : MonoBehaviour {

	private int click_num = 0;

	public new Camera camera;

	public bool finished = false;

	public GameObject shot16;
	public GameObject shot17;
	public GameObject shot18;
	public GameObject shot19;
	public GameObject shot20;
	public GameObject shot21;

	public GameObject arrow;
	public Rect arrow_frame;

	void InteractiveCallback(int shot)
	{

		switch (shot) {
		case 1:
			// Shot #16
			shot16.GetComponent<SpriteRenderer>().enabled = true;
			break;
		case 2:
			// Shot #17
			shot17.GetComponent<SpriteRenderer>().enabled =true;
			break;
		case 3:
			// Shot #18
			shot18.GetComponent<SpriteRenderer>().enabled =true;
			break;
		case 4:
			// Shot #19
			shot19.GetComponent<SpriteRenderer> ().enabled = true;
			break;

		case 5:
			// Shot #20
			shot20.GetComponent<SpriteRenderer> ().enabled = true;
			break;
		case 6:
			// Shot #21
			shot21.GetComponent<SpriteRenderer> ().enabled = true;
			arrow.GetComponent<SpriteRenderer> ().enabled = true;
			finished = true;
			break;
		default :
			break;
		}

	}

	void GotoDiary()
	{
		Debug.Log ("Go back to Diary Scene");
	}

	// Use this for initialization
	void Start () {
		arrow_frame = new Rect (
			arrow.transform.position.x - arrow.GetComponent<SpriteRenderer>().bounds.size.x / 2,
			arrow.transform.position.y + arrow.GetComponent<SpriteRenderer>().bounds.size.y / 2,
			arrow.GetComponent<SpriteRenderer>().bounds.size.x,
			-arrow.GetComponent<SpriteRenderer>().bounds.size.y
		);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.touchCount > 0 || Input.GetMouseButtonDown (0)) {
			InteractiveCallback (++click_num);
		}

		Vector3 mouse_pos = camera.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 0.0f));
		Vector3 touch_pos = Input.touchCount > 0 ? 
			camera.ScreenToWorldPoint (new Vector3 (Input.GetTouch (0).position.x, Input.GetTouch (0).position.y, 0.0f)) :
			new Vector3 (-255.0f, -255.0f, 0.0f);

		if (finished) {

			if (arrow_frame.Contains (new Vector2 (mouse_pos.x, mouse_pos.y), true)
				|| arrow_frame.Contains (new Vector2 (touch_pos.x, touch_pos.y), true)) {
				arrow.GetComponent<Animator> ().enabled = false;
				Color current = arrow.GetComponent<SpriteRenderer> ().color;
				arrow.GetComponent<SpriteRenderer> ().color = new Color (current.r,current.g,current.b,1.0f);

				// Mouse or touchpad pressing 
				if (Input.GetMouseButtonDown (0) || (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began)) {

					arrow.transform.position += GlobalVariables.click_offset;

				}

				// Mouse or touchpad releasing
				if(Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Ended))
				{
					arrow.transform.position -= GlobalVariables.click_offset;

					GotoDiary ();
				}
			}
			else
				arrow.GetComponent<Animator> ().enabled = true;

		}
	}
}
