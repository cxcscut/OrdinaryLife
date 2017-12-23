using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Chapter2Stage2Controller : MonoBehaviour {

	public GameObject shot6;
	public GameObject shot7;
	public GameObject shot8;
	public GameObject shot9;

	public GameObject option1;
	public GameObject option2;
	public GameObject option3;

	private bool auto_play = false;

	IEnumerator AutoPlay()
	{
		yield return new WaitForSeconds (GlobalVariables.AutoPlayTimeInterval);

		InteractiveCallback (1);

		yield return new WaitForSeconds (GlobalVariables.AutoPlayTimeInterval);

		InteractiveCallback (2);

		yield return new WaitForSeconds (GlobalVariables.AutoPlayTimeInterval);

		InteractiveCallback (3);

		yield return new WaitForSeconds (GlobalVariables.AutoPlayTimeInterval);

		InteractiveCallback (4);
	}

	IEnumerator Fading(string Scene_name,LoadSceneMode mode)
	{
		yield return new WaitForSeconds (GameObject.Find("blackfading").GetComponent<FadingController>().BeginFade(1));

		SceneManager.LoadScene (Scene_name,mode);

		yield return new WaitForSeconds (GameObject.Find("blackfading").GetComponent<FadingController>().BeginFade(-1));
	}

	IEnumerator ShowOption()
	{
		yield return new WaitForSeconds (2.0f);

		option1.GetComponent<SpriteRenderer> ().enabled = true;
		option2.GetComponent<SpriteRenderer> ().enabled = true;
		option3.GetComponent<SpriteRenderer> ().enabled = true;
	}

	void InteractiveCallback(int shot)
	{
		switch (shot) {
		case 1:
			// Shot #6
			shot6.GetComponent<Animator>().Play("Chapter2_shot6");
			break;
		case 2:
			// Shot #7
			shot7.GetComponent<Animator>().Play("Chapter2_shot7");
			break;
		case 3:
			// Shot #8
			shot8.GetComponent<Animator>().Play("Chapter2_shot8");
			break;
		case 4:
			// Shot #9
			shot9.GetComponent<Animator>().Play("Chapter2_shot9");

			StartCoroutine(ShowOption());

			GlobalVariables.Shot9Active = true;
			break;
		default :
			break;
		}

	}
		
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (!auto_play) {
			auto_play = true;
			StartCoroutine (AutoPlay());
		}
	}
}
