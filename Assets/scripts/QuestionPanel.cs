using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using Fungus;

public class QuestionPanel : MonoBehaviour {

	public Text question;
	public InputField playerAnswer;
	public Button submit; 
	public Button helpButton;
	public GameObject questionPanelObject;
	private string inputValue;

	private static QuestionPanel questionPanel;

	public Flowchart flowchart; 

	public static QuestionPanel Instance () {
		if (!questionPanel) {
			questionPanel = FindObjectOfType(typeof (QuestionPanel)) as QuestionPanel;
			if (!questionPanel)
				Debug.LogError ("There needs to be one active Question Panel script on a GameObject in your scene.");
		}

		return questionPanel;
	}

	// Make questionPanel appear and take input as to what question should prompt user in panel and the correct answer
	public void createQuestionPanel (string question, string correctAnswer) {
		questionPanelObject.SetActive (true);
		this.question.text = question;

		// Assign question and answer to Fungus to perform checktextanswer calculation
		flowchart.SetStringVariable ("correctAnswer", correctAnswer.ToLower().Replace(" ", "").Replace("\n", ""));
		//Debug.Log ("correctAnswer set");

	}

	//Overload with option to give additional correct answers with no tones and characters
	public void createQuestionPanel (string question, string correctAnswer, string noToneAnswer, string characterAnswer, bool help = false){
		questionPanelObject.SetActive (true);
		this.question.text = question;

		// Set help button to false if no need for it
		if (help == false) {
			helpButton.gameObject.SetActive (false);
		} else {
			helpButton.gameObject.SetActive (true);
		}


		// Assign question and answer to Fungus variables to perform checktextanswer calculation
		// At same time, replace all spaces, question marks, exclamation marks and lowercase everything
		flowchart.SetStringVariable ("correctAnswer", correctAnswer.ToLower().Replace(" ", "").Replace("\n", ""));
		flowchart.SetStringVariable ("noToneAnswer", noToneAnswer.ToLower ().Replace(" ", "").Replace("\n", ""));
		flowchart.SetStringVariable ("characterAnswer", characterAnswer.ToLower ().Replace(" ", "").Replace("\n", ""));
	
	}

	public void closeQuestionPanel () {
		questionPanelObject.SetActive (false);

		// Clear inputfield of all text
		playerAnswer.text = "";
	}

	// Update is called once per frame
	void Update () {
	
	}
}
