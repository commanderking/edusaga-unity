using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System; // Used to sort arrays


public class PopulatePinyinButtons : MonoBehaviour {

	public GameObject practicePanel; // Practice Panel to flash in 
	public GameObject parentPanel;	// Parent panel to attach Pinyin buttons to
	public GameObject reviewPanel; // Reference review panel to be able to set it active when parent panel closes
	public GameObject slot; // Slot that buttons will occupy

	public AllVocabData allVocabData; // Grab Scene 1 VocabList
	private RemoveTones removeTones; // Grab Script to remove Tones  

	// Audio GameObject contains a list of all the audio
	public GameObject audioObject;

	void Awake () {
		// Reference RemoveTones script, which should be attach to Game Controller along with this script
		removeTones = gameObject.GetComponent<RemoveTones>();

		// Grab AllVocabData from Vocab Controller
		if (!allVocabData) {
			allVocabData = GameObject.Find ("Vocab Controller").gameObject.GetComponent<AllVocabData> ();
		} else {
			Debug.LogError ("There is no Vocab Controller which contains AllVocabData Script");
		}

	}

	public void displayPinyinButtonChoices() {

		// Convert the Vocab List to a long string to be parsed
		List<sceneVocabWord> vocabList = allVocabData.sceneVocabList;
		string tempString = ListToString (vocabList);

		// Convert String to Dictionary
		Dictionary<string,int> tempDictionary = StringToDictionary (tempString);

		// Populate the screen with pinyin buttons based on dictionary
		populateButtons (tempDictionary);
	}

	/**
	 * Change the Vocab list to a long string which contains all pinyin in scene
	*/
	public string ListToString(List<sceneVocabWord> vocabList){
		string vocabString = "";
		for (int i = 0; i < vocabList.Count; i++) {
			vocabString = vocabString + vocabList [i].pinyin + " ";
		}
		return vocabString;
	}

	/**
	 * Parse the long vocabString into a List that shows the pinyin and the number of that vocab word is needed
	*/ 
	public Dictionary<string, int> StringToDictionary(string vocabString) {
		// Create new List to store all the Vocab Word Classes
		Dictionary<string, int> vocabDictionary = new Dictionary<string, int>();

		// Split all the pinyin in the pinyinString and store them in an array
		string[] pinyinArr = vocabString.Split (' ');
		// Sort the array
		Array.Sort(pinyinArr);
		foreach (string element in pinyinArr) {
			Debug.Log (element);
		}

		// usedPinyinList stores already used Pinyin in an array
		List<string> usedPinyinList = new List<string>();

		// Loop through pinyinArr
		foreach (string pinyin in pinyinArr) {
			// Check if pinyin has already been added; If so, increase the count
			if (usedPinyinList.Contains (pinyin)) {
				vocabDictionary [pinyin] += 1;
			} 
			// If not, add it to the dictionary
			else {
				vocabDictionary.Add (pinyin, 1);
				usedPinyinList.Add (pinyin);
			}
		}
		// Remove empty entries. For some reason first entry in array is always empty. Maybe from split?
		vocabDictionary.Remove ("");
		return vocabDictionary;
	}

	/**
	 * Instantiate all entries in dictionary as a slot and a button 	
	*/ 
	public void populateButtons(Dictionary<string, int> vocabDictionary) {

		foreach (KeyValuePair<string, int> entry in vocabDictionary) {
			for (int i = 0; i < entry.Value; i++) {
				// Instantiate Slot that will hold the button
				GameObject slotForButton = Instantiate (slot);
				slotForButton.transform.SetParent (parentPanel.transform, false); 
				// Edit the button that's a child of the slot prefab
				GameObject newButton = slotForButton.transform.GetChild(0).gameObject;
				// Enter correct pinyin text to be on button from Dictionary
				newButton.GetComponentInChildren<Text> ().text = entry.Key;

				// Add pinyinPart tag to allow for easy deletion later 
				newButton.tag = "pinyinPart";

				// Grab the PinyinButton Script on object 
				// Transfer key and pair values over to the button itself 

				PinyinButton buttonVariables = newButton.GetComponent<PinyinButton> ();
				buttonVariables.pinyin = entry.Key;
				buttonVariables.count = entry.Value;

				// Grab the audio list from Audio Game Object in scene
				List<AudioClip> audioList = audioObject.GetComponent<AudioList>().vocabAudioList;
				for (int j = 0; j < audioList.Count; j++ ) {
					// See if there's a match between current entry's pinyin and audiolist name
					// Remove tones from pinyin in order to properly compare to audio files
					string tempEntry = removeTones.removePinyinTones (entry.Key);
					if (tempEntry == audioList [j].name) {

						// Assign the button's clip to be that from the audio list
						buttonVariables.audioSource.clip = audioObject.GetComponent<AudioList> ().vocabAudioList [j];
						newButton.GetComponent<Button>().onClick.AddListener (() => buttonVariables.audioSource.Play ());
					}
				}
			}
		}
	}
	/*
	 * Destroy all children (slots and buttons) to reset practice zone
	 * 
	*/
	public void destroyPracticeButtonZoneSlots() {

		foreach(Transform transform in parentPanel.GetComponentsInChildren<Transform>()) {

			// GetComponentsinChildren also grabs current object so ignore the parent panel 
			if (transform.gameObject == parentPanel) {
			} else {
				Destroy (transform.gameObject);
			}
		}
	}

	/*
	 * Destroy all pinyinPart Buttons including those that have already been dragged to a slot
	 *
	*/ 
	public void destroyDropZoneButtons() {
		GameObject[] pinyinPartArray = GameObject.FindGameObjectsWithTag ("pinyinPart");
		Debug.Log(pinyinPartArray.Length);
		Debug.Log (pinyinPartArray);
		foreach (GameObject pinyinPart in pinyinPartArray) {
			Destroy (pinyinPart);
		}
	}

	/*
	 * What needs to happen to return to review screen in proper sequence
	*/
	public void returnToReviewScreen() {
		destroyDropZoneButtons ();
		destroyPracticeButtonZoneSlots ();

		// TODO: Application.Loadlevel is temp solution. Should instead hide practice panel and load review panel instead
		// PROBLEM: If press "Go back to Review Screen" and then try to practice again, double the number of buttons are
		// loaded. The pinyin buttons all clear upon closing, but for some reaosn, there are more being populated each time
		Application.LoadLevel ("1_Review");
	}
}