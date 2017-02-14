using UnityEngine;
using UnityEngine.UI; 
using System.Collections;
using UnityEngine.SceneManagement;
using Fungus;


/* As of now this script just records score from Scene 1 and saves it for the review page */
/* Attach it to the confidence score UI*/

public class confidenceScore : MonoBehaviour {

	public Text score;
	public Flowchart flowchart; 


	// Use this for initialization
	void Start () {
		// Load sceneXP earned from previous Scene
		score.text = "Experience Earned : " + PlayerPrefs.GetFloat("sceneXP");
	}

	public void updateScore() {
		score.text = "Experience Earned : " + PlayerPrefs.GetFloat("sceneXP");
	}

}
