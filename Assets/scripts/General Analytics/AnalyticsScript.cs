using UnityEngine;
using System.Collections;
using UnityEngine.Analytics;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class AnalyticsScript : MonoBehaviour {

	public void Start() {
		timeTotal ();
		sceneFunnel ();
	}

	/* Call this at the end of the scene to see how much time was spent in the scene*/

	public void timeInScene() { 
		Analytics.CustomEvent ("Time Played", new Dictionary<string, object> 
		{
			{SceneManager.GetActiveScene().name, Time.timeSinceLevelLoad}
		});
		Debug.Log ("Time played in :" + SceneManager.GetActiveScene ().name + " is " + Time.timeSinceLevelLoad);
	}

	public void timeTotal() {
		Analytics.CustomEvent ("Time Played", new Dictionary<string, object>
		{
			{ "Total Time Played", Time.time }
		});
		// Debug.Log("time total: " + Time.time);
	}

	public void sceneFunnel() {
		Analytics.CustomEvent("Level Start", new Dictionary<string, object>
		{
			{"levelname", SceneManager.GetActiveScene().name},
		});
		// Debug.Log ("Analytics for " + SceneManager.GetActiveScene().name + " Started");
	}

}
