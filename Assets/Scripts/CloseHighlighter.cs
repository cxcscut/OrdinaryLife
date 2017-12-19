using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseHighlighter : MonoBehaviour {

	public new SpriteRenderer renderer;
	public GameObject player;
	public Animator animator;

	public bool NotUsed = false;

	public static bool m_CurrentState;

	public static Rect frame = new Rect(-7.5f,0.5f,0.9f,-1.2f);

	// Use this for initialization
	void Start () {
		m_CurrentState = false;

		player = GameObject.Find ("Player");

		animator = GetComponent<Animator> ();
		renderer = GetComponent<SpriteRenderer> ();
	}

	// Update is called once per frame
	void Update () {
		if (!NotUsed) {
			if (Mathf.Abs (player.transform.position.x - transform.position.x) <= 0.6f) {
				renderer.enabled = true;
				animator.Play ("close_highlight");
				m_CurrentState = true;
			} else {
				renderer.enabled = false;
				m_CurrentState = false;
			}
		}
	}
}
