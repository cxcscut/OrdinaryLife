using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public GameObject player;
	private new Camera camera;
	private GameObject background;

	private float bound_min,bound_max;
	private float camera_width;

	private const int CLOSE_WIDGET_CLICKED = 0;
	private const int LABTOP_WIDGET_CLICKED = 1;
	private const int DRAW_WIDGET_CLICKED = 2;
	private const int VASE_WIDGET_CLICKED = 3;

	public static Vector3 CAMERA_POS_FLOWER = new Vector3(1.13f,-0.16f,-10.0f);
	public static Vector3 CAMERA_POS_WATCHMOVIE = new Vector3(-2.03f,-0.07f,-10.0f);
	public static Vector3 CAMERA_POS_CLEANTIE = new Vector3(-2.46f,-0.14f,-10.0f);

	// @params : Coordinates mouse clicked on the screen
	// @return : void
	// @brif : Forwarding click message to switch scenes
	void ForwardClick(Vector2 position)
	{

		// Click on draw
		if (DrawHighLigter.frame.Contains (position,true) && DrawHighLigter.m_CurrentState) {
			player.GetComponent<PlayerController> ().InteractiveCallback (PlayerController.INTERACTIVE_TYPE_DRAWER);

			// Do not display highlighter
			GameObject.Find ("draw_highlight").GetComponent<SpriteRenderer>().enabled = false;
		}

		// Click on close
		if (CloseHighlighter.frame.Contains (position,true) && CloseHighlighter.m_CurrentState) {
			player.GetComponent<PlayerController> ().InteractiveCallback (PlayerController.INTERACTIVE_TYPE_CLOSE);

			// Do not display highlighter
			GameObject.Find ("close_highlight").GetComponent<SpriteRenderer>().enabled = false;
		}

		// Click on labtop
		if (LabtopHighlighter.frame.Contains (position,true) && LabtopHighlighter.m_CurrentState) {
			player.GetComponent<PlayerController> ().InteractiveCallback (PlayerController.INTERACTIVE_TYPE_LABTOP);

			// Do not display highlighter
			GameObject.Find ("labtop_highlight").GetComponent<SpriteRenderer>().enabled = false;
		}

		// Click on vase
		if (VaseHighligher.frame.Contains (position,true) && VaseHighligher.m_CurrentState) {
			player.GetComponent<PlayerController> ().InteractiveCallback (PlayerController.INTERACTIVE_TYPE_VASE);

			// Do not display highlighter
			GameObject.Find ("vase_highlight").GetComponent<SpriteRenderer>().enabled = false;
		}
	}

	// Use this for initialization
	void Start () {

		background = GameObject.Find ("background_cold");

		bound_min = -background.GetComponent<SpriteRenderer> ().bounds.size.x / 2 + background.transform.position.x;
		bound_max = background.GetComponent<SpriteRenderer> ().bounds.size.x / 2 + background.transform.position.x;

		Debug.Log (bound_min);
		Debug.Log (bound_max);

		camera = GetComponent<Camera> ();
		camera_width = 2 * camera.orthographicSize * camera.aspect;

	}

	// Update is called once per frame
	void Update () {
		if(player != null)
			transform.position = new Vector3 (player.transform.position.x,transform.position.y,transform.position.z);

		float camera_min = -camera_width / 2 + transform.position.x;
		float camera_max = camera_width / 2 + transform.position.x;

		Debug.Log (camera_min);
		Debug.Log (camera_max);
	
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
