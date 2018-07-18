using UnityEngine;
using System.Collections;

public class FindLanderController : MonoBehaviour
{
	AsyncOperation levelLoader;
	AudioSource s2;
	AudioSource s2_1;
	bool shouldLoadNextLevel = false;

	void Start()
	{
		Cardboard.SDK.VRModeEnabled = AppState.getVRModeEnabled ();

		levelLoader = Application.LoadLevelAsync ("ShuttleInterior");
		levelLoader.allowSceneActivation = false;

		AudioSource ambient = gameObject.AddComponent<AudioSource> ();
		ambient.clip = Resources.Load ("Interior") as AudioClip;
		ambient.loop = true;
		ambient.Play ();
		
		s2 = gameObject.AddComponent<AudioSource> ();
		s2.clip = Resources.Load ("S2") as AudioClip;
		s2.PlayDelayed (2);
	}

	void Update ()
	{

		if (shouldLoadNextLevel)
		{
			if (!s2_1.isPlaying)
			{
				shouldLoadNextLevel = false;

				levelLoader.allowSceneActivation = true;
			}
		}
	}
	
	public void SetGazedAt(bool gazedAt) {
		if (gazedAt)
		{
			shouldLoadNextLevel = true;

			if (s2.isPlaying)
				s2.Stop();

			if (s2_1 == null)
			{
				s2_1 = gameObject.AddComponent<AudioSource> ();
				s2_1.clip = Resources.Load ("S2_1") as AudioClip;
				s2_1.Play (2);
			}
		}
	}
}
