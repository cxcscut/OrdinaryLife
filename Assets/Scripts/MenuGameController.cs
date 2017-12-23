using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuGameController : MonoBehaviour {

	public GameObject rule_button,rule_button_clicked;
	public GameObject order_button,order_button_clicked;

	private int dishes_num = 0;
	private int soup_num = 0;
	private int drinks_num = 0;
	private int TotalPrice = 0;

	public new Camera camera;

	private Rect order_frame, rule_frame;

	// Use this for initialization
	void Start () {
		rule_frame = new Rect (
			rule_button.transform.position.x - rule_button.GetComponent<SpriteRenderer>().bounds.size.x / 2,
			rule_button.transform.position.y + rule_button.GetComponent<SpriteRenderer>().bounds.size.y / 2,
			rule_button.GetComponent<SpriteRenderer>().bounds.size.x,
			-rule_button.GetComponent<SpriteRenderer>().bounds.size.y
		);

		order_frame = new Rect (
			order_button.transform.position.x - order_button.GetComponent<SpriteRenderer>().bounds.size.x / 2,
			order_button.transform.position.y + order_button.GetComponent<SpriteRenderer>().bounds.size.y / 2,
			order_button.GetComponent<SpriteRenderer>().bounds.size.x,
			-order_button.GetComponent<SpriteRenderer>().bounds.size.y
		);
	}

	IEnumerator FadingUnload(string Scene_name)
	{
		yield return new WaitForSeconds (GameObject.Find("blackfading").GetComponent<FadingController>().BeginFade(1));

		SceneManager.UnloadSceneAsync (SceneManager.GetSceneByName(Scene_name));

		yield return new WaitForSeconds (GameObject.Find("blackfading").GetComponent<FadingController>().BeginFade(-1));

	}

	IEnumerator ShowOS(string OS_name)
	{
		GameObject os = GameObject.Find (OS_name);
		if (os != null) {
			os.GetComponent<SpriteRenderer> ().enabled = true;
		}

		yield return new WaitForSeconds (GlobalVariables.OSDisplayTime);

		if (os != null)
			os.GetComponent<SpriteRenderer> ().enabled = false;
	}

	void OnRuleButton()
	{
		Debug.Log ("Rule button");	
		StartCoroutine(ShowOS ("RuleOS"));
	}

	void OnCheckBoxClicked(GameObject checkbox,int dir)
	{

		if (checkbox.CompareTag ("BlackDishes"))
			GlobalVariables.MenuGameScores += dir * 5;
		else if (checkbox.CompareTag ("RedDishes"))
			GlobalVariables.MenuGameScores += dir * 10;

		int index = int.Parse(checkbox.name.Substring (16, checkbox.name.Length - 16));

		if (index >= 1 && index <= 16)
			dishes_num++;
		else if (index >= 17 && index <= 21)
			soup_num++;
		else if (index >= 22 && index <= 25)
			drinks_num++;
		
		switch (index) {
		case 1:
			TotalPrice += dir * 28;
			break;
		case 2:
			TotalPrice += dir * 58;
			break;
		case 3:
			TotalPrice += dir *  48;
			/*
			if(dir==1)
				// gu fa shen jin kao e --> os2
				StartCoroutine (ShowOS ("os2"));
				*/
			break;
		case 4:
			TotalPrice += dir * 48;
			break;
		case 5:
			/*
			if(dir==1)
				// bo luo gu lao rou --> os1
				StartCoroutine (ShowOS ("os1"));
				*/
			TotalPrice += dir * 38;
			break;
		case 6:
			TotalPrice += dir * 38;
			if(dir==1)
				// mei cai kou rou --> os6
				StartCoroutine (ShowOS ("os6"));
			break;
		case 7:
			TotalPrice += dir * 48;
			break;
		case 8:
			TotalPrice += dir * 28;
			/*
			if(dir==1)
				// tang cu pai gu --> os3
				StartCoroutine (ShowOS ("os3"));
				*/
			break;
		case 9:
			if(dir==1)
				// liang gua niu rou --> os5
				StartCoroutine (ShowOS ("os5"));
			TotalPrice += dir * 38;
			break;
		case 10:
			if(dir==1)
				// lu yu --> os7
				StartCoroutine (ShowOS ("os7"));
			TotalPrice += dir * 38;
			break;
		case 11:
			TotalPrice += dir * 38;
			if(dir==1)
				// xia ren hua dan --> os9
				StartCoroutine (ShowOS ("os9"));
			break;
		case 12:
			TotalPrice += dir * 68;
			break;
		case 13:
			TotalPrice += dir * 68;
			/*
			if(dir==1)
				// bai zhuo xia --> os8
				StartCoroutine (ShowOS ("os8"));
				*/
			break;
		case 14:
			TotalPrice += dir * 18;
			break;
		case 15:
			TotalPrice += dir * 28;
			break;
		case 16:
			TotalPrice += dir * 28;
			break;
		case 17:
			TotalPrice += dir * 29;
			break;
		case 18:
			TotalPrice += dir * 29;
			break;
		case 19:
			TotalPrice += dir * 29;
			break;
		case 20:
			TotalPrice += dir * 39;
			break;
		case 21:
			
			TotalPrice += dir * 39;
			if(dir==1)
				// bao zhu gu --> os4
				StartCoroutine (ShowOS ("os4"));
			break;
		case 22:
			TotalPrice += dir * 10;
			break;
		case 23:
			TotalPrice += dir * 12;
			break;
		case 24:
			TotalPrice += dir * 15;
			break;
		case 25:
			TotalPrice += dir * 15;
			break;
		default:
			break;
		}
			
	}

	void OnOrderButton()
	{
		Debug.Log ("Order button");

		if (TotalPrice >= 200) {
			StartCoroutine(ShowOS ("order_os1"));
			return;
		}
		if (dishes_num < 3 || drinks_num < 1 || soup_num < 1) {
			StartCoroutine(ShowOS ("order_os2"));
			return;
		}

		GlobalVariables.MenuGameFinished = true;

		if (GlobalVariables.MenuGameScores >= 25)
			GameObject.Find ("shot5_happy").GetComponent<Animator>().Play ("Chapter2_shot5_happy");
		else
			GameObject.Find ("shot5_sad").GetComponent<Animator>().Play ("Chapter2_shot5_sad");

		StartCoroutine (FadingUnload("MenuGame"));
	}

	// Update is called once per frame
	void Update () {

		Vector3 mouse_pos = camera.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 0.0f));
		Vector3 touch_pos = Input.touchCount > 0 ? 
			camera.ScreenToWorldPoint (new Vector3 (Input.GetTouch (0).position.x, Input.GetTouch (0).position.y, 0.0f)) :
			new Vector3 (-255.0f, -255.0f, 0.0f);

		// Mouse or touchpad pressing 
		if(Input.GetMouseButtonDown (0) || (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began))
		{
			if (rule_frame.Contains (new Vector2 (mouse_pos.x, mouse_pos.y), true)
				|| rule_frame.Contains (new Vector2 (touch_pos.x, touch_pos.y), true)) {
				rule_button.GetComponent<SpriteRenderer> ().enabled = false;
				rule_button_clicked.GetComponent<SpriteRenderer> ().enabled = true;
			}

			if (order_frame.Contains (new Vector2 (mouse_pos.x, mouse_pos.y), true)
				|| order_frame.Contains (new Vector2 (touch_pos.x, touch_pos.y), true)) {
				order_button.GetComponent<SpriteRenderer> ().enabled = false;
				order_button_clicked.GetComponent<SpriteRenderer> ().enabled = true;
			}

			// Determine which check box is clicked 
			RaycastHit2D hit_mouse = Physics2D.Raycast(camera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero); 
			RaycastHit2D hit_touch = Physics2D.Raycast(camera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero); 
			if (hit_mouse.collider != null) {
				Debug.Log (hit_mouse.collider.gameObject.name);
				if (hit_mouse.collider.gameObject.GetComponent<SpriteRenderer> ().enabled) {
					hit_mouse.collider.gameObject.GetComponent<SpriteRenderer> ().enabled = false;

					// Forward click event to callback
					OnCheckBoxClicked(hit_mouse.collider.gameObject,-1);
				}
				else {
					hit_mouse.collider.gameObject.GetComponent<SpriteRenderer> ().enabled = true;

					// Forward click event to callback
					OnCheckBoxClicked(hit_mouse.collider.gameObject,1);

				}
			}
			else if(hit_touch.collider != null)
			{
				Debug.Log (hit_touch.collider.gameObject.name);
				if (hit_touch.collider.gameObject.GetComponent<SpriteRenderer> ().enabled) {
					hit_touch.collider.gameObject.GetComponent<SpriteRenderer> ().enabled = false;

					// Forward click event to callback
					OnCheckBoxClicked(hit_touch.collider.gameObject,-1);
				}
				else {
					hit_touch.collider.gameObject.GetComponent<SpriteRenderer> ().enabled = true;

					// Forward click event to callback
					OnCheckBoxClicked(hit_touch.collider.gameObject,1);
				}
			}
		}

		// Mouse or touchpad releaseing
		if (Input.GetMouseButtonUp (0) || (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Ended)) {
			if (rule_frame.Contains (new Vector2 (mouse_pos.x, mouse_pos.y), true)
				|| rule_frame.Contains (new Vector2 (touch_pos.x, touch_pos.y), true)) {
				rule_button.GetComponent<SpriteRenderer> ().enabled = true;
				rule_button_clicked.GetComponent<SpriteRenderer> ().enabled = false;

				// Rule button callback
				OnRuleButton ();
			}

			if (order_frame.Contains (new Vector2 (mouse_pos.x, mouse_pos.y), true)
				|| order_frame.Contains (new Vector2 (touch_pos.x, touch_pos.y), true)) {
				order_button.GetComponent<SpriteRenderer> ().enabled = true;
				order_button_clicked.GetComponent<SpriteRenderer> ().enabled = false;

				// Order button callback
				OnOrderButton ();
			}
		}
	}
}
