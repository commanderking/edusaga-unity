using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using Fungus;

public class PictureChoicePanel : MonoBehaviour {

	//Unity UI templates on the Choice Panel
	public Text headerText;
	public Button button1;
	public Button button2;
	public Button button3;
	public Button button4;
	public Button button5;
	public Text button5Text;
	public GameObject pictureChoicePanelObject;
	public Flowchart flowchart;

	private static PictureChoicePanel pictureChoicePanel;

	// Make sure PictureChoicePanel exists. If not, find it. If not there, show error
	public static PictureChoicePanel Instance () {
		if (!pictureChoicePanel) {
			pictureChoicePanel = FindObjectOfType(typeof (PictureChoicePanel)) as PictureChoicePanel;
			if (!pictureChoicePanel)
				Debug.LogError ("There needs to be one active PictureChoicePanel script on a GameObject in your scene.");
		}

		return pictureChoicePanel;
	}

	// Function to populate the panel with 3 images and 1 text bubble
	public void populatePictureChoicePanel (
		int questionNumber, 
		string header, 
		Sprite firstImage, 
		Sprite secondImage, 
		Sprite thirdImage, 
		Sprite textImage, 
		string textforImage) 
	{
		// Set Fungus Question Number
		flowchart.SetIntegerVariable("questionNumber", questionNumber);

		pictureChoicePanelObject.SetActive (true);

		// Populate UI with correct properties
		this.headerText.text = header;
		this.button1.image.sprite = firstImage;
		this.button2.image.sprite = secondImage;
		this.button3.image.sprite = thirdImage;
		this.button5.image.sprite = textImage;
		this.button5Text.text = textforImage; 

		// Add correct events to buttons 

		// Array of buttons to set active (3 images, 1 Text)
		Button[] buttonArray = new Button[4] {button1, button2, button3, button5};

		// Clear previous Event Listeners
		clearButtonListeners (buttonArray);

		// Add Listeners to assign the button number clicked to Fungus
		this.button1.onClick.AddListener ( ()=> assignButtonNumber(1));
		this.button2.onClick.AddListener ( ()=> assignButtonNumber(2));
		this.button3.onClick.AddListener ( ()=> assignButtonNumber(3));
		this.button5.onClick.AddListener ( ()=> assignButtonNumber(5));

		// Setup the Buttons to Apper and Function correctly
		setButtonsActive (buttonArray);
		this.button4.gameObject.SetActive (false);
	}

	/* BEHIND THE SCENES FUNCTIONS CALLED IN populatePictureChoicePanel
	 * 
	 * 
	 * 
	 * 
	*/

	// Integration with Fungus to allow Fungus to know which block to execute
	public void assignButtonNumber(int number) {
		flowchart.SetIntegerVariable ("buttonNumber", number);
	}


	// Takes array of buttons and the questionNumber (which is passed into populatePictureChoicePanel
	public void setButtonsActive (Button[] arr) {
		// Loop over all buttons to 
		for (int i = 0; i < arr.Length; i++) {
			// Set all buttons active 
			arr [i].gameObject.SetActive (true);

			// TODO: Should cycle through topass button info from functions to Fungus (doesn't work)
			// arr [i].onClick.AddListener ( ()=> passButtonClickedToFungus(i+1));

			// Send Message to go to Picture Choice Controller and handle logic from there
			arr [i].onClick.AddListener ( ()=> sendFungusMessage("Picture Choice Controller"));

			// Close the panel when button clicked
			arr [i].onClick.AddListener (closePanel);
		}
	}
	//Clear all listeners on buttons. Should be done prior to listeners are assigned to make sure no leftover listeners present
	public void clearButtonListeners (Button[] arr) {
		for (int i = 0; i < arr.Length; i++) {
			arr [i].onClick.RemoveAllListeners ();
		}
	}

	public void closePanel() {
		pictureChoicePanelObject.SetActive (false);
	}
					

	public void sendFungusMessage (string message) {
		flowchart.SendFungusMessage (message);
	}
}
