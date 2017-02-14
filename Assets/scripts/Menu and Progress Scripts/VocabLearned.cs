using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/*
 * 
 * This script is responsible for showing that a piece of vocabulary was learned and added to the 
 * notebook and animating an object from somewhere on the screen (TBD) to the menu.
 * 
 * */

public class VocabLearned : MonoBehaviour {
	public GameObject vocabCard;
	public GameObject StageCanvas;
	public GameObject menuDialogMiddleButton;
	public GameObject MenuButtonText;

	// Vector 3s for positions
	private Vector3 offScreen = new Vector3 (-1000, 1000, 0);
	private Vector3 centerScreen = new Vector3 (Screen.width / 2, Screen.height / 2, 0);
	private Vector3 offCenterTopRight = new Vector3 (Screen.width / 2 + 200, Screen.height / 2 + 200, 0);
	private Vector3 doubleScale = new Vector3 (2.0f, 2.0f, 2.0f);
	private Vector3 halfScale = new Vector3 (0.5f, 0.5f, 0.5f);
	private float initialTweenTime = 1.0f;

	void Start() {
		if (MenuButtonText == null) {
			MenuButtonText = GameObject.Find ("Level Text").gameObject;
		} else {
			Debug.Log ("Can't find Level Text to move vocab card towards");
		}

	}

	// Animate vocab if only need card to appear without any characters
	public void newVocabLearnedAnimate() {

		// Instantiate vocab card
		GameObject newVocabCard = Instantiate (vocabCard, centerScreen, Quaternion.identity) as GameObject;
		newVocabCard.transform.SetParent (StageCanvas.transform, false);
		newVocabCard.transform.position = centerScreen; 
		LeanTween.scale (newVocabCard, doubleScale, 1.0f);
		LeanTween.move(newVocabCard, MenuButtonText.transform.position, 1.0f).setDelay(2.0f);
		LeanTween.alpha (newVocabCard.GetComponent<RectTransform> (), 0.0f, 1.0f).setDelay(2.2f);
		LeanTween.scale (newVocabCard, Vector3.one, 2.0f).setDelay(3.2f);
		LeanTween.move (newVocabCard, offScreen, 1.0f).setDelay(3.5f);
	}

	// Overload method to populate card with Characters
	public void newVocabLearnedAnimate(string character) {

		// Instantiate vocab card
		GameObject newVocabCard = Instantiate (vocabCard, centerScreen, Quaternion.identity) as GameObject;
		newVocabCard.transform.SetParent (StageCanvas.transform, false);
		newVocabCard.GetComponent<RectTransform> ().localScale = new Vector3 (0.05f, 0.05f, 0.05f);
		newVocabCard.transform.position = centerScreen; 

		// Set values on card using the notebookVocab Card Script
		newVocabCard.GetComponentInChildren<Text>().text = character;

		// All the animation effects when word is learned
		LeanTween.scale (newVocabCard, doubleScale, initialTweenTime);
		LeanTween.move (newVocabCard, offCenterTopRight, initialTweenTime / 2);
		LeanTween.move (newVocabCard, centerScreen, initialTweenTime / 2).setDelay (initialTweenTime / 2);
		LeanTween.move(newVocabCard, MenuButtonText.transform.position, 1.0f).setDelay(initialTweenTime + 0.5f).setEase(LeanTweenType.easeOutCirc);
		LeanTween.alpha (newVocabCard.GetComponent<RectTransform> (), 0.0f, 1.0f).setDelay(initialTweenTime + 0.7f);
		LeanTween.textAlpha (newVocabCard.GetComponentInChildren<Text>().rectTransform, 0.0f, 1.2f).setDelay(initialTweenTime + 0.7f);
		LeanTween.scale (newVocabCard, halfScale, 1.0f).setDelay(initialTweenTime + 0.7f);
		LeanTween.move (newVocabCard, offScreen, 1.0f).setDelay(initialTweenTime + 2.0f);		
	}

}
