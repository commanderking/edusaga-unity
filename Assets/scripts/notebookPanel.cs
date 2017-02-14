using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class notebookPanel : MonoBehaviour {

	//sampleVocabCard refers to prefab where the mandarin, pinyin, and english will go
	public GameObject sampleVocabCard;

	//sampleCategory refers to prefab where the category will go
	public GameObject sampleCategory;

	// Grab the sceneVocabList to be able to create the proper list of vocab for the scene
	public Scene1VocabList scene1VocabList;

	// Grab the AllVocabData scrip to be able to create the proper list for vocab of the scene
	public AllVocabData allVocabData;

	//Panel to attach the card to
	public Transform vocabPanel;

	//Panel to attach the categories to
	public Transform sidebar;

	//List of cateories
	private List<string> categoryList = new List<string>();

	// List of Audio for all sounds in scene
	public GameObject audioObjectWords;

	// List of Images for all pictures in scene
	// public GameObject imageObjects;

	/*
	 * NOTE: Prior to calling PopulateReviewList, be sure to load the appropriate vocab from the scene using Scene1VocabList
	*/

	void Start () {
		// If All Vocab Data not found, look for it on Vocab Controller
		if (!allVocabData){
			allVocabData = GameObject.Find ("Vocab Controller").gameObject.GetComponent<AllVocabData>();

		}

		PopulateVocabCards ();
	}

	public void PopulateVocabCards () {
		foreach (sceneVocabWord vocab in allVocabData.gameVocabList) {
			GameObject newCard = Instantiate (sampleVocabCard) as GameObject;

			//Scale correctly with respect to Parent panel
			newCard.transform.SetParent (vocabPanel.transform, false);

			// Easy reference to the button variables (pinyin, english, audio) that was instantiated
			notebookVocabCard card = newCard.GetComponent <notebookVocabCard>();
			card.category = vocab.category;
			card.mandarin.text = vocab.hanzi;
			card.pinyin.text = vocab.pinyin;
			card.english.text = vocab.english;

			//Check if category is already in list
			if (!categoryList.Contains (vocab.category)) {
				//Add new category to categoryList
				categoryList.Add (vocab.category);

				//Add new category to sidebar
				GameObject newCategory = Instantiate (sampleCategory) as GameObject;
				newCategory.transform.SetParent (sidebar.transform, false);
				notebookCategory category = newCategory.GetComponent <notebookCategory>();
				category.categoryText.text = vocab.category;

				//Without this, vocab.category has scoping issues
				string categoryParameter = vocab.category;

				//Add onClick Listener
				newCategory.GetComponent<Button>().onClick.AddListener (() => showAllInCategory(categoryParameter));
			}

			// Grab the audio list of audio clips from Audio Game Object for Words in scene
			List<AudioClip> audioList = audioObjectWords.GetComponent<AudioList>().vocabAudioList;

			// Dymaically add appropriate audio clip to each button based on Soundkey
			// Iterate through all Audio Clips in the list attached to Audio Game Object 
			// to see if the soundkey matches any audio files in the Audio Game Object
			for (int i = 0; i < audioList.Count; i++ ) {
				// If there's a match, attach audio to button and add event to play on click
				if (vocab.soundkey == audioList [i].name) {
					card.wordAudio.clip = audioList [i];
					card.GetComponent<Button>().onClick.AddListener (() => card.wordAudio.Play ());
				} 
			}
/*
			// Grab the image list of Texture2Ds from Image Game Object for Words in scene
			List<Texture2D> imageList = imageObjects.GetComponent<ImageList>().vocabImageList;

			// Dymaically add appropriate image to each button based on english (lowercase with no spaces)
			for (int j = 0; j < imageList.Count; j++ ) {
				// If there's a match, attach audio to button and add event to play on click
				if (vocab.english.ToLower().Replace(" ", "") == imageList [j].name) {
					Debug.Log (vocab.english.ToLower ().Replace (" ", ""));
					card.setImage(imageList [j]);
				} 
			}
*/
		}
	}

	public void UpdateVocabCards(){
		if (allVocabData.sceneVocabList.Count > vocabPanel.childCount) {
			for (int i = vocabPanel.childCount; i < allVocabData.sceneVocabList.Count; i++) {
				sceneVocabWord vocab = allVocabData.sceneVocabList [i];

				GameObject newCard = Instantiate (sampleVocabCard) as GameObject;

				//Scale correctly with respect to Parent panel
				newCard.transform.SetParent (vocabPanel.transform, false);

				// Easy reference to the button variables (pinyin, english, audio) that was instantiated
				notebookVocabCard card = newCard.GetComponent <notebookVocabCard> ();
				card.category = vocab.category;
				card.mandarin.text = vocab.hanzi;
				card.pinyin.text = vocab.pinyin;
				card.english.text = vocab.english;

				//Check if category is already in list
				if (!categoryList.Contains (vocab.category)) {
					//Add new category to categoryList
					categoryList.Add (vocab.category);

					//Add new category to sidebar
					GameObject newCategory = Instantiate (sampleCategory) as GameObject;
					newCategory.transform.SetParent (sidebar.transform, false);
					notebookCategory category = newCategory.GetComponent <notebookCategory>();
					category.categoryText.text = vocab.category;

					string categoryParameter = vocab.category; // Necessary for scope, but unclear Why does this work?

					//Add onClick Listener
					newCategory.GetComponent<Button>().onClick.AddListener (() => showAllInCategory(categoryParameter));
				}

				// Grab the audio list of audio clips from Audio Game Object for Words in scene
				List<AudioClip> audioList = audioObjectWords.GetComponent<AudioList>().vocabAudioList;

				// Dymaically add appropriate audio clip to each button based on Soundkey
				// Iterate through all Audio Clips in the list attached to Audio Game Object 
				// to see if the soundkey matches any audio files in the Audio Game Object
				for (int j = 0; j < audioList.Count; j++ ) {
					// If there's a match, attach audio to button and add event to play on click
					if (vocab.soundkey == audioList [j].name) {
						card.wordAudio.clip = audioList [j];
						card.GetComponent<Button>().onClick.AddListener (() => card.wordAudio.Play ());
					} 
				}
/*
				// Grab the image list of Texture2Ds from Image Game Object for Words in scene
				List<Texture2D> imageList = imageObjects.GetComponent<ImageList>().vocabImageList;

				// Dymaically add appropriate image to each button based on english (lowercase with no spaces)
				for (int j = 0; j < imageList.Count; j++ ) {
					// If there's a match, attach audio to button and add event to play on click
					if (vocab.english.ToLower().Replace(" ", "") == imageList [j].name) {
						card.setImage(imageList [j]);
					} 
				}
*/
			}
		}
	}

	public void showAllInCategory(string category){
		foreach (Transform card in vocabPanel.transform) {
			//Make sure to add onClick event to All button
			if(category == "All"){
				card.gameObject.SetActive (true);
			}else if (card.gameObject.GetComponent<notebookVocabCard> ().category != category) {
				card.gameObject.SetActive (false);
			} else {
				card.gameObject.SetActive (true);
			}
		}
	}
}
