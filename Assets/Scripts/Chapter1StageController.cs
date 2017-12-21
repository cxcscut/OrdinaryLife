using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Chapter1StageController : MonoBehaviour {

	private int click_num = 1;

	public GameObject shot2;
	public GameObject shot3;
	public GameObject shot4;
	public GameObject shot5;
	public GameObject arrow;
	public new Camera camera;

	private bool EnterWechatGame = false;

	public Rect arrow_frame;

	void InteractiveCallback(int shot)
	{
		if (!GlobalVariables.LightGameFinished)
			return;
		
		switch (shot) {
		case 2:
			// Shot #2
			shot2.GetComponent<SpriteRenderer>().enabled = true;
			break;
		case 3:
			// Shot #3
			shot3.GetComponent<SpriteRenderer>().enabled =true;
			break;
		case 4:
			// Shot #4
			shot4.GetComponent<SpriteRenderer>().enabled =true;
			break;
		case 5:
			// Shot #5
			shot5.GetComponent<SpriteRenderer> ().enabled = true;
			GameObject.Find ("cellphone_highlight").GetComponent<SpriteRenderer>().enabled = true;
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
		if (Input.touchCount > 0 || Input.GetMouseButtonDown (0)) {
			if(GlobalVariables.LightGameFinished)
				InteractiveCallback (++click_num);
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

					if (!EnterWechatGame) {
						EnterWechatGame = true;
						StartCoroutine (GameObject.Find ("Chapter1_background").GetComponent<SceneFadeInOut> ().Fading ("Chapter2_1"));
					}
				}
			}
			else
				arrow.GetComponent<Animator> ().enabled = true;
				
		}
	}
}
