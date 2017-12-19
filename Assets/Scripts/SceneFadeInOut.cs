using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneFadeInOut : MonoBehaviour {

	public Animator animator;
	public Image black;

	public IEnumerator Fading(string Scene_name)
	{
		animator.SetInteger ("FadeDir",-1);
		yield return new WaitUntil (()=>black.color.a >= 0.98f);

		SceneManager.LoadScene (Scene_name);
	}
		
}
