using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabtopHighlighter : MonoBehaviour {

	public new SpriteRenderer renderer;
	public GameObject player;
	public Animator animator;

	public static bool m_CurrentState;
	public bool NotUsed = false;
	public static Rect frame;

	// Use this for initialization
	void Start () {
		m_CurrentState = false;

		player = GameObject.Find ("Player");

		animator = GetComponent<Animator> ();
		renderer = GetComponent<SpriteRenderer> ();

		frame = new Rect (
			transform.position.x - GetComponent<SpriteRenderer>().bounds.size.x / 2,
			transform.position.y + GetComponent<SpriteRenderer>().bounds.size.y / 2,
			GetComponent<SpriteRenderer>().bounds.size.x,
			-GetComponent<SpriteRenderer>().bounds.size.y
		);
	}

	// Update is called once per frame
	void Update () {
		if (!NotUsed) {
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
}
