using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Chapter1StageController : MonoBehaviour {

	public GameObject shot2;
	public GameObject shot3;
	public GameObject shot4;
	public GameObject shot5;
	public GameObject arrow;
	public new Camera camera;

	public Rect arrow_frame;

	private bool auto_play = false;

	IEnumerator AutoPlay()
	{
		yield return new WaitForSeconds (1.5f * GlobalVariables.AutoPlayTimeInterval);

		InteractiveCallback (2);

		yield return new WaitForSeconds (GlobalVariables.AutoPlayTimeInterval);

		InteractiveCallback (3);

		yield return new WaitForSeconds (GlobalVariables.AutoPlayTimeInterval);

		InteractiveCallback (4);

		yield return new WaitForSeconds (GlobalVariables.AutoPlayTimeInterval);

		InteractiveCallback (5);
	}

	IEnumerator ShowShot5()
	{
		yield return new WaitForSeconds (2.0f);

		GameObject.Find ("cellphone_highlight").GetComponent<SpriteRenderer>().enabled = true;

	}

	void InteractiveCallback(int shot)
	{
		if (!GlobalVariables.LightGameFinished)
			return;
		
		switch (shot) {
		case 2:
			// Shot #2
			shot2.GetComponent<Animator>().Play("Chapter1_shot2");
			break;
		case 3:
			// Shot #3
			shot3.GetComponent<Animator>().Play("Chapter1_shot3");
			break;
		case 4:
			// Shot #4
			shot4.GetComponent<Animator>().Play("Chapter1_shot4");
			break;
		case 5:
			// Shot #5
			shot5.GetComponent<Animator> ().Play ("Chapter1_shot5");
			StartCoroutine (ShowShot5());
			GlobalVariables.WechatGameActive = true;
			break;
		default :
			break;
		}

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

		if (GlobalVariables.LightGameFinished && !auto_play) {
			auto_play = true;
			StartCoroutine (AutoPlay());
		}

		if (GlobalVariables.WechatGameFinished) {
			arrow.GetComponent<SpriteRenderer>().enabled = true;

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

					StartCoroutine (GameObject.Find ("Chapter1_background").GetComponent<SceneFadeInOut> ().Fading ("Diary"));

				}
			}
			else
				arrow.GetComponent<Animator> ().enabled = true;
				
		}
	}
}
