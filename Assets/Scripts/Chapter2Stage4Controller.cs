using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapter2Stage4Controller : MonoBehaviour {

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

	private bool auto_play = false;

	IEnumerator AutoPlay ()
	{
		yield return new WaitForSeconds (GlobalVariables.AutoPlayTimeInterval);

		InteractiveCallback (1);

		yield return new WaitForSeconds (GlobalVariables.AutoPlayTimeInterval);

		InteractiveCallback (2);

		yield return new WaitForSeconds (GlobalVariables.AutoPlayTimeInterval);

		InteractiveCallback (3);

		yield return new WaitForSeconds (GlobalVariables.AutoPlayTimeInterval);

		InteractiveCallback (4);

		yield return new WaitForSeconds (GlobalVariables.AutoPlayTimeInterval);

		InteractiveCallback (5);

		yield return new WaitForSeconds (GlobalVariables.AutoPlayTimeInterval);

		InteractiveCallback (6);
	}

	IEnumerator ShowArrow()
	{
		shot21.GetComponent<Animator>().Play("Chapter2_shot21");

		yield return new WaitForSeconds (2.0f);

		arrow.GetComponent<SpriteRenderer> ().enabled = true;
		finished = true;
	}

	void InteractiveCallback(int shot)
	{

		switch (shot) {
		case 1:
			// Shot #16
			shot16.GetComponent<Animator>().Play("Chapter2_shot16");
			break;
		case 2:
			// Shot #17
			shot17.GetComponent<Animator>().Play("Chapter2_shot17");
			break;
		case 3:
			// Shot #18
			shot18.GetComponent<Animator>().Play("Chapter2_shot18");
			break;
		case 4:
			// Shot #19
			shot19.GetComponent<Animator>().Play("Chapter2_shot19");
			break;

		case 5:
			// Shot #20
			shot20.GetComponent<Animator>().Play("Chapter2_shot20");
			break;
		case 6:
			// Shot #21

			StartCoroutine (ShowArrow());
			break;
		default :
			break;
		}

	}

	void GotoEndingScene()
	{
		Debug.Log ("Go to Ending Scene");

		int FinalScores=0, Chapter1Scores=0, Chapter2Scores=0;

		// Compute Chapter1 scores
		if (GlobalVariables.LightGameScores + GlobalVariables.WechatGameScores < 20)
			Chapter1Scores = 0;
		else if (GlobalVariables.LightGameScores + GlobalVariables.WechatGameScores >= 20
		         && GlobalVariables.LightGameScores + GlobalVariables.WechatGameScores < 70)
			Chapter1Scores = 1;
		else if (GlobalVariables.LightGameScores + GlobalVariables.WechatGameScores >= 70)
			Chapter1Scores = 2;

		// Compute Chapter2 scores
		if (GlobalVariables.OptionScores + GlobalVariables.MenuGameScores < 20)
			Chapter2Scores = 0;
		else if (GlobalVariables.OptionScores + GlobalVariables.MenuGameScores >= 20
		         && GlobalVariables.OptionScores + GlobalVariables.MenuGameScores < 70)
			Chapter2Scores = 1;
		else if (GlobalVariables.OptionScores + GlobalVariables.MenuGameScores >= 70)
			Chapter2Scores = 2;

		Debug.Log ("LightGameScores = " + GlobalVariables.LightGameScores.ToString());
		Debug.Log ("WechatGameScores = " + GlobalVariables.WechatGameScores.ToString());
		Debug.Log ("Chapter1Scores = " + Chapter1Scores.ToString());

		Debug.Log ("MenuGameScores = " + GlobalVariables.MenuGameScores.ToString());
		Debug.Log ("OptionScores = " + GlobalVariables.OptionScores.ToString());
		Debug.Log ("Chapter2Scores = " + Chapter2Scores.ToString());

		FinalScores = Chapter1Scores + Chapter2Scores;

		Debug.Log ("FinalScores = " + FinalScores.ToString());

		// Set the ending type
		if (FinalScores <= 1) {
			// Entering the bad ending
			GlobalVariables.EndingType = 3;
		} else if (FinalScores > 1 && FinalScores <= 3) {
			// Entering the ordinary ending
			GlobalVariables.EndingType = 2;
		} else if (FinalScores >= 4) {
			// Entering the best endind
			GlobalVariables.EndingType = 1;
		}

		// Entering the ending scene
		StartCoroutine(GetComponent<SceneFadeInOut> ().Fading ("Ending"));
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

					// Go to ENDING scene
					GotoEndingScene ();
				}
			}
			else
				arrow.GetComponent<Animator> ().enabled = true;

		}
	}
}
