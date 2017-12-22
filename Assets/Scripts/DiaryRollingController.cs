using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaryRollingController : MonoBehaviour {

	public GameObject text1,text2;

	// Use this for initialization
	void Start () {
		if (GlobalVariables.DiaryTextIndex == 1)
			text1.SetActive (true);
		else if(GlobalVariables.DiaryTextIndex == 2)
			text2.SetActive (true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
