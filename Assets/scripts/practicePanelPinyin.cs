using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class scene1Practice {
	public int blankSpaces;
	public string english;
	public AudioSource scene1ReviewSound;

}

public class practicePanelPinyin : MonoBehaviour {

	//sampleButton refers to button where the pinyin, english, and sound icon will go
	public GameObject sampleButton;
	public List<scene1Practice> scene1PracticeList;
	//Panel to attach the button to? 
	public Transform contentPanel;

	//This is the bigger panel that is hidden, and then activate when called
	public GameObject reviewPanelObject;

	private static ModalPanel modalPanel;

	public static ModalPanel Instance () {
		if (!modalPanel) {
			modalPanel = FindObjectOfType (typeof(ModalPanel)) as ModalPanel;
			if (!modalPanel)
				Debug.LogError ("There needs to be one active ModalPanel script on a GameObject in your scene.");
		}

		return modalPanel;

	}

	void PopulateReviewList () {
		//Make review panel active
		reviewPanelObject.SetActive (true);

		//For each item, populate it based on the parameters in the GAmeObject 
		foreach (var vocab in scene1PracticeList) {
			GameObject newButton = Instantiate (sampleButton) as GameObject;
			string spaces = "";

			//Scale correctly with respect to Parent panel
			newButton.transform.SetParent (contentPanel.transform, false);
			//Call the button object called in the script 
			reviewPanelPinyinButton button = newButton.GetComponent <reviewPanelPinyinButton>();
			for (int x = 0; x < vocab.blankSpaces; x++) {
				spaces += "___  ";
			}
			button.pinyin.text = spaces;
			button.english.text = vocab.english;

			//Grab the AudioSource from the list for vocab word and play it
			button.wordAudio = vocab.scene1ReviewSound;
			button.button.onClick.AddListener (() => button.wordAudio.Play ()); 

			//Play sound when the picture of the audio button is clicked
			button.audioButton.onClick.AddListener (() => button.wordAudio.Play ()); 
		}


	}

}
