using UnityEngine;
using System.Collections;

public class AppState : MonoBehaviour
{
	static AppState instance = null;

	private bool isVRModeEnabled = true;

	// Use this for initialization
	void Start ()
	{
		if (instance != null)
			Destroy (gameObject);
		else
			instance = this;

		DontDestroyOnLoad(this.gameObject);
	}

	public static void setVRModeEnabled (bool aEnabled)
	{
		instance.isVRModeEnabled = aEnabled;
	}

	public static bool getVRModeEnabled()
	{
		return instance.isVRModeEnabled;
	}
}
