using UnityEngine;
using System.Collections;

public class CountdownTimer : MonoBehaviour {
	
	float timeRemaining = 14;
	TextMesh text;

	void Start () {
		text = gameObject.GetComponent("TextMesh") as TextMesh;
	}

	void Update () {
		timeRemaining -= Time.deltaTime;

		if (timeRemaining > 1) {
			text.text = "" + (int)timeRemaining;
		} 
		else {
			text.text = "Blast Off!";
		}
	}
 
}
