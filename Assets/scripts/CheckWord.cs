using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

/*
 * Script attached to "Drop Zone" game Object
 * Functionality to determine 
 */

public class CheckWord : MonoBehaviour {
	//public variables
	public AllVocabData allVocabData;
	public Scene1VocabList scene1Vocablist;
	public List<sceneVocabWord> translationsList = new List<sceneVocabWord>();//List of english & pinyin translations to be tested
		// TODO: dynamically populate the translationsList

	public List<GameObject> slots = new List<GameObject>();//List of slots
	public GameObject displayText;//Text gameObject where english goes
	public Button previous;//previous button
	public Button next;//next button
	public GameObject pinyinPartPrefab;
	public AudioSource blopSource; // Drag blop.wav into the audioclip of this object

	//private variables
	private List<int> randomOrder = new List<int> ();//Hold new order of words (randomized during init)
	private int currIndex;//Index in randomOrder of the word currently being displayed
	private Text english;//Text that goes in displayText
	private int wordLength;//Length of pinyin word
	private GameObject pagination;
	private Text scoreText;
	private int score = 0;
	private Color correctColor = new Color (0.17f, 0.59f, 0.07f); // Color of Drop Zone if answer correct
	private Color incorrectColor = new Color (1.00f, 0.1f, 0.1f); // Color of Drop Zone if answer incorrect

	/*
	 * Initialize
	 */
	void Start(){
		// Find allVocabData if not present
		if (allVocabData == null) {
			allVocabData = GameObject.Find ("Vocab Controller").GetComponent<AllVocabData> ();
		}

		int rand;

		//Set background to gray
		gameObject.GetComponent<Image>().color =  UnityEngine.Color.gray;

		//Add event listeners to buttons & arrow keys
		previous.onClick.AddListener(delegate {ScrollWords("previous");});
		next.onClick.AddListener(delegate {ScrollWords("next");});

		// Set translationsList to current scene's Vocab List
		translationsList = scene1Vocablist.getSceneVocab();
		Debug.Log (translationsList);

		//Randomly scramble word list so that they are not in the same order every time
		randomOrder.Add(Random.Range(0, translationsList.Count));
		for (int i = 1; i < translationsList.Count; i++) {
			rand = Random.Range(0, translationsList.Count);
			while (randomOrder.Contains (rand)) {
				rand = Random.Range(0, translationsList.Count);
			}
			randomOrder.Add(rand);
		}

		//Set up filledSlots for each translation
		for (int i = 0; i < translationsList.Count; i++) {
			for (int j = 0; j < translationsList [i].pinyin.Split (' ').Length; j++) {
				translationsList [i].filledSlots.Add ("");
			}
		}

		//Set up first word
		currIndex = 0;
		english = displayText.GetComponent<UnityEngine.UI.Text> ();
		english.text = translationsList [randomOrder [0]].english;
		//Set slots
		wordLength = translationsList[randomOrder[currIndex]].pinyin.Split(' ').Length;
		for (int i = 0; i < slots.Count; i++) {
			if (i < wordLength) {
				slots [i].SetActive (true);
			} else{
				slots [i].SetActive (false);
			}
		}

		//Set pagination counter
		pagination = GameObject.Find ("Pagination");
		pagination.transform.GetComponent<Text> ().text = (currIndex + 1) + " of " + translationsList.Count;

		//Set scoreText
		scoreText = GameObject.Find("Score").GetComponent<Text>();
		SetScore (0);
	}

	/*
	 * Update: called once per frame. Keeps track of keyboard input
	 */
	void Update () {
		if (Input.GetKeyDown("left")) {
			ScrollWords ("previous");
		} else if (Input.GetKeyDown("right")) {
			ScrollWords ("next");
		}
	}

	/*
	 * ScrollWords: handler for user clicking next & previous buttons. Updates UI to show next or previous word.
	 */
	public void ScrollWords(string button){
		List<string> filledSlots;

		//Get filledSlots from translationsList so that it can be updated
		filledSlots = translationsList [randomOrder [currIndex]].filledSlots;

		//Save current state of drop zone to filledSlots
		for (int i = 0; i < slots.Count; i++) {
			if (slots [i].transform.childCount == 0 && i < filledSlots.Count) {
				filledSlots [i] = "";
			} else if (slots [i].transform.childCount == 1 && i < filledSlots.Count) {
				filledSlots [i] = slots[i].transform.GetChild(0).GetChild(0).GetComponent<Text>().text;
			}
		}

		//Update index based on button selected (increase for "next", decrease for "previous"
		if (button == "next") {
			currIndex++;

			//Loop back to beginning of list if currIndex goes above the length of the list
			if (currIndex >= translationsList.Count) {
				currIndex = 0;
			}
		} else { //previous
			currIndex--;

			//Loop back to end of list if currIndex goes below 0
			if (currIndex < 0) {
				currIndex = translationsList.Count - 1;
			}
		}

		//Update pagination
		pagination.transform.GetComponent<Text> ().text = (currIndex + 1) + " of " + translationsList.Count;

		//Get list of filled slots for new currIndex
		filledSlots = translationsList [randomOrder [currIndex]].filledSlots;

		//Clear slots
		for(int i = 0; i < slots.Count; i++){
			if (slots[i].transform.childCount == 1) {
				DestroyImmediate (slots [i].transform.GetChild (0).gameObject);
			}
		}

		//Set english of new vocab word/phrase
		english = displayText.GetComponent<UnityEngine.UI.Text> ();
		english.text = translationsList [randomOrder [currIndex]].english;

		//Set slots
		wordLength = translationsList[randomOrder[currIndex]].pinyin.Split(' ').Length;

		// Go through slots and show the number of slots equal to the number of pinyin sounds in word
		for (int i = 0; i < slots.Count; i++) {
			if (i < wordLength) {
				slots [i].SetActive (true);

				//Check for previously filled slots and instantiate
				if (filledSlots.Count > i && filledSlots[i] != "") {
					//Instantiate
					GameObject pinyinPart = Instantiate (pinyinPartPrefab) as GameObject;
					pinyinPart.transform.SetParent (slots [i].transform, false);
					pinyinPart.transform.GetChild (0).GetComponent<Text> ().text = filledSlots [i];

					// If previous answer is already correct, remove drag handler and make non-interactable (lock in place)
					if (translationsList [randomOrder [currIndex]].correct == true) {
						Destroy (pinyinPart.GetComponent<DragHandler> ());
						pinyinPart.GetComponent<Button> ().interactable = false;
					}
				}
			} 
			// Set unneeded slots to false
			else {
				slots [i].SetActive (false);
			}
		}

		//Set background colour
		if (translationsList [randomOrder [currIndex]].correct == true) {
			gameObject.GetComponent<Image> ().color = correctColor;
		} else if (SlotsFilled() && translationsList [randomOrder [currIndex]].correct == false) {
			gameObject.GetComponent<Image> ().color = incorrectColor;
		} else {
			gameObject.GetComponent<Image> ().color =  UnityEngine.Color.gray;
		}
	}

	/*
	 * SlotsFilled: checks to see if all the slots are filled
	 */
	public bool SlotsFilled(){
		int slotsFilled = 0;

		//Check if slots have child (word)
		for(int i = 0; i < slots.Count; i++){

			// Count the number of slots are filled
			if (slots[i].transform.childCount == 1) {
				slotsFilled++;
			}
		}

		// If the number of slots filled equals the length of the word, all slots are filled
		if (slotsFilled == wordLength) {
			return true;
		}
		return false;
	}

	/*
	 * CheckAnswer: checks to see if pinyin matches translation; changes the correct bool in translations class to true if correct
	 * Is called in DragHandler and checks every time pinyin block is dropped into a slot
	 */
	public void CheckAnswer(){
		string[] characters; 

		//Create array of pinyin split by spaces (individual characters)
		characters = translationsList[randomOrder[currIndex]].pinyin.Split(' ');

		if (SlotsFilled ()) {
			//Check if word in slot equals the correct character for that slot
			for (int i = 0; i < wordLength; i++) {
				if (slots [i].transform.GetChild (0).GetChild (0).GetComponent<Text> ().text != characters [i]){
					// If incorrect, change background to red 
					gameObject.GetComponent<Image> ().color = incorrectColor;
					score -= 5;
					SetScore (score);
					return;
				}
			}
			// If loop through all pinyin and everything matches... 
			translationsList [randomOrder [currIndex]].correct = true;
			blopSource.Play ();
			StartCoroutine (playAudioSequence ());
			gameObject.GetComponent<Image> ().color = correctColor;
			score += 10;
			SetScore (score);

			//Lock buttons in place
			for (int i = 0; i < wordLength; i++) {
				slots [i].transform.GetChild (0).GetComponent<Button> ().interactable = false;
			}
		} else { // If slots not filled just return out of function
			// PLay blop sound 
			blopSource.Play();
			return;
		}
	}

	/*
	 * SetScore: updates score text
	 */
	public void SetScore(int newScore){
		scoreText.text = "Score: " + newScore;
	}

	/*
	 * Coroutine to play audio of pinyin in sequence
	 */
	IEnumerator playAudioSequence() {
		// Loop through slots to play audio
		for (int i = 0; i < wordLength; i++) {
			//Wait for the blop sound to play
			yield return new WaitForSeconds(blopSource.clip.length);
			// Grab length of current audioClip
			float audioLength = slots [i].transform.GetChild (0).GetComponent<PinyinButton> ().audioSource.clip.length;
			// Play the sound
			slots [i].transform.GetChild (0).GetComponent<PinyinButton>().audioSource.Play ();
			// Wait the length of previous audioclip before playing the next one
			yield return new WaitForSeconds (audioLength);
		}
	}

	/*
	 * Clear: clears filled slots on current word
	 */
}
