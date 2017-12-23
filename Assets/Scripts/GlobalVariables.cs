using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables : MonoBehaviour {

	// Resolution setting
	public const int SCREEN_WIDTH = 1280;
	public const int SCREEN_HEIGHT = 600;
	public const bool isFullScreen = false;

	public static Vector3 click_offset = new Vector3(0.1f,-0.1f,0.0f);

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

	public const float text_rolling_speed = 0.018f;
	public const int MIN_ACTIVE_WIDGET = 2;

	public const float fading_speed = 0.5f;

	// Auto play setting
	public const float AutoPlayTimeInterval = 2.0f;

	// Player control
	public const float MovingSpeed = 0.05f;

	// Scene #1
	public static bool EnterWarmScene = false;

	// Diary Scene
	public static int DiaryTextIndex = 1;

	// Wechat game stage
	public const float TextInterval = 1.5f;
	public const float UpMovingDistance = 1.1f;
	public const float UpMovingSpeed = 0.2f;
	public static bool WechatGameFinished = false;
	public static int WechatGameScores = 0;

	// Chapter #2
	public static bool Shot4Finished = false;
	public static bool Shot5Finished = false;
	public static bool Shot9Active = false;
	public static bool Shot4Active = false;
	public static bool WechatGameActive = false;

	// Option
	public static int OptionScores = 0;

	// Light game stage
	public const float TimeLimit = 120.0f;
	public static bool LightGameFinished = false;
	public static bool LightGameActive = true;
	public static int LightGameScores = 0;

	// Menu game stage
	public static bool MenuGameFinished = false;
	public const float OSDisplayTime = 2.0f;
	public static int MenuGameScores = 0;

	// Ending
	public static int EndingType = 3;
	public const float ProducerInfo_rollingspeed = 0.008f;
}
