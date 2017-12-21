using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapter1Shot1Reactor : MonoBehaviour {

	private Rect frame;

	public GameObject highlight;
	public new Camera camera;

	void GotoLightGame()
	{
		GameObject.Find ("LightGame_highlight").GetComponent<SpriteRenderer> ().enabled = false;
	}

	// Use this for initialization
	void Start () {
		frame = new Rect (
			highlight.transform.position.x - highlight.GetComponent<SpriteRenderer>().bounds.size.x / 2,
			highlight.transform.position.y + highlight.GetComponent<SpriteRenderer>().bounds.size.y / 2,
			highlight.GetComponent<SpriteRenderer>().bounds.size.x,
			-highlight.GetComponent<SpriteRenderer>().bounds.size.y
		);
	}
	
	// Update is called once per frame
	void Update () {
		if (!GlobalVariables.LightGameFinished) {
			// Mouse or touch pad pressing at shot #1
			if (Input.GetMouseButtonDown (0) || (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began)) {
				Vector3 mouse_pos = camera.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 0.0f));
				Vector3 touch_pos = Input.touchCount > 0 ? 
					camera.ScreenToWorldPoint (new Vector3 (Input.GetTouch (0).position.x, Input.GetTouch (0).position.y, 0.0f)) :
					new Vector3 (-255.0f, -255.0f, 0.0f);

				// Click on shot #1
				if (frame.Contains (new Vector2 (mouse_pos.x, mouse_pos.y),true) || frame.Contains (new Vector2 (touch_pos.x, touch_pos.y),true)) {

					// Switch scene to lightgame
					GotoLightGame ();

					// Deactivated shot #4
					GlobalVariables.LightGameFinished = true;

					Debug.Log ("Go to light game");
				}
			}
		}
	}
}
