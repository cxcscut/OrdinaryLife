using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public static int InteractiveScene;
	public Image fade_image;
	// GameObject in currently active scene 
	public Animator animator;
	public new SpriteRenderer renderer;
	public GameObject lying;
	public GameObject player;
	public GameObject close_highlight;
	public GameObject draw_highlight;
	public GameObject vase_highlight;
	public GameObject labtop_highlight;

	public SpriteRenderer left_arrow_renderer, right_arrow_renderer;

	public AudioSource audio_walking;

	public int m_Progress;
	private bool facingRight;
	private int CurrentScene;
	public int WidgetActiveNum;

	// Current player state
	private int playerState;

	IEnumerator Fading(string Scene_name,LoadSceneMode mode)
	{
		yield return new WaitForSeconds (GameObject.Find("blackfading").GetComponent<FadingController>().BeginFade(1));

		SceneManager.LoadScene (Scene_name,mode);

		yield return new WaitForSeconds (GameObject.Find("blackfading").GetComponent<FadingController>().BeginFade(-1));
	}

	// @params : interactive type
	// @return : void
	// @brif : Callback function to trigger scene switch when player 
	//		   click corresponding interactive widget
	public void InteractiveCallback(int type)
	{
		WidgetActiveNum++;
		InteractiveScene = type;

		switch (type) {
		case GlobalVariables.INTERACTIVE_TYPE_CLOSE:
			if (!close_highlight.GetComponent<CloseHighlighter> ().NotUsed)
				close_highlight.GetComponent<CloseHighlighter> ().NotUsed = true;
			else
				return;
			break;
		case GlobalVariables.INTERACTIVE_TYPE_DRAWER:
			if (!draw_highlight.GetComponent<DrawHighLigter> ().NotUsed)
				draw_highlight.GetComponent<DrawHighLigter> ().NotUsed = true;
			else
				return;
			break;
		case GlobalVariables.INTERACTIVE_TYPE_LABTOP:
			if (!labtop_highlight.GetComponent<LabtopHighlighter> ().NotUsed )
				labtop_highlight.GetComponent<LabtopHighlighter> ().NotUsed = true;
			else
				return;
			break;
		case GlobalVariables.INTERACTIVE_TYPE_VASE:
			if(!vase_highlight.GetComponent<VaseHighligher> ().NotUsed)
				vase_highlight.GetComponent<VaseHighligher> ().NotUsed = true;
			else
				return;
			break;
		default:
			break;
		}

		if (type == GlobalVariables.INTERACTIVE_TYPE_DRAWER)
			SwitchScene (GlobalVariables.SCENE_DIRAY);
		else 
			SwitchScene (GlobalVariables.SCENE_WARM);
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
			case GlobalVariables.SCENE_WARM:
				if (!SceneManager.GetSceneByName ("Scene_warm").IsValid ()) {
					GlobalVariables.EnterWarmScene = true;
					StartCoroutine (Fading("Scene_warm",LoadSceneMode.Additive));
				}					
				break;
			case GlobalVariables.SCENE_DIRAY:
				DontDestroyOnLoad (GameObject.Find ("BGMplayer"));
				StartCoroutine (GameObject.Find ("background_cold").GetComponent<SceneFadeInOut> ().Fading ("Diary"));
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
		left_arrow_renderer.enabled = false;
		right_arrow_renderer.enabled = false;
		GameObject.Find ("notice").GetComponent<SpriteRenderer> ().enabled = false;

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

		transform.Translate (move_vector * GlobalVariables.MovingSpeed);
	}

	// @params : Player state type
	// @return : void
	// @brif : Change current state of player and play corresponding animation
	void SetPlayerState(int state)
	{
		switch (state) {
		case GlobalVariables.PLAYER_IDLE:

			if (audio_walking.isPlaying)
				audio_walking.Stop ();

			animator.Play ("Idle");
			break;
		case GlobalVariables.PLAYER_WALKING:
			
			if (!audio_walking.isPlaying)
				audio_walking.Play ();
			
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
		WidgetActiveNum = 0;

		// Initialize variables
		InteractiveScene = GlobalVariables.INTERACTIVE_TYPE_INVALID;
		facingRight = false;
		CurrentScene = GlobalVariables.SCENE_COLD;
		playerState = GlobalVariables.PLAYER_IDLE;
		m_Progress = GlobalVariables.INTERACTIVE_DISABLE;

		// Get componnents' reference of Player 
		animator = GetComponent<Animator> ();
		renderer = GetComponent<SpriteRenderer> ();
	}

	void Update()
	{
		
		if (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.D)) {
			SetPlayerState (GlobalVariables.PLAYER_WALKING);

			if (m_Progress == GlobalVariables.INTERACTIVE_ENABLE) {
				renderer.enabled = true;
				if (lying != null)
					lying.SetActive (false);
			} else {
				m_Progress = GlobalVariables.INTERACTIVE_ENABLE;
			
			}
		} else
			SetPlayerState (GlobalVariables.PLAYER_IDLE);
	}

	// Update is called once per frame
	void FixedUpdate () {

		switch (playerState) {
		case GlobalVariables.PLAYER_IDLE:
			break;
		case GlobalVariables.PLAYER_WALKING:
			Move ();
			break;
		}
	}
		
}

