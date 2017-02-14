using UnityEngine;
using System.Collections;

public class LoadSaveStats : MonoBehaviour {

	// Use this for initialization
	void Start () {
		// Load Saved Stats and store as temporary stats; During each level, only temporary stats are changed, 
		// Permant stats (level and XP) are only changed at the end of the level when called
		PlayerPrefs.SetFloat ("XPTemp", PlayerPrefs.GetFloat ("XP"));
		PlayerPrefs.SetInt ("levelTemp", PlayerPrefs.GetInt ("level"));
		Debug.Log ("XPTemp loaded: " + PlayerPrefs.GetFloat("XPTemp"));
		Debug.Log ("Level loaded: " + PlayerPrefs.GetInt("level"));
		Debug.Log ("Current sceneXP :" + PlayerPrefs.GetFloat ("sceneXP"));
	}

	// Call only the very first time the player starts the game
	public void beginGameStats() {
		PlayerPrefs.SetInt ("level", 1);
		Debug.Log ("Beginner Stats Set");
	}

	// If new scene, reset the sceneXP
	public void beginNewScene() {
		PlayerPrefs.SetFloat ("sceneXP", 0);
	}

	public void saveStats() {
		PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("levelTemp"));
		PlayerPrefs.SetFloat ("XP", PlayerPrefs.GetFloat ("XPTemp"));
		// Print the saved stats
		Debug.Log ("Level saved: " + PlayerPrefs.GetInt("level"));
		Debug.Log ("XP Saved: " + PlayerPrefs.GetFloat("XP"));
	}

	public void deleteStats() {
		PlayerPrefs.DeleteKey ("level");
		PlayerPrefs.DeleteKey ("XP");
		Debug.Log ("Stats deleted");
	}
}
