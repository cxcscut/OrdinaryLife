using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadingController : MonoBehaviour {

	public int FadingDir = -1;
	public new SpriteRenderer renderer;
	private float alpha = 0.0f;

	// Use this for initialization
	void Start () {
		renderer = GetComponent<SpriteRenderer> ();
	}

	// Update is called once per frame
	void Update () {
		alpha += FadingDir * GlobalVariables.fading_speed * Time.deltaTime;

		alpha = Mathf.Clamp01 (alpha);

		renderer.color = new Color (renderer.color.r,renderer.color.g,renderer.color.b,alpha);
	}

	public float BeginFade(int FadeDir)
	{
		FadingDir = FadeDir;

		return 1 / GlobalVariables.fading_speed;
	}
}
