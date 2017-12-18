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
	public const int SCENE_DIRAY = 3;
	public const int PUZZLE_STAGE_1 = 4;

	// Animation type
	public const int PLAYER_IDLE = 0;
	public const int PLAYER_WALKING = 1;

	// Interactive flags
	public const int INTERACTIVE_ENABLE = 1;
	public const int INTERACTIVE_DISABLE = 0;

	// Interactive widget type
	public const int INTERACTIVE_TYPE_INVALID = -1;
	public const int INTERACTIVE_TYPE_CLOSE = 0;
	public const int INTERACTIVE_TYPE_LABTOP = 1;
	public const int INTERACTIVE_TYPE_DRAWER = 2;
	public const int INTERACTIVE_TYPE_VASE = 3;

	// Current player state
	private int playerState;

	// @params : interactive type
	// @return : void
	// @brif : Callback function to trigger scene switch when player 
	//		   click corresponding interactive widget
	public void InteractiveCallback(int type)
	{
		WidgetActiveNum++;
		InteractiveScene = type;

		if (type == INTERACTIVE_TYPE_DRAWER)
			SwitchScene (SCENE_DIRAY);
		else
			SwitchScene (SCENE_WARM);
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
			case SCENE_DIRAY:
				DontDestroyOnLoad (GameObject.Find("Main Camera"));
				SceneManager.LoadScene ("Diary");
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
		vase_highlight =GameObject.Find ("vase_highlight");

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
		
		if (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.D)) {
			SetPlayerState (PLAYER_WALKING);

			if (m_Progress == INTERACTIVE_ENABLE) {
				renderer.enabled = true;
				if (lying != null)
					lying.SetActive (false);
			} else {
				m_Progress = INTERACTIVE_ENABLE;
			
			}
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

