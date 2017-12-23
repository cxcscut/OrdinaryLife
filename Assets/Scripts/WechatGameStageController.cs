using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WechatGameStageController : MonoBehaviour {

	public GameObject LeftChoice,RightChoice;
	public GameObject LeftHighlight,RightHighlight;
	public GameObject LeftShadow,RightShadow;
	public GameObject return_button;
	public Animator blur;
	public new Camera camera;

	private bool ChoiceLock = false;

	public new AudioSource audio;

	public Rect Left_frame, Right_frame;

	private int progress = 1;
	private bool choice1 =false,choice2 = false,choice3 = false;

	private List<GameObject> MoveList = new List<GameObject>();

	public Rect return_frame;

	void UpMovingObject()
	{
		MoveList.ForEach (delegate(GameObject o) {
			o.transform.position += new Vector3 (0,GlobalVariables.UpMovingDistance,0) ;
		});

	}

	IEnumerator FadingUnload(string Scene_name)
	{
		yield return new WaitForSeconds (GameObject.Find("blackfading").GetComponent<FadingController>().BeginFade(1));

		SceneManager.UnloadSceneAsync (SceneManager.GetSceneByName(Scene_name));

		yield return new WaitForSeconds (GameObject.Find("blackfading").GetComponent<FadingController>().BeginFade(-1));

	}

	IEnumerator ShowConversation1A()
	{

		GameObject.Find ("1A-1").GetComponent<SpriteRenderer> ().enabled = true;

		yield return new WaitForSeconds (GlobalVariables.TextInterval);

		audio.Play ();

		GameObject.Find ("1A-2").GetComponent<SpriteRenderer> ().enabled = true;

		yield return new WaitForSeconds (GlobalVariables.TextInterval);


		GameObject.Find ("1A-3").GetComponent<SpriteRenderer> ().enabled = true;

		yield return new WaitForSeconds (GlobalVariables.TextInterval);

		audio.Play ();

		GameObject.Find ("1A-4").GetComponent<SpriteRenderer> ().enabled = true;

		yield return new WaitForSeconds (GlobalVariables.TextInterval);


		GameObject.Find ("1A-5").GetComponent<SpriteRenderer> ().enabled = true;

		yield return new WaitForSeconds (GlobalVariables.TextInterval);

		audio.Play ();

		GameObject.Find ("1A-6").GetComponent<SpriteRenderer> ().enabled = true;

		GameObject.Find ("choice_text_left1").GetComponent<SpriteRenderer> ().enabled = false;
		GameObject.Find ("choice_text_right1").GetComponent<SpriteRenderer> ().enabled = false;

		GameObject.Find ("choice_text_left2").GetComponent<SpriteRenderer>().enabled = true;
		GameObject.Find ("choice_text_right2").GetComponent<SpriteRenderer>().enabled = true;

		blur.Play ("BlurIn");
		LeftChoice.GetComponent<Animator> ().Play ("LeftChoiceSlide");
		RightChoice.GetComponent<Animator> ().Play ("RightChoiceSlide");

	}

	IEnumerator ShowConversation2A()
	{

		GameObject.Find ("2A-1").GetComponent<SpriteRenderer> ().enabled = true;

		yield return new WaitForSeconds (GlobalVariables.TextInterval);

		audio.Play ();

		GameObject.Find ("2A-2").GetComponent<SpriteRenderer> ().enabled = true;

		yield return new WaitForSeconds (GlobalVariables.TextInterval);

		UpMovingObject ();


		GameObject.Find ("2A-3").GetComponent<SpriteRenderer> ().enabled = true;

		yield return new WaitForSeconds (GlobalVariables.TextInterval);

		UpMovingObject ();

		audio.Play ();

		GameObject.Find ("2A-4").GetComponent<SpriteRenderer> ().enabled = true;

		UpMovingObject ();

		yield return new WaitForSeconds (GlobalVariables.TextInterval);


		GameObject.Find ("2A-5").GetComponent<SpriteRenderer> ().enabled = true;

		UpMovingObject ();

		yield return new WaitForSeconds (GlobalVariables.TextInterval);

		audio.Play ();

		GameObject.Find ("2A-6").GetComponent<SpriteRenderer> ().enabled = true;

		UpMovingObject ();

		yield return new WaitForSeconds (GlobalVariables.TextInterval);


		GameObject.Find ("2A-7").GetComponent<SpriteRenderer> ().enabled = true;

		UpMovingObject ();

		yield return new WaitForSeconds (GlobalVariables.TextInterval);

		audio.Play ();

		GameObject.Find ("2A-8").GetComponent<SpriteRenderer> ().enabled = true;

		UpMovingObject ();

		yield return new WaitForSeconds (GlobalVariables.TextInterval);


		GameObject.Find ("2A-9").GetComponent<SpriteRenderer> ().enabled = true;

		UpMovingObject ();

		yield return new WaitForSeconds (GlobalVariables.TextInterval);

		UpMovingObject ();

		audio.Play ();

		GameObject.Find ("2A-10").GetComponent<SpriteRenderer> ().enabled = true;

		GameObject.Find ("choice_text_left2").GetComponent<SpriteRenderer> ().enabled = false;
		GameObject.Find ("choice_text_right2").GetComponent<SpriteRenderer> ().enabled = false;

		GameObject.Find ("choice_text_left3").GetComponent<SpriteRenderer>().enabled = true;
		GameObject.Find ("choice_text_right3").GetComponent<SpriteRenderer>().enabled = true;

		blur.Play ("BlurIn");
		LeftChoice.GetComponent<Animator> ().Play ("LeftChoiceSlide");
		RightChoice.GetComponent<Animator> ().Play ("RightChoiceSlide");
	
	}

	IEnumerator ShowConversation3A()
	{
		UpMovingObject ();

		GlobalVariables.WechatGameFinished = true;

		GameObject.Find ("3A-1").GetComponent<SpriteRenderer> ().enabled = true;

		GameObject.Find ("Wechat_return").GetComponent<SpriteRenderer>().enabled = true;

		yield return new WaitForSeconds (GlobalVariables.TextInterval);

	}

	IEnumerator ShowConversation1B()
	{

		GameObject.Find ("1B-1").GetComponent<SpriteRenderer> ().enabled = true;

		yield return new WaitForSeconds (GlobalVariables.TextInterval);

		audio.Play ();

		GameObject.Find ("1B-2").GetComponent<SpriteRenderer> ().enabled = true;

		yield return new WaitForSeconds (GlobalVariables.TextInterval);


		GameObject.Find ("1B-3").GetComponent<SpriteRenderer> ().enabled = true;

		yield return new WaitForSeconds (GlobalVariables.TextInterval);

		audio.Play ();

		GameObject.Find ("1B-4").GetComponent<SpriteRenderer> ().enabled = true;

		yield return new WaitForSeconds (GlobalVariables.TextInterval);


		GameObject.Find ("1B-5").GetComponent<SpriteRenderer> ().enabled = true;

		yield return new WaitForSeconds (GlobalVariables.TextInterval);

		audio.Play ();

		GameObject.Find ("1B-6").GetComponent<SpriteRenderer> ().enabled = true;

		GameObject.Find ("choice_text_left1").GetComponent<SpriteRenderer> ().enabled = false;
		GameObject.Find ("choice_text_right1").GetComponent<SpriteRenderer> ().enabled = false;

		GameObject.Find ("choice_text_left2").GetComponent<SpriteRenderer>().enabled = true;
		GameObject.Find ("choice_text_right2").GetComponent<SpriteRenderer>().enabled = true;

		blur.Play ("BlurIn");
		LeftChoice.GetComponent<Animator> ().Play ("LeftChoiceSlide");
		RightChoice.GetComponent<Animator> ().Play ("RightChoiceSlide");

	}

	IEnumerator ShowConversation2B()
	{
		GameObject.Find ("2B-1").GetComponent<SpriteRenderer> ().enabled = true;

		UpMovingObject ();

		yield return new WaitForSeconds (GlobalVariables.TextInterval);

		audio.Play ();

		GameObject.Find ("2B-2").GetComponent<SpriteRenderer> ().enabled = true;

		yield return new WaitForSeconds (GlobalVariables.TextInterval);

		UpMovingObject ();

		GameObject.Find ("2B-3").GetComponent<SpriteRenderer> ().enabled = true;

		yield return new WaitForSeconds (GlobalVariables.TextInterval);

		UpMovingObject ();

		audio.Play ();

		GameObject.Find ("2B-4").GetComponent<SpriteRenderer> ().enabled = true;

		UpMovingObject ();

		yield return new WaitForSeconds (GlobalVariables.TextInterval);

		GameObject.Find ("2B-5").GetComponent<SpriteRenderer> ().enabled = true;

		UpMovingObject ();

		audio.Play ();

		yield return new WaitForSeconds (GlobalVariables.TextInterval);

		GameObject.Find ("2B-6").GetComponent<SpriteRenderer> ().enabled = true;

		UpMovingObject ();

		yield return new WaitForSeconds (GlobalVariables.TextInterval);

		GameObject.Find ("2B-7").GetComponent<SpriteRenderer> ().enabled = true;

		UpMovingObject ();

		yield return new WaitForSeconds (GlobalVariables.TextInterval);

		UpMovingObject ();

		audio.Play ();

		GameObject.Find ("2B-8").GetComponent<SpriteRenderer> ().enabled = true;

		GameObject.Find ("3A-1").transform.position += new Vector3(0,0.9f,0);
		GameObject.Find ("3B-1").transform.position += new Vector3(0,0.9f,0);

		GameObject.Find ("choice_text_left2").GetComponent<SpriteRenderer> ().enabled = false;
		GameObject.Find ("choice_text_right2").GetComponent<SpriteRenderer> ().enabled = false;

		GameObject.Find ("choice_text_left3").GetComponent<SpriteRenderer>().enabled = true;
		GameObject.Find ("choice_text_right3").GetComponent<SpriteRenderer>().enabled = true;

		blur.Play ("BlurIn");
		LeftChoice.GetComponent<Animator> ().Play ("LeftChoiceSlide");
		RightChoice.GetComponent<Animator> ().Play ("RightChoiceSlide");
	}

	IEnumerator ShowConversation3B()
	{
		UpMovingObject ();
		UpMovingObject ();

		GameObject.Find ("3B-1").GetComponent<SpriteRenderer> ().enabled = true;

		GameObject.Find ("Wechat_return").GetComponent<SpriteRenderer>().enabled = true;

		GlobalVariables.WechatGameFinished = true;

		yield return new WaitForSeconds (GlobalVariables.TextInterval);

	}

	void OnClickedLeftChoice()
	{
		if (ChoiceLock)
			return;
		
		ChoiceLock = true;

		blur.Play ("BlurOut");
		switch (progress) {
		case 1:
			if (!choice1) {
				progress++;
				StartCoroutine (ShowConversation1A());
			}
			break;
		case 2:
			if (!choice2) {
				GlobalVariables.WechatGameScores += 10;
				StartCoroutine (ShowConversation2A());
				progress++;
			}
			break;
		case 3:
			if (!choice3) {
				GlobalVariables.WechatGameScores += 10;
				StartCoroutine (ShowConversation3A());
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
		if (ChoiceLock)
			return;
		
		ChoiceLock = true;

		blur.Play ("BlurOut");
		switch (progress) {
		case 1:
			if (!choice1) {
				GlobalVariables.WechatGameScores += 20;
				StartCoroutine (ShowConversation1B());
				progress++;
			}
			break;
		case 2:
			if (!choice2) {
				StartCoroutine (ShowConversation2B());
				progress++;
			}
			break;
		case 3:
			if (!choice3) {
				GlobalVariables.WechatGameScores += 20;
				StartCoroutine (ShowConversation3B());
				progress++;
			}
			break;
		default:
			break;
		}

		LeftChoice.GetComponent<Animator> ().Play ("LeftChoiceSlideOut");
		RightChoice.GetComponent<Animator> ().Play ("RightChoiceSlideout");
	}

	void OnReturnButton()
	{
		StartCoroutine (FadingUnload("WechatGame"));
		GlobalVariables.WechatGameActive = false;
		GlobalVariables.WechatGameFinished = true;
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
		MoveList.Add (GameObject.Find("1A"));
		MoveList.Add (GameObject.Find("2A"));
		MoveList.Add (GameObject.Find("3A"));

		MoveList.Add (GameObject.Find("1B"));
		MoveList.Add (GameObject.Find("2B"));
		MoveList.Add (GameObject.Find("3B"));

		return_frame = new Rect (
			return_button.transform.position.x - return_button.GetComponent<SpriteRenderer>().bounds.size.x / 2,
			return_button.transform.position.y + return_button.GetComponent<SpriteRenderer>().bounds.size.y / 2,
			return_button.GetComponent<SpriteRenderer>().bounds.size.x,
			-return_button.GetComponent<SpriteRenderer>().bounds.size.y
		);

		blur.Play ("BlurIn");
	}
	
	// Update is called once per frame
	void Update () {

		// Check if animation is done
		AnimatorStateInfo info = LeftChoice.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0);
		if (info.normalizedTime >= 1.0f)
			ChoiceLock = false;

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

		if (GlobalVariables.WechatGameFinished) {

			if (return_frame.Contains (new Vector2 (mouse_pos.x, mouse_pos.y), true)
			    || return_frame.Contains (new Vector2 (touch_pos.x, touch_pos.y), true)) {

				GameObject.Find ("ReturnButton").GetComponent<Animator>().enabled = false;

				Color color1 = GameObject.Find ("Wechat_return_highlight").GetComponent<SpriteRenderer> ().color;
				GameObject.Find ("Wechat_return_highlight").GetComponent<SpriteRenderer> ().color = new Color (color1.r,color1.g,color1.g,255);

				Color color2 = GameObject.Find ("Wechat_return_shadow").GetComponent<SpriteRenderer> ().color;
				GameObject.Find ("Wechat_return_highlight").GetComponent<SpriteRenderer> ().color = new Color (color2.r,color2.g,color2.g,255);

				Color color3 = GameObject.Find ("Wechat_return").GetComponent<SpriteRenderer> ().color;
				GameObject.Find ("Wechat_return_highlight").GetComponent<SpriteRenderer> ().color = new Color (color3.r,color3.g,color3.g,255);

				GameObject.Find ("Wechat_return_highlight").GetComponent<SpriteRenderer> ().enabled = true;

				// Mouse or touchpad pressing 
				if (Input.GetMouseButtonDown (0) || (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began)) {
					GameObject.Find ("Wechat_return_shadow").GetComponent<SpriteRenderer> ().enabled = true;

					OnReturnButton ();
				} else
					// Highlighting return button
					GameObject.Find ("Wechat_return_highlight").GetComponent<SpriteRenderer> ().enabled = true;

				// Mouse or touchpad releasing
				if (Input.GetMouseButtonUp (0) || (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Ended)) {
					GameObject.Find ("Wechat_return_shadow").GetComponent<SpriteRenderer> ().enabled = false;
					GameObject.Find ("Wechat_return_highlight").GetComponent<SpriteRenderer> ().enabled = true;
				}
			} else {
				GameObject.Find ("Wechat_return_highlight").GetComponent<SpriteRenderer> ().enabled = false;
				GameObject.Find ("ReturnButton").GetComponent<Animator>().enabled = true;
			}
		}

	}
}
