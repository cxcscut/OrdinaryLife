using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabtopHighlighter : MonoBehaviour {

	public new SpriteRenderer renderer;
	public GameObject player;
	public Animator animator;

	public static bool m_CurrentState;

	public static Rect frame = new Rect (-3.0f,-0.1f,1.1f,-0.6f);

	// Use this for initialization
	void Start () {
		m_CurrentState = false;

		player = GameObject.Find ("Player");

		animator = GetComponent<Animator> ();
		renderer = GetComponent<SpriteRenderer> ();
	}

	// Update is called once per frame
	void Update () {
		if (Mathf.Abs (player.transform.position.x - transform.position.x) <= 0.6f) {
			renderer.enabled = true;
			animator.Play ("labtop_highlight");
			m_CurrentState = true;
		} else {
			renderer.enabled = false;
			m_CurrentState = false;
		}
	}
}
