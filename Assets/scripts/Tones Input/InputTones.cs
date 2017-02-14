using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

// This script should be attached to each button with a vowel/tone on it

public class InputTones : MonoBehaviour {

	private InputField inputField;

	// Grab the correct TextInputField from this scene. This is field where one will add tones
	void Start() {
		inputField = GameObject.Find ("TextInputField").GetComponent<InputField>();
		if (!inputField) {
			Debug.Log ("No Text Input Field found. You might need to change the name of the Input Field to TextInputField");
		}
	}

	/*
	 * 
	 * Add Pinyin of the Button pressed to the Input Field
	*
	*/ 
	public void AddPinyinToInputField() {
		string buttonToneText = this.gameObject.GetComponentInChildren<Text>().text;
		Debug.Log (buttonToneText);
		// Add the tone on the button to the text
		inputField.text += buttonToneText;

		// Refocus on the InputField
		// inputField.Select();
		inputField.ActivateInputField();

		// In order to move cursor to the end, there must be one fram between activating inputfield and MoveTextEnd
		StartCoroutine (MoveToEnd());
	}

	// Moves cursor to end of text box and closes the toneDropdown
	IEnumerator MoveToEnd() {
		yield return 0;
		inputField.MoveTextEnd (true);
		// Hide the panel afterwards
		this.gameObject.transform.parent.gameObject.SetActive(false);
	}

}
