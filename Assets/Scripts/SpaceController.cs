using UnityEngine;
using System.Collections;

public class SpaceController : MonoBehaviour
{
	AsyncOperation levelLoader;
	AudioSource s4;
	bool shouldLoadNextLevel = false;

	void Start ()
	{
		Cardboard.SDK.VRModeEnabled = AppState.getVRModeEnabled ();		//VR (stereoscopic) mode or not

		levelLoader = Application.LoadLevelAsync("Main");
		levelLoader.allowSceneActivation = false;

		//Audio guide script
		s4 = gameObject.AddComponent<AudioSource> ();
		s4.clip = Resources.Load ("S4") as AudioClip;					//play the audio resource stated
		s4.PlayDelayed (2);												//delay playing audio for the scene
	}
	
	void Update ()
	{

		if (shouldLoadNextLevel)
		{
			if (!s4.isPlaying)
			{
				shouldLoadNextLevel = false;
				
				levelLoader.allowSceneActivation = true;
			}
		}
	}
}
