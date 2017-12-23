using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingSceneController : MonoBehaviour {

	public new Camera camera;

	public GameObject quit,quit_clicked;
	public GameObject playagain,playagain_clicked;
	public GameObject ProducerInfo;

	private bool button_lock = false;

	private Rect quit_frame,again_frame;

	void OnQuit()
	{
		Application.Quit ();
	}

	void OnPlayAgain()
	{
		// Reset global variables
		GlobalVariables.EnterWarmScene = false;
		GlobalVariables.WechatGameFinished = false;
		GlobalVariables.WechatGameScores = 0;
		GlobalVariables.DiaryTextIndex = 1;
		GlobalVariables.Shot4Finished = false;
		GlobalVariables.Shot5Finished = false;
		GlobalVariables.Shot9Active = false;
		GlobalVariables.Shot4Active = false;
		GlobalVariables.WechatGameActive = false;
		GlobalVariables.OptionScores = 0;
		GlobalVariables.LightGameFinished = false;
		GlobalVariables.LightGameActive = true;
		GlobalVariables.LightGameScores = 0;
		GlobalVariables.MenuGameFinished = false;
		GlobalVariables.MenuGameScores = 0;
		GlobalVariables.EndingType = -1;

		if (GameObject.Find ("BGMplayer") != null)
			DestroyObject (GameObject.Find("BGMplayer"));

		StartCoroutine(GetComponent<SceneFadeInOut> ().Fading ("Starting"));
	}

	IEnumerator ShowBestEndingText()
	{
		GameObject.Find ("best_ending_text1").GetComponent<Animator>().Play("bestEnding_text1");

		yield return new WaitForSecondsRealtime (3.0f);

		GameObject.Find ("best_ending_text2").GetComponent<Animator>().Play("bestEnding_text2");

		yield return new WaitForSecondsRealtime (3.0f);

		GameObject.Find ("best_ending_text3").GetComponent<Animator>().Play("bestEnding_text3");

		yield return new WaitForSecondsRealtime (3.0f);

		GameObject.Find ("best_ending_text4").GetComponent<Animator>().Play("bestEnding_text4");
	}

	IEnumerator ShowOrdinaryEndingText()
	{
		GameObject.Find ("ordinary_ending_text1").GetComponent<Animator>().Play("ordinaryEnding_text1");

		yield return new WaitForSecondsRealtime (3.0f);

		GameObject.Find ("ordinary_ending_text2").GetComponent<Animator>().Play("ordinaryEnding_text2");

		yield return new WaitForSecondsRealtime (3.0f);

		GameObject.Find ("ordinary_ending_text3").GetComponent<Animator>().Play("ordinaryEnding_text3");

		yield return new WaitForSecondsRealtime (3.0f);

		GameObject.Find ("ordinary_ending_text4").GetComponent<Animator>().Play("ordinaryEnding_text4");
	}

	IEnumerator ShowBadEndingText()
	{
		GameObject.Find ("bad_ending_text1").GetComponent<Animator>().Play("badEnding_text1");

		yield return new WaitForSecondsRealtime (3.0f);

		GameObject.Find ("bad_ending_text2").GetComponent<Animator>().Play("badEnding_text2");

		yield return new WaitForSecondsRealtime (3.0f);

		GameObject.Find ("bad_ending_text3").GetComponent<Animator>().Play("badEnding_text3");

		yield return new WaitForSecondsRealtime (3.0f);

		GameObject.Find ("bad_ending_text4").GetComponent<Animator>().Play("badEnding_text4");

		yield return new WaitForSecondsRealtime (3.0f);

		GameObject.Find ("bad_ending_text5").GetComponent<Animator>().Play("badEnding_text5");

		yield return new WaitForSecondsRealtime (3.0f);

		GameObject.Find ("bad_ending_text6").GetComponent<Animator>().Play("badEnding_text6");
	}

	void DisplayBestEnding()
	{
		GameObject.Find ("bestending_background").GetComponent<SpriteRenderer>().enabled = true;

		StartCoroutine (ShowBestEndingText());
	}

	void DisplayOrdinaryEnding()
	{
		GameObject.Find ("ordinaryending_background").GetComponent<SpriteRenderer>().enabled = true;

		StartCoroutine (ShowOrdinaryEndingText());
	}

	void DisplayBadEnding()
	{
		GameObject.Find ("badending_background").GetComponent<SpriteRenderer>().enabled = true;

		StartCoroutine (ShowBadEndingText());
	}

	// Use this for initialization
	void Start () {

		if (GlobalVariables.EndingType == 3)
			ProducerInfo.transform.position = new Vector3 (
				-ProducerInfo.transform.position.x,
				ProducerInfo.transform.position.y,
				ProducerInfo.transform.position.z
			);

		if (GameObject.Find ("BGMplayer") != null) {
			if (GameObject.Find ("BGMplayer").GetComponent<AudioSource> ().isPlaying)
				GameObject.Find ("BGMplayer").GetComponent<AudioSource> ().Stop ();
		}

		quit_frame = new Rect (
			quit.transform.position.x - quit.GetComponent<SpriteRenderer>().bounds.size.x / 2,
			quit.transform.position.y + quit.GetComponent<SpriteRenderer>().bounds.size.y / 2,
			quit.GetComponent<SpriteRenderer>().bounds.size.x,
			-quit.GetComponent<SpriteRenderer>().bounds.size.y
		);

		again_frame = new Rect (
			playagain.transform.position.x - playagain.GetComponent<SpriteRenderer>().bounds.size.x / 2,
			playagain.transform.position.y + playagain.GetComponent<SpriteRenderer>().bounds.size.y / 2,
			playagain.GetComponent<SpriteRenderer>().bounds.size.x,
			-playagain.GetComponent<SpriteRenderer>().bounds.size.y
		);

		switch (GlobalVariables.EndingType) {
		case 1:
			DisplayBestEnding ();
			GameObject.Find ("bestending_background").GetComponent<AudioSource>().Play();
			break;
		case 2:
			DisplayOrdinaryEnding();
			GameObject.Find ("ordinaryending_background").GetComponent<AudioSource>().Play();
			break;
		case 3:
			DisplayBadEnding ();
			GameObject.Find ("badending_background").GetComponent<AudioSource>().Play();
			break;
		default:
			Application.Quit ();
			break;
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (transform.position.y <= 13.0f) {
			Vector3 move_vector = new Vector3 (0, 1, 0);
			ProducerInfo.transform.position += move_vector * GlobalVariables.ProducerInfo_rollingspeed;
		} 

		Vector3 mouse_pos = camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,0.0f));
		Vector3 touch_pos = Input.touchCount > 0 ? 
			camera.ScreenToWorldPoint (new Vector3 (Input.GetTouch (0).position.x, Input.GetTouch (0).position.y, 0.0f)) :
			new Vector3 (-255.0f,-255.0f,0.0f);

		if (quit_frame.Contains (new Vector2 (mouse_pos.x, mouse_pos.y), true)
		    || quit_frame.Contains (new Vector2 (touch_pos.x, touch_pos.y), true)) {
		
			quit.GetComponent<SpriteRenderer> ().enabled = false;
			quit_clicked.GetComponent<SpriteRenderer> ().enabled = true;

			if (Input.GetMouseButtonDown (0) || (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began)) {
				quit_clicked.transform.position += GlobalVariables.click_offset;
			}

			if (Input.GetMouseButtonUp (0) || (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Ended)) {
				
				quit_clicked.transform.position -= GlobalVariables.click_offset;

				if (!button_lock) {
					button_lock = true;	

					OnQuit ();
				}
			}
		} else {
			quit.GetComponent<SpriteRenderer> ().enabled = true;
			quit_clicked.GetComponent<SpriteRenderer> ().enabled = false;
		}

		if (again_frame.Contains (new Vector2 (mouse_pos.x, mouse_pos.y), true)
		    || again_frame.Contains (new Vector2 (touch_pos.x, touch_pos.y), true)) {

			playagain.GetComponent<SpriteRenderer> ().enabled = false;
			playagain_clicked.GetComponent<SpriteRenderer> ().enabled = true;

			if (Input.GetMouseButtonDown (0) || (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began)) {
				playagain_clicked.transform.position += GlobalVariables.click_offset;
			}

			if (Input.GetMouseButtonUp (0) || (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Ended)) {
				playagain_clicked.transform.position -= GlobalVariables.click_offset;

				if (!button_lock) {
					button_lock = true;		

					OnPlayAgain ();
				}
			}
		} else {
			playagain.GetComponent<SpriteRenderer> ().enabled = true;
			playagain_clicked.GetComponent<SpriteRenderer> ().enabled = false;
		}
	}
}
