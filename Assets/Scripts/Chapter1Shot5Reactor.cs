using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Chapter1Shot5Reactor : MonoBehaviour {

	public new Camera camera;

	private bool EnterWechatGame = false;

	private Rect frame;

	IEnumerator Fading(string Scene_name,LoadSceneMode mode)
	{
		yield return new WaitForSeconds (GameObject.Find("blackfading").GetComponent<FadingController>().BeginFade(1));

		SceneManager.LoadScene (Scene_name,mode);

		yield return new WaitForSeconds (GameObject.Find("blackfading").GetComponent<FadingController>().BeginFade(-1));
	}
		
	void GotoWechatGame()
	{
		StartCoroutine (Fading("WechatGame",LoadSceneMode.Additive));
	}

	// Use this for initialization
	void Start () {
		frame = new Rect (
			transform.position.x - GetComponent<SpriteRenderer>().bounds.size.x / 2,
			transform.position.y + GetComponent<SpriteRenderer>().bounds.size.y / 2,
			GetComponent<SpriteRenderer>().bounds.size.x,
			-GetComponent<SpriteRenderer>().bounds.size.y
		);
	}
	
	// Update is called once per frame
	void Update () {
		if (GlobalVariables.WechatGameActive) {
			// Mouse or touch pad pressing at shot #5
			if (Input.GetMouseButtonDown (0) || (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began)) {
				Vector3 mouse_pos = camera.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 0.0f));
				Vector3 touch_pos = Input.touchCount > 0 ? 
					camera.ScreenToWorldPoint (new Vector3 (Input.GetTouch (0).position.x, Input.GetTouch (0).position.y, 0.0f)) :
					new Vector3 (-255.0f, -255.0f, 0.0f);

				// Click on shot #5
				if (frame.Contains (new Vector2 (mouse_pos.x, mouse_pos.y),true) || frame.Contains (new Vector2 (touch_pos.x, touch_pos.y),true)) {

					if (!EnterWechatGame && GlobalVariables.LightGameFinished) {

						GameObject.Find ("cellphone_highlight").GetComponent<SpriteRenderer>().enabled = false;

						EnterWechatGame = true;
						// Switch scene to wechatGame
						GotoWechatGame ();
					}

					Debug.Log ("Go to Wechat game");
				}
			}
		}
	}
}
