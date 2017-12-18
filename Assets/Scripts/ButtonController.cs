using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour {
	public GameObject start;
	public GameObject exit;
	public GameObject start_clicked;
	public GameObject exit_clicked;

	private const int HIGHLIGHT_ENABLED = 0;
	private const int HIGHLIGHT_DISABLED = 1;
	private const int START_BUTTON = 1;
	private const int EXIT_BUTTON = 0;
	private const int BUTTON_CLICKED = 0;
	private const int BUTTON_RELEASED = 1;

	private Vector3 click_offset = new Vector3(0.1f,-0.1f,0.0f);
	private int start_button_state;
	private int exit_button_state;
	private int highlight_state;
	public Rect start_rect, exit_rect;

	public new Camera camera;

	// @params : Button type 
	// @return : void
	// @brif : Display button pressing effect
	void ButtonPressed(int button_type)
	{
		switch (button_type) {
		case START_BUTTON:
			start_clicked.transform.position += click_offset;
			break;
		case EXIT_BUTTON:
			exit_clicked.transform.position += click_offset;
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
		case START_BUTTON:
			start_clicked.transform.position -= click_offset;
			break;
		case EXIT_BUTTON:
			exit_clicked.transform.position -= click_offset;
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
		SceneManager.LoadScene ("Scene_cold");
	}

	// @params : void
	// @return : void
	// @brif : Message handler of clicking event on exit button
	void OnExitButton()
	{
		// Exit the game
		Application.Quit();
	}

	// Use this for initialization
	void Start () {

		start_button_state = BUTTON_RELEASED;
		exit_button_state = BUTTON_RELEASED;
		highlight_state = HIGHLIGHT_DISABLED;

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

		Debug.Log (start_rect);
		Debug.Log (exit_rect);
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

			highlight_state = HIGHLIGHT_ENABLED;

		} else {
			start.SetActive (true);
			start_clicked.SetActive (false);
		}

		if (highlight_state == HIGHLIGHT_ENABLED && Input.GetMouseButtonDown (0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)) {
			// Clicking start button
			Debug.Log("Start button clicked");
			start_button_state = BUTTON_CLICKED;

			ButtonPressed (START_BUTTON);
		}

		if (exit_rect.Contains (new Vector2(mouse_pos.x,mouse_pos.y),true) ||
			exit_rect.Contains(new Vector2(touch_pos.x,touch_pos.y),true)) {

			exit.SetActive (false);
			exit_clicked.SetActive (true);

			highlight_state = HIGHLIGHT_ENABLED;
		} else {
			exit.SetActive (true);
			exit_clicked.SetActive (false);
		}

		if (highlight_state == HIGHLIGHT_ENABLED && Input.GetMouseButtonDown (0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)) {
			// Clicking exit button
			Debug.Log("Exit button clicked");

			exit_button_state = BUTTON_CLICKED;

			ButtonPressed (EXIT_BUTTON);
		}

		if (start_button_state == BUTTON_CLICKED && Input.GetMouseButtonUp (0) || (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Ended)) {
			Debug.Log ("Start button released");

			start_button_state = BUTTON_RELEASED;

			ButtonReleased (START_BUTTON);

			OnStartButton ();
		}

		if (exit_button_state == BUTTON_CLICKED && Input.GetMouseButtonUp (0) || (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Ended)) {
			Debug.Log ("Exit button released");

			exit_button_state = BUTTON_RELEASED;

			ButtonReleased (EXIT_BUTTON);

			OnExitButton ();
		}
	}
}