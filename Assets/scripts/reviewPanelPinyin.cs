using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class scene1Review {
	public string pinyin;
	public string english;
	public AudioSource scene1ReviewSound;
}

public class reviewPanelPinyin : MonoBehaviour {

	//sampleButton refers to prefab where the pinyin, english, and sound icon will go
	public GameObject sampleButton;

	// Grab the sceneVocabList to be able to create the proper list of vocab for the scene
	public AllVocabData allVocabData;

	// Actual panel that vocab words will be placed on
	public Transform contentPanel;

	//This is the bigger panel that is hidden, and then activate when called
	public GameObject reviewPanelObject;

	// List of Audio for all sounds in scene
	public GameObject audioObjectWords;

	private static ModalPanel modalPanel;

	public static ModalPanel Instance () {
		if (!modalPanel) {
			modalPanel = FindObjectOfType (typeof(ModalPanel)) as ModalPanel;
			if (!modalPanel)
				Debug.LogError ("There needs to be one active ModalPanel script on a GameObject in your scene.");
		}
		return modalPanel;
	}

	/*
	 * NOTE: Prior to calling PopulateReviewList, be sure to load the appropriate vocab from the scene using Scene1VocabList
	*/

	void Start() {
		// Find AllVocabData script if null
		if (!allVocabData) {
			allVocabData = GameObject.Find ("Vocab Controller").gameObject.GetComponent<AllVocabData> ();
		} else {
			Debug.LogError ("There is no Vocab Controller which contains AllVocabData Script");
		}
	}

	void PopulateReviewList () {
		//Make review panel active
		reviewPanelObject.SetActive (true);

		foreach (sceneVocabWord vocab in allVocabData.sceneVocabList) {
			GameObject newButton = Instantiate (sampleButton) as GameObject;

			//Scale correctly with respect to Parent panel
			newButton.transform.SetParent (contentPanel.transform, false);

			// Easy reference to the button variables (pinyin, english, audio) that was instantiated
			reviewPanelPinyinButton button = newButton.GetComponent <reviewPanelPinyinButton>();
			button.pinyin.text = vocab.pinyin;
			button.english.text = vocab.english;

			// Grab the audio list of audio clips from Audio Game Object for Words in scene
			List<AudioClip> audioList = audioObjectWords.GetComponent<AudioList>().vocabAudioList;

			// Dymaically add appropriate audio clip to each button based on Soundkey
			// Iterate through all Audio Clips in the list attached to Audio Game Object 
			// to see if the soundkey matches any audio files in the Audio Game Object
			for (int j = 0; j < audioList.Count; j++ ) {
				// If there's a match, attach audio to button and add event to play on click
				if (vocab.soundkey == audioList [j].name) {
					button.wordAudio.clip = audioList [j];
					button.button.onClick.AddListener (() => button.wordAudio.Play ());
				} 
			}
		}
	}
}
