using UnityEngine;
using System.Collections;
using System; // In order to implement try catch

/*
 * 
 * This is the Scene Initializer that should be attached to Game Controller for every scene;
 * Makes integration much easier!
 * 
 * */

public class SceneInitializer : MonoBehaviour {
	public ProgressDataController progressDataController;

	// Use this for initialization
	void Awake () {
		// Try to see if progressDataControler is attached to Game Data object, if not, instantiate a new one
		if (GameObject.Find("Game Data") == null) {
			GameObject gameDataInstance = Instantiate (Resources.Load ("Game Data") as GameObject);
			gameDataInstance.name = "Game Data";
		} 

		// Try to find Vocab Controller, if not Load it from Resources
		if (GameObject.Find("Vocab Controller") == null) {
			GameObject vocabControllerInstance = Instantiate (Resources.Load ("Vocab Controller") as GameObject);
			vocabControllerInstance.name = "Vocab Controller";
		}

		if (GameObject.Find ("Notebook Canvas") == null) {
			GameObject vocabControllerInstance = Instantiate (Resources.Load ("Notebook Canvas") as GameObject);
			vocabControllerInstance.name = "Notebook Canvas";
		}
	}

}
