using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour {
	public GameObject start;
	public GameObject exit;
	public GameObject start_clicked;
	public GameObject exit_clicked;

	private int start_button_state;
	private int exit_button_state;
	private int start_highlight_state;
	private int exit_highlight_state;
	public Rect start_rect, exit_rect;

	public new Camera camera;

	// @params : Button type 
	// @return : void
	// @brif : Display button pressing effect
	void ButtonPressed(int button_type)
	{
		switch (button_type) {
		case GlobalVariables.START_BUTTON:
			start_clicked.transform.position += GlobalVariables.click_offset;
			break;
		case GlobalVariables.EXIT_BUTTON:
			exit_clicked.transform.position += GlobalVariables.click_offset;
			break;
		default:
			break;
		}
	}

	// @params : Button type 
	// @return : void
	// @brif : Display button releasing effect
	void ButtonReleased(int button_type)
	{
		switch (button_type) {
		case GlobalVariables.START_BUTTON:
			start_clicked.transform.position -= GlobalVariables.click_offset;
			break;
		case GlobalVariables.EXIT_BUTTON:
			exit_clicked.transform.position -= GlobalVariables.click_offset;
			break;
		default:
			break;
		}
	}

	// @params : void
	// @return : void
	// @brif : Message handler of clicking event on start button
	void OnStartButton()
	{
		start_clicked.GetComponent<AudioSource> ().Play ();
		StartCoroutine(GetComponent<SceneFadeInOut> ().Fading ("Scene_cold"));
	}

	// @params : void
	// @return : void
	// @brif : Message handler of clicking event on exit button
	void OnExitButton()
	{
		exit_clicked.GetComponent<AudioSource> ().Play ();
		// Exit the game
		Application.Quit();
	}

	// Use this for initialization
	void Start () {

		start_button_state = GlobalVariables.BUTTON_RELEASED;
		exit_button_state = GlobalVariables.BUTTON_RELEASED;
		start_highlight_state = GlobalVariables.HIGHLIGHT_DISABLED;
		exit_highlight_state = GlobalVariables.HIGHLIGHT_DISABLED;

		start_rect = new Rect (
			start.transform.position.x - start.GetComponent<SpriteRenderer>().bounds.size.x / 2,
			start.transform.position.y + start.GetComponent<SpriteRenderer>().bounds.size.y / 2,
			start.GetComponent<SpriteRenderer>().bounds.size.x,
			-start.GetComponent<SpriteRenderer>().bounds.size.y
		);

		exit_rect = new Rect (
			exit.transform.position.x - exit.GetComponent<SpriteRenderer>().bounds.size.x / 2,
			exit.transform.position.y + exit.GetComponent<SpriteRenderer>().bounds.size.y / 2,
			exit.GetComponent<SpriteRenderer>().bounds.size.x,
			-exit.GetComponent<SpriteRenderer>().bounds.size.y
		);
			
	}

	// Update is called once per frame
	void Update () {
		Vector3 mouse_pos = camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,0.0f));
		Vector3 touch_pos = Input.touchCount > 0 ? 
			camera.ScreenToWorldPoint (new Vector3 (Input.GetTouch (0).position.x, Input.GetTouch (0).position.y, 0.0f)) :
			new Vector3 (-255.0f,-255.0f,0.0f);

		if (start_rect.Contains (new Vector2(mouse_pos.x,mouse_pos.y),true) || 
			start_rect.Contains(new Vector2(touch_pos.x,touch_pos.y),true)) {

			start.SetActive (false);
			start_clicked.SetActive (true);

			start_highlight_state = GlobalVariables.START_HIGHLIGHT_ENABLED;
		} else {
			start.SetActive (true);
			start_clicked.SetActive (false);

			start_highlight_state = GlobalVariables.HIGHLIGHT_DISABLED;
		}
		if (exit_rect.Contains (new Vector2(mouse_pos.x,mouse_pos.y),true) ||
			exit_rect.Contains(new Vector2(touch_pos.x,touch_pos.y),true)) {

			exit.SetActive (false);
			exit_clicked.SetActive (true);

			exit_highlight_state = GlobalVariables.EXIT_HIGHLIGHT_ENABLED;
		} else {
			exit.SetActive (true);
			exit_clicked.SetActive (false);

			exit_highlight_state = GlobalVariables.HIGHLIGHT_DISABLED;
		}

		if (start_highlight_state == GlobalVariables.START_HIGHLIGHT_ENABLED && Input.GetMouseButtonDown (0) 
			|| (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)) {
			// Clicking start button
			start_button_state = GlobalVariables.BUTTON_CLICKED;

			ButtonPressed (GlobalVariables.START_BUTTON);
		}

		if (exit_highlight_state == GlobalVariables.EXIT_HIGHLIGHT_ENABLED && Input.GetMouseButtonDown (0)
			|| (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)) {
			// Clicking exit button

			exit_button_state = GlobalVariables.BUTTON_CLICKED;

			ButtonPressed (GlobalVariables.EXIT_BUTTON);
		}

		if (start_button_state == GlobalVariables.BUTTON_CLICKED && Input.GetMouseButtonUp (0) 
			|| (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Ended)) {

			start_button_state = GlobalVariables.BUTTON_RELEASED;

			ButtonReleased (GlobalVariables.START_BUTTON);

			start_button_state = GlobalVariables.BUTTON_RELEASED;

			OnStartButton ();
		}

		if (exit_button_state == GlobalVariables.BUTTON_CLICKED && Input.GetMouseButtonUp (0) 
			|| (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Ended)) {

			exit_button_state = GlobalVariables.BUTTON_RELEASED;

			ButtonReleased (GlobalVariables.EXIT_BUTTON);

			exit_button_state = GlobalVariables.BUTTON_RELEASED;

			OnExitButton ();
		}
	}
}