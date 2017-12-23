using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Chapter2Stage1Controller : MonoBehaviour {

	public GameObject shot1;
	public GameObject shot2;
	public GameObject shot3;
	public GameObject shot4;
	public GameObject shot5_happy;
	public GameObject shot6_sad;
	public GameObject shot4_highlighter;

	public new Camera camera;

	public GameObject arrow;
	public Rect arrow_frame;

	private bool auto_play = false;

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

	IEnumerator ShowShot4()
	{
		yield return new WaitForSeconds (2.0f);
		// Display highlighter
		shot4_highlighter.GetComponent<SpriteRenderer> ().enabled = true;
		shot4_highlighter.GetComponent<Animator> ().enabled = true;
	}

	void InteractiveCallback(int shot)
	{
		switch (shot) {
		case 1:
			// Shot #1
			shot1.GetComponent<Animator>().Play("Chapter2_shot1");
			break;
		case 2:
			// Shot #2
			shot2.GetComponent<Animator>().Play("Chapter2_shot2");
			break;
		case 3:
			// Shot #3
			shot3.GetComponent<Animator>().Play("Chapter2_shot3");
			break;
		case 4:
			// Shot #4
			shot4.GetComponent<Animator> ().Play ("Chapter2_shot4");
			GlobalVariables.Shot4Active = true;

			StartCoroutine (ShowShot4());
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
		arrow_frame = new Rect (
			arrow.transform.position.x - arrow.GetComponent<SpriteRenderer>().bounds.size.x / 2,
			arrow.transform.position.y + arrow.GetComponent<SpriteRenderer>().bounds.size.y / 2,
			arrow.GetComponent<SpriteRenderer>().bounds.size.x,
			-arrow.GetComponent<SpriteRenderer>().bounds.size.y
		);
	}

	// Update is called once per frame
	void Update () {

		if (!auto_play) {
			auto_play = true;
			StartCoroutine (AutoPlay());
		}

		if (GlobalVariables.MenuGameFinished) {
			// display happy shot #5 or sad shot #5
			arrow.GetComponent<SpriteRenderer> ().enabled = true;

			Vector3 mouse_pos = camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,0.0f));
			Vector3 touch_pos = Input.touchCount > 0 ? 
				camera.ScreenToWorldPoint (new Vector3 (Input.GetTouch (0).position.x, Input.GetTouch (0).position.y, 0.0f)) :
				new Vector3 (-255.0f,-255.0f,0.0f);

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

					GlobalVariables.DiaryTextIndex = 2;

					if(GlobalVariables.MenuGameFinished)
						StartCoroutine (GetComponent<SceneFadeInOut> ().Fading ("Chapter2_2"));

				}
			}
			else
				arrow.GetComponent<Animator> ().enabled = true;
		}
	}
}
