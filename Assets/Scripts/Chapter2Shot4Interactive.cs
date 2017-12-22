using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Chapter2Shot4Interactive : MonoBehaviour {

	public new Camera camera;
	public GameObject highligher;

	private Rect frame;

	private bool EnterMenuGame = false;

	// Use this for initialization
	void Start () {
		frame = new Rect (
			transform.position.x - GetComponent<SpriteRenderer>().bounds.size.x / 2,
			transform.position.y + GetComponent<SpriteRenderer>().bounds.size.y / 2,
			GetComponent<SpriteRenderer>().bounds.size.x,
			-GetComponent<SpriteRenderer>().bounds.size.y
		);

		Debug.Log (frame);
	}

	IEnumerator Fading(string Scene_name,LoadSceneMode mode)
	{
		yield return new WaitForSeconds (GameObject.Find("blackfading").GetComponent<FadingController>().BeginFade(1));

		SceneManager.LoadScene (Scene_name,mode);

		yield return new WaitForSeconds (GameObject.Find("blackfading").GetComponent<FadingController>().BeginFade(-1));
	}

	// @params : void
	// @return : void
	// @brif : Go to Menu game when mouse or touch pad pressing the shot #4
	void GotoMenuGame()
	{
		StartCoroutine (Fading("MenuGame",LoadSceneMode.Additive));
	}

	// Update is called once per frame
	void Update () {

		if (GlobalVariables.Shot4Active) {

			// Mouse or touch pad pressed when active
			if (Input.GetMouseButtonDown (0) || (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began)) {
				Vector3 mouse_pos = camera.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 0.0f));
				Vector3 touch_pos = Input.touchCount > 0 ? 
				camera.ScreenToWorldPoint (new Vector3 (Input.GetTouch (0).position.x, Input.GetTouch (0).position.y, 0.0f)) :
				new Vector3 (-255.0f, -255.0f, 0.0f);

				// Click on Menu
				if (frame.Contains (new Vector2 (mouse_pos.x, mouse_pos.y),true) || frame.Contains (new Vector2 (touch_pos.x, touch_pos.y),true)) {

					if (!EnterMenuGame) {
						EnterMenuGame = true;
						// Switch scene to menu game
						GotoMenuGame ();
					}

					// Deactivated shot #4
					GlobalVariables.Shot4Active = false;
					highligher.GetComponent<SpriteRenderer> ().enabled = false;
					highligher.GetComponent<Animator> ().enabled = false;

					Debug.Log ("Go to menu game");
				}
			}
		}
	}
}
