using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class TestModalPanel : MonoBehaviour {
	//Pulling ModalPanel script
	public Sprite exampleImage;
	private ModalPanel modalPanel;

	void Awake () {
		modalPanel = ModalPanel.Instance ();


	}

	//Send to the Modal Panel to set up the buttons and functions to call
	public void openModalPanel() {
		modalPanel.populateExamplePanel ("Examples of Ask for Location", "厕所在哪里?", "Cèsuǒ zài nǎlǐ", 
			exampleImage);
	}

	public void addExample() {
		modalPanel.addExampletoPanel ("You're awesome!");
	}
		
}
