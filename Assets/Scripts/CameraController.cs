using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public GameObject player;
	private Vector3 offset;
	private GUILayer gui_layer;
	private new Camera camera;

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

		gui_layer = GetComponent<GUILayer> ();

		camera = GetComponent<Camera> ();
		offset = transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
		transform.position = player.transform.position + offset;

		if (Input.touchCount > 0 || Input.GetMouseButtonDown (0)) {
			Vector3 pos = camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,0.0f));
			ForwardClick (new Vector2(pos.x,pos.y));
		}
	}
}
