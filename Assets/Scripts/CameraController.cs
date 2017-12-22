using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public GameObject player;
	private new Camera camera;
	private GameObject background;

	private float bound_min,bound_max;
	private float camera_width;

	// @params : Coordinates mouse clicked on the screen
	// @return : void
	// @brif : Forwarding click message to switch scenes
	void ForwardClick(Vector2 position)
	{
		if (GlobalVariables.EnterWarmScene)
			return;
		
		// Click on draw
		if (DrawHighLigter.frame.Contains (position,true) && DrawHighLigter.m_CurrentState) {
			player.GetComponent<PlayerController> ().InteractiveCallback (GlobalVariables.INTERACTIVE_TYPE_DRAWER);

			GameObject draw = GameObject.Find ("draw_highlight");

			if(draw != null)
				// Do not display highlighter
				draw.GetComponent<SpriteRenderer>().enabled = false;
		}

		// Click on close
		if (CloseHighlighter.frame.Contains (position,true) && CloseHighlighter.m_CurrentState) {
			player.GetComponent<PlayerController> ().InteractiveCallback (GlobalVariables.INTERACTIVE_TYPE_CLOSE);

			GameObject close = GameObject.Find ("close_highlight");

			if(close != null)
				// Do not display highlighter
				GameObject.Find ("close_highlight").GetComponent<SpriteRenderer>().enabled = false;
		}

		// Click on labtop
		if (LabtopHighlighter.frame.Contains (position,true) && LabtopHighlighter.m_CurrentState) {
			player.GetComponent<PlayerController> ().InteractiveCallback (GlobalVariables.INTERACTIVE_TYPE_LABTOP);

			GameObject labtop = GameObject.Find ("labtop_highlight");

			if(labtop != null)
				// Do not display highlighter
				GameObject.Find ("labtop_highlight").GetComponent<SpriteRenderer>().enabled = false;
		}

		// Click on vase
		if (VaseHighligher.frame.Contains (position,true) && VaseHighligher.m_CurrentState) {
			player.GetComponent<PlayerController> ().InteractiveCallback (GlobalVariables.INTERACTIVE_TYPE_VASE);

			GameObject vase = GameObject.Find ("vase_highlight");

			if(vase != null)
				// Do not display highlighter
				GameObject.Find ("vase_highlight").GetComponent<SpriteRenderer>().enabled = false;
		}
	}

	// Use this for initialization
	void Start () {

		background = GameObject.Find ("background_cold");

		bound_min = -background.GetComponent<SpriteRenderer> ().bounds.size.x / 2 + background.transform.position.x;
		bound_max = background.GetComponent<SpriteRenderer> ().bounds.size.x / 2 + background.transform.position.x;

		camera = GetComponent<Camera> ();
		camera_width = 2 * camera.orthographicSize * camera.aspect;

	}

	// Update is called once per frame
	void Update () {
		if(player != null)
			transform.position = new Vector3 (player.transform.position.x,transform.position.y,transform.position.z);

		float camera_min = -camera_width / 2 + transform.position.x;
		float camera_max = camera_width / 2 + transform.position.x;
	
		if (camera_min < bound_min)
			transform.position = new Vector3 (bound_min + camera_width/2,transform.position.y,transform.position.z);
		
		if(camera_max > bound_max)
			transform.position = new Vector3 (bound_max - camera_width/2,transform.position.y,transform.position.z);

		if (Input.touchCount > 0 || Input.GetMouseButtonDown (0)) {
			Vector3 pos = camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,0.0f));
			Debug.Log (new Vector2(pos.x,pos.y));
			ForwardClick (new Vector2(pos.x,pos.y));
		}
	}
}
