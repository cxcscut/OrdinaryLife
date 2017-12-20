using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables : MonoBehaviour {
	
	public const int START_HIGHLIGHT_ENABLED = 1;
	public const int EXIT_HIGHLIGHT_ENABLED = 2;
	public const int HIGHLIGHT_DISABLED = 3;

	public const int START_BUTTON = 1;
	public const int EXIT_BUTTON = 0;
	public const int BUTTON_CLICKED = 0;
	public const int BUTTON_RELEASED = 1;

	// Scene type
	public const int SCENE_COLD = 1;
	public const int SCENE_WARM = 2;
	public const int SCENE_DIRAY = 3;

	// Animation type
	public const int PLAYER_IDLE = 0;
	public const int PLAYER_WALKING = 1;

	// Interactive flags
	public const int INTERACTIVE_ENABLE = 1;
	public const int INTERACTIVE_DISABLE = 0;

	// Interactive widget type
	public const int INTERACTIVE_TYPE_INVALID = -1;
	public const int INTERACTIVE_TYPE_CLOSE = 0;
	public const int INTERACTIVE_TYPE_LABTOP = 1;
	public const int INTERACTIVE_TYPE_DRAWER = 2;
	public const int INTERACTIVE_TYPE_VASE = 3;

	public const float text_rolling_speed = 0.025f;
	public const int MIN_ACTIVE_WIDGET = 2;

	public const float fading_speed = 0.5f;

	// Chapter #1
	public static bool LightGameFinished = false;

	// Wechat game stage
	public static float TextInterval = 1.0f;

	// Chapter #2
	public static bool Shot4Finished = false;
	public static bool Shot5Finished = false;
	public static bool Shot9Active = false;
	public static bool Shot4Active = false;
	public static bool WechatGameActive = false;

	// Scores
	public static float TotalScores = 0.0f;

}
