using UnityEngine;
using System.Collections;

public class ShuttleInteriorController : MonoBehaviour
{
	AsyncOperation levelLoader;
	bool shouldLoadNextLevel = true;

	AudioSource s3;
	float s3Time = 0.0f;

	Vector3 originalCameraRot;

	public Transform cameraTransform;
	
	// How long the object should shake for.
	public float shake = 1000.0f;
	
	// Amplitude of the shake. A larger value shakes the camera harder.
	public float shakeAmount = 0.5f;
	public float decreaseFactor = 1.0f;

	void Start()
	{
		Cardboard.SDK.VRModeEnabled = AppState.getVRModeEnabled ();

		levelLoader = Application.LoadLevelAsync ("SpaceNew");
		levelLoader.allowSceneActivation = false;

		AudioSource ambient = gameObject.AddComponent<AudioSource> ();
		ambient.clip = Resources.Load ("room_tone") as AudioClip;
		ambient.loop = true;
		ambient.Play ();
		
		s3 = gameObject.AddComponent<AudioSource> ();
		s3.clip = Resources.Load ("S3") as AudioClip;
		s3.PlayDelayed (2);

		originalCameraRot = cameraTransform.localEulerAngles;
	}

	void Update ()
	{

		s3Time += Time.deltaTime;

		if (shouldLoadNextLevel)
		{
			if (!s3.isPlaying)
			{
				shouldLoadNextLevel = false;

				levelLoader.allowSceneActivation = true;
			}
		}

		if (s3Time > 15.0f) // seconds until audio is at "BLAST OFF"
		{
			if (shake > 0)
			{
				cameraTransform.localEulerAngles = originalCameraRot + Random.insideUnitSphere * shakeAmount;
			
				shake -= Time.deltaTime * decreaseFactor;
			} 
			else
			{
				shake = 0f;
			
				cameraTransform.localEulerAngles = originalCameraRot;
			}
		}
	}
}
