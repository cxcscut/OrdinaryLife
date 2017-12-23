using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapter2Stage3Controller : MonoBehaviour {

	public new Camera camera;

	public GameObject shot10;
	public GameObject shot11;
	public GameObject shot12;
	public GameObject shot13;
	public GameObject shot14;
	public GameObject shot15;

	public GameObject presentA,presentB;
	public GameObject presentA_highlight,presentB_highlight;

	public GameObject arrow;

	public Rect arrow_frame;
	public Rect frameA,frameB;

	private bool auto_play = false;
	private bool finish_choice = false,present_active = false;

	IEnumerator AutoPlay()
	{
		yield return new WaitForSeconds (GlobalVariables.AutoPlayTimeInterval);

		InteractiveCallback (1);

		yield return new WaitForSeconds (GlobalVariables.AutoPlayTimeInterval);

		InteractiveCallback (2);

		yield return new WaitForSeconds (GlobalVariables.AutoPlayTimeInterval);

		InteractiveCallback (3);

		yield return new WaitForSeconds (GlobalVariables.AutoPlayTimeInterval);

		InteractiveCallback (4);
	}

	IEnumerator ShowShot1415()
	{
		shot14.GetComponent<Animator> ().Play ("Chapter2_shot14");

		yield return new WaitForSeconds (2.0f);

		shot15.GetComponent<Animator> ().Play ("Chapter2_shot15");

		yield return new WaitForSeconds (2.0f);

		arrow.GetComponent<SpriteRenderer>().enabled =true;
	}

	IEnumerator ShowPresent()
	{
		yield return new WaitForSeconds (2.0f);

		presentA.GetComponent<SpriteRenderer> ().enabled = true;
		presentB.GetComponent<SpriteRenderer> ().enabled = true;
		presentA_highlight.GetComponent<SpriteRenderer> ().enabled = true;
		presentB_highlight.GetComponent<SpriteRenderer> ().enabled = true;
	}

	void InteractiveCallback(int shot)
	{

		switch (shot) {
		case 1:
			// Shot #10
			shot10.GetComponent<Animator>().Play("Chapter2_shot10");
			break;
		case 2:
			// Shot #11
			shot11.GetComponent<Animator>().Play("Chapter2_shot11");
			break;
		case 3:
			// Shot #12
			shot12.GetComponent<Animator>().Play("Chapter2_shot12");
			break;
		case 4:
			// Shot #13
			shot13.GetComponent<Animator> ().Play ("Chapter2_shot13");

			StartCoroutine (ShowPresent());

			present_active = true;
			break;
		default :
			break;
		}

	}

	void OnPresentA()
	{
		StartCoroutine (ShowShot1415 ());

		Debug.Log ("A");
	}

	void OnPresentB()
	{
		StartCoroutine (ShowShot1415 ());
		Debug.Log ("B");
	}

	// Use this for initialization
	void Start () {

		arrow_frame = new Rect (
			arrow.transform.position.x - arrow.GetComponent<SpriteRenderer>().bounds.size.x / 2,
			arrow.transform.position.y + arrow.GetComponent<SpriteRenderer>().bounds.size.y / 2,
			arrow.GetComponent<SpriteRenderer>().bounds.size.x,
			-arrow.GetComponent<SpriteRenderer>().bounds.size.y
		);

		frameA = new Rect (
			presentA_highlight.transform.position.x - presentA_highlight.GetComponent<SpriteRenderer>().bounds.size.x / 2,
			presentA_highlight.transform.position.y + presentA_highlight.GetComponent<SpriteRenderer>().bounds.size.y / 2,
			presentA_highlight.GetComponent<SpriteRenderer>().bounds.size.x,
			-presentA_highlight.GetComponent<SpriteRenderer>().bounds.size.y
		);

		frameB = new Rect (
			presentB_highlight.transform.position.x - presentB_highlight.GetComponent<SpriteRenderer>().bounds.size.x / 2,
			presentB_highlight.transform.position.y + presentB_highlight.GetComponent<SpriteRenderer>().bounds.size.y / 2,
			presentB_highlight.GetComponent<SpriteRenderer>().bounds.size.x,
			-presentB_highlight.GetComponent<SpriteRenderer>().bounds.size.y
		);
	}
	
	// Update is called once per frame
	void Update () {

		if (!auto_play) {
			auto_play = true;
			StartCoroutine (AutoPlay());
		}

		Vector3 mouse_pos = camera.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 0.0f));
		Vector3 touch_pos = Input.touchCount > 0 ? 
			camera.ScreenToWorldPoint (new Vector3 (Input.GetTouch (0).position.x, Input.GetTouch (0).position.y, 0.0f)) :
			new Vector3 (-255.0f, -255.0f, 0.0f);

		if (present_active) {
			// Mouse or touch pad pressing at shot #5
			if (Input.GetMouseButtonDown (0) || (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began)) {

				// Click on A
				if (!finish_choice && frameA.Contains (new Vector2 (mouse_pos.x, mouse_pos.y), true)
					|| frameA.Contains (new Vector2 (touch_pos.x, touch_pos.y), true)) {

					finish_choice = true;

					presentA_highlight.GetComponent<SpriteRenderer> ().enabled = false;
					presentB_highlight.GetComponent<SpriteRenderer> ().enabled = false;

					OnPresentA ();
				}

				// Click on B
				if (!finish_choice && frameB.Contains (new Vector2 (mouse_pos.x, mouse_pos.y), true)
					|| frameB.Contains (new Vector2 (touch_pos.x, touch_pos.y), true)) {

					finish_choice = true;

					presentA_highlight.GetComponent<SpriteRenderer> ().enabled = false;
					presentB_highlight.GetComponent<SpriteRenderer> ().enabled = false;

					OnPresentB ();
				}
			}
		}

		if (finish_choice) {
			
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

					StartCoroutine (GameObject.Find ("Chapter2_3_background").GetComponent<SceneFadeInOut> ().Fading ("Chapter2_4"));
				}
			}
			else
				arrow.GetComponent<Animator> ().enabled = true;

		}
	}
}
