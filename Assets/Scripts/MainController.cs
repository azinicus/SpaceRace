using UnityEngine;
using System.Collections;

public class MainController : MonoBehaviour
{
	public GameObject door;

	AsyncOperation levelLoader;
	float timeLimit = 2.0f; // 2 seconds
	float fadeAlpha = 0.25f;
	bool shouldFade = false;

	void Start()
	{
		Cardboard.SDK.VRModeEnabled = AppState.getVRModeEnabled ();				//VR (stereoscopic) mode or not

		levelLoader = Application.LoadLevelAsync ("FindLander");
		levelLoader.allowSceneActivation = false;

		//Ambient environmental audio playing in background
		AudioSource ambient = gameObject.AddComponent<AudioSource> ();
		ambient.clip = Resources.Load ("Interior") as AudioClip;
		ambient.loop = true;
		ambient.Play ();

		//Audio guide playing code
		AudioSource s1 = gameObject.AddComponent<AudioSource> ();
		s1.clip = Resources.Load ("S1") as AudioClip;
		s1.playOnAwake = true;
		s1.PlayDelayed (3);

		Color color = door.GetComponent<Renderer>().material.color;
		color.a = 0.25f;
		door.GetComponent<Renderer> ().material.color = color;
	}


	void Update()
	{

		if (shouldFade)
		{
			if (timeLimit > 0)
			{
				// decrease timeLimit.
				timeLimit -= Time.deltaTime;

				// update fade value
				float timeProgress = (2.0f - timeLimit) / 2.0f;
				fadeAlpha = 0.25f + timeProgress;

				UpdateFadeAlpha ();

				if (fadeAlpha >= 1.0f)
				{
					shouldFade = false;

					levelLoader.allowSceneActivation = true;
				}
			}
		}
	}

	//Exit button animation
	public void SetGazedAt(bool gazedAt)
	{
		if (gazedAt)
		{
			shouldFade = true;
		}
		else
		{
			shouldFade = false;
			timeLimit = 2.0f;
			fadeAlpha = 0.25f;

			UpdateFadeAlpha ();
		}
	}

	void UpdateFadeAlpha ()
	{
		Color color = door.GetComponent<Renderer> ().material.color;
		color.a = fadeAlpha;
		door.GetComponent<Renderer> ().material.color = color;
	}
}
