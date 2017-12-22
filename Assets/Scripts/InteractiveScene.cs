using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InteractiveScene : MonoBehaviour {

	public GameObject player;
	public GameObject labtop;
	public GameObject table;
	public GameObject watchmovie;
	public GameObject maketie;
	public GameObject makeflower;

	public Image fade_image;

	private new SpriteRenderer renderer;

	private float last_time;

	public PlayerController controller;

	IEnumerator FadingUnload(string Scene_name)
	{
		yield return new WaitForSeconds (GameObject.Find("blackfading").GetComponent<FadingController>().BeginFade(1));

		SceneManager.UnloadSceneAsync (SceneManager.GetSceneByName(Scene_name));

		yield return new WaitForSeconds (GameObject.Find("blackfading").GetComponent<FadingController>().BeginFade(-1));
	}
		
	void OnSceneLoaded(Scene scene,LoadSceneMode mode)
	{
		GameObject.Find ("Player").GetComponent<SpriteRenderer> ().enabled = false;
	}

	void OnSceneunLoaded(Scene scene,LoadSceneMode mode)
	{
		GameObject.Find ("Player").GetComponent<SpriteRenderer> ().enabled = true;
	}

	// @params : void
	// @return : void
	// @brif : Rollback to Scene_cold when 5s passed after PlayScene()
	void SceneRollback()
	{
		StartCoroutine (FadingUnload("Scene_warm"));
		GlobalVariables.EnterWarmScene = false;
	}

	// @params : Scene type that is about to play
	// @return : void
	// @brif: Play the corresponding scene according to interactive widget 
	//        clicked by player in last scene
	void PlayScene(int Scenetype)
	{
		switch (Scenetype) {
		case GlobalVariables.INTERACTIVE_TYPE_INVALID:
			// Don't do anything, this should never happen
			Debug.Log("INTERACTIVE_TYPE_INVALID passed to Scene_warm");
			break;

		case GlobalVariables.INTERACTIVE_TYPE_CLOSE:
			// Play Cleaning up tie scene
			renderer = maketie.GetComponent<SpriteRenderer> ();
			renderer.enabled = true;
			break;

		case GlobalVariables.INTERACTIVE_TYPE_LABTOP:
			// Play watching movie scene
			renderer = table.GetComponent<SpriteRenderer> ();
			renderer.enabled = false;

			renderer = labtop.GetComponent<SpriteRenderer> ();
			renderer.enabled = false;

			renderer = watchmovie.GetComponent<SpriteRenderer> ();
			renderer.enabled = true;
			break;

		case GlobalVariables.INTERACTIVE_TYPE_VASE:
			// Play arranging flower scene
			renderer = makeflower.GetComponent<SpriteRenderer> ();
			renderer.enabled = true;
			break;

		default :
			break;
		}
	}

	// Use this for initialization
	void Start () {
		
		// Get reference of GameObject instance
		labtop = GameObject.Find ("labtop");
		table = GameObject.Find ("table");
		watchmovie = GameObject.Find ("watchmovie");
		maketie = GameObject.Find ("maketie");
		makeflower = GameObject.Find ("makeflower");
		last_time = Time.time;

		player = GameObject.Find ("Player");
		if (player == null)
			return;

		// Get PlayerController script in Scene_warm
		controller = (PlayerController)player.GetComponent (typeof(PlayerController));

		// Play corresponding scene 
		PlayScene(PlayerController.InteractiveScene);
	}

	// FixedUpdate is called in fixed time
	void FixedUpdate () {
		// Wait for 5s
		if (Time.time - last_time > 5.0f)
			SceneRollback ();
	}
}
