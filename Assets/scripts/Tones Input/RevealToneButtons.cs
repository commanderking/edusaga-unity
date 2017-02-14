using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class RevealToneButtons : MonoBehaviour {
	/*
	 * 
	 * Reveal the "dropdown" panel of tones for letter
	 * Should be attached to a button that will reveal the child panel of tones
	 * Ex. Attach this script to the "a" button. Upon clicking a button, it will find the toneDropdown gameObject
	 * and reveal it
	 * 
	 * */

	public void revealToneDropdown() {
		// Close all toneDropdown panels that might already be open (The toneDropdown under vowel button 
		// should be tagged with toneDropdown
		// Ex. If you opened the a tone dropdown, but realized you need the e on,e it should close the a one. 
		GameObject[] toneDropdownArray = GameObject.FindGameObjectsWithTag("toneDropdown");
		foreach (GameObject toneDropdown in toneDropdownArray) {
			toneDropdown.SetActive (false);
		}

		// Grab the current dropdown of the vowel
		GameObject panel = this.gameObject.transform.Find ("toneDropdown").gameObject;
		Debug.Log (panel);
		// If the child panel is not active, we want to set it active
		if (!panel.activeSelf) {
			panel.SetActive (true);
		} 
		// Else, if it's already active, close it
		else {
			panel.SetActive (false);
		}


	}
}
