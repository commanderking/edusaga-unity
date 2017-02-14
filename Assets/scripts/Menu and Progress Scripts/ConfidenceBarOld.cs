using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Fungus;

public class ConfidenceBarOld : MonoBehaviour {
	public Flowchart flowchart;
	public PlayerPreferences playerPreferences;
	public GameObject confidencePanel;
	public int currentConfidence;
	private Image[] heartImagesArray;
	private bool gameOver;

	void Start () {
		setupConfidence ();
		gameOver = false;
	}

	void Update () {
		// If on last heart, then pulse heart
		// TODO: Need to fix behavior. It's really sporadic right now
		if (currentConfidence == 1) {
			Vector3 largeHeartSize = new Vector3 (1.25f, 1.25f, 1.25f);
			Vector3 smallHeartSize = new Vector3 (0.75f, 0.75f, 0.75f);
			LeanTween.scale(heartImagesArray[currentConfidence].gameObject.GetComponent<RectTransform>(), smallHeartSize, 1.0f).setDelay(0.5f);
			LeanTween.scale(heartImagesArray[currentConfidence].gameObject.GetComponent<RectTransform>(), largeHeartSize, 1.0f).setDelay(1.5f);
		}
	}

	public void setupConfidence(){
		// Grab all the heart symbols to manipulate
		heartImagesArray = confidencePanel.GetComponentsInChildren<Image>();

		// Reset the panel to be all clear of hearts, set background to white
		foreach (Image heart in heartImagesArray) {
			//Exclude the parent in the if statement since GetComponentsInChildren also grabs the image in parent
			if (heart.gameObject.GetInstanceID () != confidencePanel.GetInstanceID ()) {
				heart.color = Color.white;
			}
		}
		// Add appropriate number of hearts into the scene based on currentConfidence
		currentConfidence = 3;
		// populateHearts(heartImagesArray);
	}

	public void populateHearts(Image[] heartsArray) {
		// Avoid 0 index because that's the parent gameObject
		// Color up to currentConfidence red
		for(int i = 1; i < currentConfidence + 1; i++) {
			heartsArray [i].color = Color.red;
		}
		// Color anything past currentConfidence white
		for (int i = currentConfidence + 1; i < heartsArray.Length; i++) {
			heartsArray [i].color = Color.white; 
		}
	}

	public void addHearts() {
		// Only make changes if player is at the limit on number of hearts
		if (currentConfidence < 5) {
			currentConfidence += 1;
			populateHearts (heartImagesArray);
			Vector3 newHeartSize = new Vector3(1.5f, 1.5f, 1.5f);
			LeanTween.scale(heartImagesArray[currentConfidence].gameObject.GetComponent<RectTransform>(), newHeartSize, 0.5f);
			LeanTween.scale(heartImagesArray[currentConfidence].gameObject.GetComponent<RectTransform>(), Vector3.one, 1f).setDelay(0.5f);
		}
	}

	public void subHearts() {
		// Only deduct if, confidence is nonzero
		if (currentConfidence > 0) {
			currentConfidence -= 1;
		} 

		populateHearts (heartImagesArray);
		Vector3 newHeartSize = new Vector3(0.5f, 0.5f, 0.5f);
		// + 1 because the heart you just lost should animate, not the current heart
		LeanTween.scale(heartImagesArray[currentConfidence + 1].gameObject.GetComponent<RectTransform>(), newHeartSize, 0.5f);
		LeanTween.scale(heartImagesArray[currentConfidence + 1].gameObject.GetComponent<RectTransform>(), Vector3.one, 1f).setDelay(0.5f);
		if (currentConfidence == 0) {
			gameOver = true;
			Debug.Log ("Game Over!");

			// If nightmare scene, wake the player up
			if(flowchart.FindBlock("nightmare")) {
				flowchart.StopAllBlocks ();
				flowchart.ExecuteBlock ("nightmare");
			}
			// If exploratory scene, look for sceneOver block on GameOver
			else if (flowchart.FindBlock("sceneOver")){
				flowchart.StopAllBlocks();
				flowchart.ExecuteBlock("sceneOver");
			}
		}
	}
}



/* 
	 * 
	 * OLD CONFIDENCE BAR CODE
	 * 
	 * 
	 * 
	public int startingConfidence;
	public int currentConfidence;
	public Slider confidenceSlider;
	public Text confidenceText;
	public GameObject confidencePanel;
	public float flashSpeed = 5f;

	//Affects confidenceBar if answer is correct or incorrect
	public bool wrongAnswer = false; 
	public bool correctAnswer = false;
	public bool confidenceFadeBool = false;  //If true, fade the confidence bar to green or red based on correct or not

	//Access flowchart to save to fungusvariables
	public Flowchart flowchart; 

	void Start () {
		//Setup initial confidence bar;
		currentConfidence = startingConfidence;
		confidenceSlider.value = currentConfidence;
		confidenceText.text = "Confidence: " + currentConfidence; 
	}
	
	void Update () {
		//If answer is wrong/right, add effects to confidencebar 
		if (confidenceFadeBool && correctAnswer == true) {
			StartCoroutine (confidenceFade (true));
		} else if (confidenceFadeBool && correctAnswer == false) {
			StartCoroutine (confidenceFade (false));
		}

	}

	//Based on correct or incorrect answer, animate confidence bar 
	IEnumerator confidenceFade(bool correctAnswer) {
		//If answer incorrect, set color of panel to red
		if (correctAnswer == true) {
			confidencePanel.GetComponent<Image>().color = new Color(0.09f,0.32f,0.14f,1); //turn bar green
		}
		else {
			confidencePanel.GetComponent<Image>().color = new Color(1,0,0,1); //turn bar red
		}
		//Slowly fade to desired color
		for (float f = 0f; f <= 1; f += 0.03f) {
			//Define the panel that's being changed
			Color c = confidencePanel.GetComponent<Image>().color;
			c.a = f; //Set color to incremental variable. This will allow slow fade to desired color. 
			confidencePanel.GetComponent<Image>().color = c; //Actually change the color to be the new alpha value.
			yield return null;
		}
		//Revert back to original confidence bar color
		confidencePanel.GetComponent<Image>().color = new Color(0,0,0,1);
		confidenceFadeBool = false; //Turn off coroutine
	}

	//Handle wrong answers
	public void subConfidence (int points) {

		handleAnswers (false, points);
	}
	//Handle Correct or Incorrect Answers 
	public void handleAnswers (bool correctOrNot, int points) {
		//For old scenes. If no points are specified, default to 10 points. 
		if (points == 0) {
			points = 10;
		}
		//Handle specific actions for correct and wrong answers; 
		if (correctOrNot == true) {
			correctAnswer = true; 
			currentConfidence += points;
		} else {
			correctAnswer = false;
			//wrongAnswer = true;
			currentConfidence -= points;
			// Confidence should never drop below 0;
			if (currentConfidence <= 0) {
				currentConfidence = 0;
			}
		}
		confidenceFadeBool = true; 

		flowchart.SetIntegerVariable ("sceneConfidence", currentConfidence); 	//Save the confidence as a fungus variable...
		confidenceSlider.value = currentConfidence; 							//Set Confidence slider and text to reflect current health
		confidenceText.text = "Confidence: " + currentConfidence; 
	}
*/