using UnityEngine;
using System.Collections;

public class Scene1DreamPC : MonoBehaviour {

	public Sprite button1IMG;
	public Sprite button2IMG;
	public Sprite button3IMG;
	public Sprite button4IMG;
	public Sprite textIMG;

	public PictureChoicePanel pictureChoicePanel;

	// Use this for initialization
	void Awake () {
		pictureChoicePanel = PictureChoicePanel.Instance ();
	}
		
	public void choice1() {
		pictureChoicePanel.populatePictureChoicePanel (
			1, 
			"What should I give the customer?",
			button1IMG, 
			button2IMG, 
			button3IMG, 
			textIMG, 
			"I'm not giving this guy anything!");
	}

	public void choice2() {
		pictureChoicePanel.populatePictureChoicePanel (
			2, 
			"What should I give the customer?",
			button1IMG, 
			button2IMG, 
			button3IMG, 
			textIMG, 
			"I'm not giving this guy anything!");
	}

	/* 
	 * 
	 * Questions related to Traditional Chinese Restaurant
	 * 
	 * */
	public void choice3() {
		pictureChoicePanel.populatePictureChoicePanel (
			3, 
			"What should I give the customer?",
			button1IMG, 
			button2IMG, 
			button3IMG, 
			textIMG, 
			"I'm not giving this guy anything!");
	}

	// Questions related to Traditional Chinese Restaurant
	public void choice4() {
		pictureChoicePanel.populatePictureChoicePanel (
			4, 
			"What should I give the customer?",
			button1IMG, 
			button2IMG, 
			button3IMG, 
			textIMG, 
			"I'm not giving this guy anything!");
	}

	// Boss scene - AFM - 

	public void choice6() {
		pictureChoicePanel.populatePictureChoicePanel (
		6, 
		"What should I give the customer?",
		button1IMG,
		button2IMG,
		button3IMG,
		textIMG,
		"Nothing");
	}

	public void choice7() {
		pictureChoicePanel.populatePictureChoicePanel (
		7,
		"Is he ordering something else?",
		button1IMG,
		button2IMG,
		button3IMG,
		textIMG,
		"Nothing");
	}

	// Boss scene - TCR - 
	public void choice8() {
		pictureChoicePanel.populatePictureChoicePanel (
			8, 
			"What should I give the customer?",
			button1IMG,
			button2IMG,
			button3IMG,
			textIMG,
			"Nothing");
	}

	public void choice9() {
		pictureChoicePanel.populatePictureChoicePanel (
			9,
			"Is he ordering something else?",
			button1IMG,
			button2IMG,
			button3IMG,
			textIMG,
			"Nothing");
	}


}
