using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// Script should be attached to the Button that players click to open the IconMenu
// This is the assumed structure of the game objects
/* Menu Button
 * 		Menu Icon Panel
 * 			Notebook Button
 * 			Profile Button
 */

public class LevelMenu : MonoBehaviour {

	public GameObject notebookPanel;

	// Use this for initialization
	void Start () {
		// assign notebookPanel
		try {
			notebookPanel = GameObject.Find("Notebook Canvas").gameObject.transform.GetChild(0).gameObject;
		} catch {
			
		}
		// Allow Menu Button to open the Icon Menu
		gameObject.GetComponent<Button> ().onClick.AddListener (() => openIconMenuPanel());

	}

	public void openIconMenuPanel() {
		GameObject notebook = gameObject.transform.GetChild (0).GetChild (0).gameObject;
		GameObject profiles = gameObject.transform.GetChild (0).GetChild (1).gameObject;


		// If icon menu already opened and called, then close the icon menu
		if (gameObject.transform.GetChild (0).gameObject.activeSelf) {
			// Set the entire icon menu false
			resetIconMenu(notebook, profiles);
			gameObject.transform.GetChild (0).gameObject.SetActive (false);
		} 
		// Otherwise, menu is currently closed and open it up
		else {
			GameObject iconMenu = gameObject.transform.GetChild (0).gameObject;
			iconMenu.SetActive (true);
			// Fade notebook in and add listener to open vocab list
			LeanTween.alpha (notebook.GetComponent<RectTransform>(), 1.0f, 1.0f);
			notebook.GetComponent<Button> ().onClick.AddListener (() => openNotebook(iconMenu));
			LeanTween.alpha (profiles.GetComponent<RectTransform>(), 1.0f, 1.0f);
		}

	}

	public void openNotebook(GameObject iconMenuReference) {
		Debug.Log ("Notebook should be opened");
		// Open notebook
		notebookPanel.SetActive (true);

		// Close the iconMenu
		iconMenuReference.SetActive(false);
	}

	public void resetIconMenu(GameObject notebookIcon, GameObject profileIcon) {
		LeanTween.alpha (notebookIcon.GetComponent<RectTransform> (), 0.0f, 0.0f);
		LeanTween.alpha (profileIcon.GetComponent<RectTransform> (), 0.0f, 0.0f);
	}
}
