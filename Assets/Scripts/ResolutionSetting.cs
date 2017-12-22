using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionSetting : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Screen.SetResolution (GlobalVariables.SCREEN_WIDTH,GlobalVariables.SCREEN_HEIGHT,GlobalVariables.isFullScreen);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
