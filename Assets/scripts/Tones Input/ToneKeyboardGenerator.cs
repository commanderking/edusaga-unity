using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Serialization;
using System.Collections.Generic;

/* Attach this script to Tones Input Manager or Input Panel
 * 
 * */

public class ToneKeyboardGenerator : MonoBehaviour {
	// Panel that the general vowel tone buttons attach to
	public GameObject toneButtonPanel;
	// Prefab for generic vowels
	public Button vowelButton;
	// Prefab for generic toneButtons
	public Button toneButton;


	// Class that contains the vowel and all associated tones of that vowel
	public class toneBreakdown {
		public string vowel; //a 
		public List<string> vowelToneList; //{ "ā", "á", "ǎ", "à" }
		public toneBreakdown(string newVowel, List<string> newVowelToneList) {
			vowel = newVowel;
			vowelToneList = newVowelToneList;
		}
	}

	private List<toneBreakdown> vowelsList = new List<toneBreakdown> ();

	// Use this for initialization
	void Start () {

		// Create list of tones for each vowel
		var aTones = new List<string> { "ā", "á", "ǎ", "à" };
		var eTones = new List<string> {"ē", "é", "ě", "è"};
		var iTones = new List<string> {"ī", "í", "ǐ", "ì" };
		var oTones = new List<string> {"ō", "ó", "ǒ", "ò" };
		var uTones = new List<string> {"ū", "ú", "ǔ", "ù"};
		//var vTones = new List<string> {"ǖ", "ǘ", "ǚ", "ǜ" };

		// Create new classes
		vowelsList.Add (new toneBreakdown ("a", aTones));
		vowelsList.Add (new toneBreakdown ("e", eTones));
		vowelsList.Add (new toneBreakdown ("i", iTones));	
		vowelsList.Add (new toneBreakdown ("o", oTones));
		vowelsList.Add (new toneBreakdown ("u", uTones));
		//vowelsList.Add (new toneBreakdown ("v", eTones));

		createVowelButtons ();
	}

	/* 
	 * 
	 * 1st instantiates a button for each vowel onto a toneButtonPanel
	 * 2nd instantiates a dropdown menu for each tone under each vowel based on toneBreakdown Class
	 * 
	 * */
	public void createVowelButtons() {
		foreach (toneBreakdown vowelClass in vowelsList) {
			// Create button with generic vowel and attach it to the tonebuttonPanel
			Button currentButton = Instantiate (vowelButton);
			currentButton.transform.SetParent (toneButtonPanel.transform, false);
			// Set text to generic letter
			currentButton.GetComponentInChildren<Text> ().text = vowelClass.vowel;
			// Grab the toneDropdown 
			GameObject toneDropdown = currentButton.transform.Find("toneDropdown").gameObject;
			// Create a button for each of the tones of the vowel
			foreach (string tone in vowelClass.vowelToneList) {
				Button currentTone = Instantiate (toneButton);
				currentTone.transform.SetParent (toneDropdown.transform, false);
				currentTone.GetComponentInChildren<Text> ().text = tone;
			}
		}
	}
}
