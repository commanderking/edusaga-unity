using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Fungus;

[CommandInfo("Camera", 
	"Fade to bg Scene",
	"Fade to New Background Image")]

public class changeBackgroundScene : Command {

	public Transform backgroundPanel;
	public Sprite backgroundImage; 

	[Tooltip("Time for fade effect to complete")]
	public float duration = 1f;


	public override void OnEnter () {

		//Grab the image component of background panel and set its sprite to the background image
		//backgroundPanel.GetComponent<Image>().color = Color.black;
		backgroundPanel.GetComponent<Image>().sprite = backgroundImage;
		backgroundPanel.gameObject.SetActive(true);
		//backgroundPanel.GetComponent<Image> ().CrossFadeColor (Color.black, 2f, false, false);

		//Move on to next item in Fungus
		Continue ();
	}
}
