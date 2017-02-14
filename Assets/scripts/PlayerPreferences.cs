using UnityEngine;
using System.Collections;
using Fungus;

public class PlayerPreferences : MonoBehaviour {
	public Flowchart flowchart;
	public bool AFM = false;
	public bool TCR = false;
	public int level = 1;
	public float XP = 0;
	public int confidence = 3;

	void Start() {
		// Load Saved Stats and store as temporary stats; During each level, only temporary stats are changed, 
		// Permant stats (level and XP) are only changed at the end of the level when called
		PlayerPrefs.SetFloat ("XPTemp", PlayerPrefs.GetFloat ("XP"));
		PlayerPrefs.SetFloat ("levelTemp", PlayerPrefs.GetFloat ("level"));
	}

	public void saveStats() 
	{
		PlayerPrefs.SetInt ("level", level);
		PlayerPrefs.SetFloat ("XP", XP);
		PlayerPrefs.SetInt ("confidence", confidence);
	}

	// Set AFM vocab words to true if user goes to American Fried Meats
	public void scene1PlayerPrefAFMVisited() {
		AFM = true;
		PlayerPrefs.SetString ("AFM", "true");
	}

	// Set TCR words to true if user goes to TCR 
	public void scene1PlayerPrefTCRVisited() {
		TCR = true;
		PlayerPrefs.SetString ("TCR", "true");
	}

	// Check restaurant visited and set variable in Fungus to allow for differentiated dream experience
	public void checkRestaurantVisited() {
		if (PlayerPrefs.GetString ("AFM") == "true") {
			flowchart.SetStringVariable ("restaurantVisited", "AFM");
		} else {
			flowchart.SetStringVariable ("restaurantVisited", "TCR");
		}
	}

	public void clearPlayerPrefs() {
		PlayerPrefs.DeleteAll ();
	}
}