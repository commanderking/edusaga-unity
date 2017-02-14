using UnityEngine;
//reference UI elements
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class ModalPanel : MonoBehaviour {

	//For populating Phone Example Panel with first item
	public Text headerText;
	public Text hanziText;
	public Text pinyinText;
	public Image exampleImage;
	public Button item1Button;
	public Button item2Button;
	public Button item3Button;
	private string hanzi;
	private string pinyin;

	//For adding additional items to Phone Example Panel
	public GameObject screenPanel;
	public GameObject itemButton; 
	public GameObject itemImage; 

	public GameObject modalPanelObject;

	private static ModalPanel modalPanel;

	public static ModalPanel Instance () {
		if (!modalPanel) {
			modalPanel = FindObjectOfType (typeof(ModalPanel)) as ModalPanel;
			if (!modalPanel)
				Debug.LogError ("There needs to be one active ModalPanel script on a GameObject in your scene.");
		}

		return modalPanel;

	}

	//Set the modal panel based on what user wants in panel
	public void populateExamplePanel (string headerText, string hanziText, string pinyinText, Sprite exampleImage) {
		modalPanelObject.SetActive (true);
		this.headerText.text = headerText;
		this.hanziText.text = hanziText;
		this.pinyinText.text = pinyinText;
		this.exampleImage.sprite = exampleImage;
		this.exampleImage.gameObject.SetActive (true);
	}

	//Instantiate additional examples into phone
	public void addExampletoPanel(string hanzi) {
		GameObject newExample = Instantiate(itemButton) as GameObject;
		newExample.transform.SetParent (screenPanel.transform, false); 
		GameObject.Find ("Hanzi Text").GetComponentInChildren<Text> ().text = hanzi; 
		Debug.Log (hanzi);
		Debug.Log ("What?");
	}



	void ClosePanel () {
		modalPanelObject.SetActive (false);
	}

}