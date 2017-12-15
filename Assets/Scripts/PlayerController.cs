using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	public static int InteractiveScene;

	// GameObject in currently active scene 
	public Animator animator;
	public new SpriteRenderer renderer;
	public GameObject lying;
	public GameObject player;
	public GameObject close_highlight;
	public GameObject draw_highlight;
	public GameObject vase_highlight;
	public GameObject labtop_highlight;

	public int m_Progress;
	public float speed;
	private bool facingRight;
	private int CurrentScene;
	private int WidgetActiveNum;

	// Scene type
	public const int SCENE_COLD = 1;
	public const int SCENE_WARM = 2;
	public const int PUZZLE_STAGE_1 = 3;

	// Animation type
	public const int PLAYER_IDLE = 0;
	public const int PLAYER_WALKING = 1;

	// Interactive flags
	public const int INTERACTIVE_ENABLE = 1;
	public const int INTERACTIVE_DISABLE = 0;

	// Current player state
	private int playerState;

	// Interactive widget type
	public const int INTERACTIVE_TYPE_INVALID = -1;
	public const int INTERACTIVE_TYPE_WARDROBE = 0;
	public const int INTERACTIVE_TYPE_LAPTOP = 1;
	public const int INTERACTIVE_TYPE_DRAWER = 2;
	public const int INTERACTIVE_TYPE_VASE = 3;

	// Distance of highlighting interactive widgets
	private const float MIN_HIGHLIGHT_DISTANCE = 2.0f;

	// @params : interactive type
	// @return : void
	// @brif : Callback function to trigger scene switch when player 
	//		   click corresponding interactive widget
	void InteractiveCallback(int type)
	{
		WidgetActiveNum++;
		InteractiveScene = type;

		SwitchScene (SCENE_WARM);
	}

	// @params : two position coordinate
	// @return : distance between two coordinates
	// @brif : Computing distance between two coordinates
	float SquareDistance(Vector3 vec1,Vector3 vec2)
	{
		return vec1.x * vec1.x + vec2.y * vec2.y + vec1.z * vec2.z;
	}

	// @params : void
	// @return : void
	// @brif : Highlight interactive widget if distance less than MIN_HIGHLIGHT_DISTANCE
	void HighLightWidget()
	{
		SpriteRenderer highlighter_renderer;
		if (SquareDistance (transform.position, close_highlight.transform.position) <= MIN_HIGHLIGHT_DISTANCE) {
			// Display highlighter
			highlighter_renderer = close_highlight.GetComponent<SpriteRenderer> ();
			highlighter_renderer.enabled = true;

			// Play animation
			close_highlight.GetComponent<Animator> ().Play ("close_highlight");

		} else {
			// Do not display highlighter
			highlighter_renderer = close_highlight.GetComponent<SpriteRenderer> ();
			highlighter_renderer.enabled = false;
		}
		if (SquareDistance (transform.position, labtop_highlight.transform.position) <= MIN_HIGHLIGHT_DISTANCE) {
			// Display highlighter
			highlighter_renderer = labtop_highlight.GetComponent<SpriteRenderer> ();
			highlighter_renderer.enabled = true;

			// Play animation
			labtop_highlight.GetComponent<Animator> ().Play ("labtop_highlight");
		} else {
			// Do not display highlighter
			highlighter_renderer = labtop_highlight.GetComponent<SpriteRenderer> ();
			highlighter_renderer.enabled = false;
		}
		if (SquareDistance (transform.position, draw_highlight.transform.position) <= MIN_HIGHLIGHT_DISTANCE) {
			// Display highlighter
			highlighter_renderer = draw_highlight.GetComponent<SpriteRenderer> ();
			highlighter_renderer.enabled = true;

			// Play animation
			draw_highlight.GetComponent<Animator> ().Play ("draw_highlight");
		} else {
			// Do not display highlighter
			highlighter_renderer = draw_highlight.GetComponent<SpriteRenderer> ();
			highlighter_renderer.enabled = false;
		}
		if (SquareDistance (transform.position, vase_highlight.transform.position) <= MIN_HIGHLIGHT_DISTANCE) {
			// Display highlighter
			highlighter_renderer = vase_highlight.GetComponent<SpriteRenderer> ();
			highlighter_renderer.enabled = true;

			// Play animation
			vase_highlight.GetComponent<Animator> ().Play ("vase_highlight");
		} else {
			// Do not display highlighter
			highlighter_renderer = vase_highlight.GetComponent<SpriteRenderer> ();
			highlighter_renderer.enabled = false;
		}
	}

	// @params : void
	// @return : void
	// @brif : Flip x-axis of sprite to match the moving direction
	void TurnAround()
	{
		Vector3 theScale = transform.localScale;

		theScale.x *= -1;

		transform.localScale = theScale;
	}

	// @params : Scene type
	// @return : void
	// @brif : Switch between current scene and target scene
	void SwitchScene(int scene)
	{
		if (CurrentScene != scene) {
			switch(scene)
			{
			case SCENE_WARM:
				if (!SceneManager.GetSceneByName ("Scene_warm").IsValid ()) {
					renderer.enabled = false;
					SceneManager.LoadScene ("Scene_warm",LoadSceneMode.Additive);
				}					
				break;
			case PUZZLE_STAGE_1:
				break;
			default:
				break;
			}
		}
	}

	// @params : void
	// @return : void
	// @brif : Moving player sprite along x-axis from input device
	void Move()
	{
		float move= Input.GetAxis ("Horizontal");

		if (move > 0.0f) {
			if (!facingRight) {
				facingRight = true;
				TurnAround ();
			}
		} else if (move < 0.0f) {
			if (facingRight) {
				facingRight = false;
				TurnAround ();
			}
		}

		Vector2 move_vector = new Vector2 (move, 0.0f);

		transform.Translate (move_vector * speed);
	}

	// @params : Player state type
	// @return : void
	// @brif : Change current state of player and play corresponding animation
	void SetPlayerState(int state)
	{
		switch (state) {
		case PLAYER_IDLE:
			animator.Play ("Idle");
			break;
		case PLAYER_WALKING:
			animator.Play ("Walking");
			break;
		}

		playerState = state;
	}

	void Start()
	{
		// Get instance of GameObject
		player = GameObject.Find ("Player");
		lying = GameObject.Find("lying");
		close_highlight = GameObject.Find("close_highlight");
		labtop_highlight = GameObject.Find("labtop_highlight");
		draw_highlight = GameObject.Find ("draw_highlight");
		vase_highlight =GameObject.Find ("vase_highligh");

		// Initialize variables
		InteractiveScene = INTERACTIVE_TYPE_INVALID;
		facingRight = false;
		CurrentScene = SCENE_COLD;
		WidgetActiveNum = 0;
		playerState = PLAYER_IDLE;
		m_Progress = INTERACTIVE_DISABLE;

		// Get componnents' reference of Player 
		animator = GetComponent<Animator> ();
		renderer = GetComponent<SpriteRenderer> ();
	}

	void Update()
	{
		if (Input.GetKeyDown (KeyCode.F))
			InteractiveCallback (INTERACTIVE_TYPE_VASE);
		
		if (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.D)) {
			SetPlayerState (PLAYER_WALKING);

			if (m_Progress == INTERACTIVE_ENABLE) {
				renderer.enabled = true;
				if(lying != null)
					lying.SetActive (false);
			} else
				m_Progress = INTERACTIVE_ENABLE;
		} else
			SetPlayerState (PLAYER_IDLE);
	}

	// Update is called once per frame
	void FixedUpdate () {
		
		switch (playerState) {
		case PLAYER_IDLE:
			break;
		case PLAYER_WALKING:
			Move ();
			break;
		}
	}
		
}

