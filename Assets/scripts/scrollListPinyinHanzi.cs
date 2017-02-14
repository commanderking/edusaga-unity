using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Item {
	public string hanzi;
	public string pinyin;
	public Sprite icon;
	public AudioSource audioFile;

}

public class scrollListPinyinHanzi : MonoBehaviour {

	//sampleButton is the button where characters, pinyin, and image will populate
	public GameObject sampleButton;
	public List<Item> itemList;
	public Transform contentPanel;

	//The overall panel that should be hidden, then pop up
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
		
	void PopulateList () {

		//First make the modalPanelObject active
		modalPanelObject.SetActive (true);
		//Clear all previous children in contentPanel in case need to reload this panel, prevents doubling of entries.
		foreach (Transform child in contentPanel.transform) {
			GameObject.Destroy(child.gameObject);
		}

		//For each item, populate it based on the parameters in the GameObject
		foreach (var item in itemList) {
			GameObject newButton = Instantiate (sampleButton) as GameObject;
			//This makes sure it scales well with respect to the parent. Remove to see the craziness! Scale!
			newButton.transform.SetParent (contentPanel.transform, false); 

			//Call the button object called in the script scrollListPinyinHanziButton.cs
			scrollListPinyinHanziButton button = newButton.GetComponent <scrollListPinyinHanziButton> ();
			button.hanzi.text = item.hanzi;
			button.pinyin.text = item.pinyin;
			button.icon.sprite = item.icon;

			newButton.transform.SetParent (contentPanel);

			//Grab the AudioSource from the list for vocab word and play it
			button.audio = item.audioFile;
			button.button.onClick.AddListener (() => button.audio.Play ()); 
		}

	}
}
