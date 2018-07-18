using UnityEngine;
using System.Collections;

public class IntroController : MonoBehaviour
{
	AsyncOperation levelLoader;
	float timeLimit = 10.0f; // in seconds
	bool shouldLoadMainAfterTimerExpires = true;

	void Start ()
	{
		levelLoader = Application.LoadLevelAsync ("Main");
		levelLoader.allowSceneActivation = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		timeLimit -= Time.deltaTime;

		if (shouldLoadMainAfterTimerExpires && timeLimit <= 0) 
		{
			levelLoader.allowSceneActivation = true;
			shouldLoadMainAfterTimerExpires = false;
		}

		//Touch screen to flip between Stereoscopic and normal camera
		if (Input.touchSupported)
		{
			if (Input.GetTouch (0).phase == TouchPhase.Ended)
			{
				Cardboard.SDK.VRModeEnabled = !Cardboard.SDK.VRModeEnabled;
				AppState.setVRModeEnabled (Cardboard.SDK.VRModeEnabled);
			}
		}
		else if (Input.GetKeyDown (KeyCode.V))
		{
			Cardboard.SDK.VRModeEnabled = !Cardboard.SDK.VRModeEnabled;
			AppState.setVRModeEnabled (Cardboard.SDK.VRModeEnabled);
		}
	}
}
