using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AllVocabData : MonoBehaviour {

	// Store the instance of this to not destroy on load
	public static AllVocabData Instance;

	// Keeps track of all the vocbulary the player has ever learned
	public List<sceneVocabWord> gameVocabList = new List<sceneVocabWord>();

	// Keeps track of all the vocabulary the player has learned in the scene
	public List<sceneVocabWord> sceneVocabList = new List <sceneVocabWord>();

	// Make this object persist through scene loads
	void Awake () {
		if (Instance == null) {
			DontDestroyOnLoad (gameObject);
			Instance = this;
		} else if (Instance != this) {
			Destroy (gameObject);
		}
	}
}
