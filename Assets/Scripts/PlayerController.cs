using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	public static int InteractiveScene;

	public Animator animator;
	public new SpriteRenderer renderer;
	public GameObject lying;

	private int m_Progress;
	public float speed;
	private bool facingRight;
	private int CurrentScene;
	private int WidgetActiveNum;

	// Scene type
	public const int SCENE_COLD = 1;
	public const int SCENE_WARM = 2;
	public const int PUZZLE_STAGE = 3;

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

	PlayerController()
	{
		
	}

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
					SceneManager.LoadScene ("Scene_warm", LoadSceneMode.Additive);
				}					
				break;
			case PUZZLE_STAGE:
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

	// @params :  Player state type
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
		DontDestroyOnLoad (this);

		InteractiveScene = INTERACTIVE_TYPE_INVALID;
		facingRight = false;
		CurrentScene = SCENE_COLD;
		WidgetActiveNum = 0;
		playerState = PLAYER_IDLE;

		animator = GetComponent<Animator> ();
		renderer = GetComponent<SpriteRenderer> ();

		m_Progress = INTERACTIVE_DISABLE;
	}

	void Update()
	{

		if (Input.GetKeyDown (KeyCode.F))
			InteractiveCallback (INTERACTIVE_TYPE_VASE);

		if (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.D)) {
			SetPlayerState (PLAYER_WALKING);
			if (m_Progress == INTERACTIVE_ENABLE) {
				renderer.enabled = true;
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

