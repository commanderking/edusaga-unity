using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Fungus;

public class ConfidenceBar : MonoBehaviour {
	private Slider sliderConfidence;
	private Image confidenceIcon;

	// Script to talk to Controller which talks to Data
	public ProgressDataController progressDataController;

	// This type of data will need to be stored
	private SceneProgress sceneProgressData;
	// private GameProgress gameProgressData;

	public void Start () {
		// Get objects in scene that need to be rendered / manipulated
		// This assumes that the slider and confidence icon are the child of the parent
		sliderConfidence = gameObject.transform.GetChild(1).GetComponent<Slider>();
		Debug.Log (sliderConfidence);
		confidenceIcon = gameObject.transform.GetChild (0).GetComponent<Image> ();

		// Make sure Game Data gameObject and Progress Data Controller script exist
		if (!progressDataController) {
			progressDataController = GameObject.Find ("Game Data").GetComponent<ProgressDataController> ();
		} else {
			Debug.Log ("No Game Data Object fund in scene");
		}

		// Get Data from Progress Data Controller
		// gameProgressData = progressDataController.getGameProgress ();
		sceneProgressData = progressDataController.getSceneProgress ();

		// Set the slider to the confidence
		sliderConfidence.value = sceneProgressData.sceneConfidenceCurrent;

		//confidence up
		/*LeanTween.scale (confidenceIcon.gameObject, new Vector3 (1.25f, 1.25f, 1.25f), .25f);
		LeanTween.scale (confidenceIcon.gameObject, new Vector3 (.75f, .75f, .75f), .125f).setDelay (.25f);
		LeanTween.scale (confidenceIcon.gameObject, new Vector3 (1f, 1f, 1f), .125f).setDelay (.625f);
		LeanTween.rotate (confidenceIcon.gameObject, new Vector3 (0.0f, 0.0f, -25.0f), .125f).setDelay (.625f);
		LeanTween.rotate (confidenceIcon.gameObject, new Vector3 (0.0f, 0.0f, 20.0f), .125f).setDelay (.75f);
		LeanTween.rotate (confidenceIcon.gameObject, new Vector3 (0.0f, 0.0f, -15.0f), .125f).setDelay (.875f);
		LeanTween.rotate (confidenceIcon.gameObject, Vector3.zero, .125f).setDelay (1f);*/



		/*LeanTween.color (
			confidenceIcon.gameObject, new Color(1.0f, 1.0f, 1.0f, 1.0f), 0.5f).setDelay(.25f);
*/


	}

	public void testConfidence() {
		//confidence down
		LeanTween.scale (confidenceIcon.gameObject, new Vector3 (.25f, .25f, .25f), .25f);
		LeanTween.rotate (confidenceIcon.gameObject, new Vector3 (0.0f, 0.0f, -25.0f), .125f).setDelay (.25f);
		LeanTween.rotate (confidenceIcon.gameObject, new Vector3 (0.0f, 0.0f, 20.0f), .125f).setDelay (.375f);
		LeanTween.rotate (confidenceIcon.gameObject, new Vector3 (0.0f, 0.0f, -15.0f), .125f).setDelay (.5f);
		LeanTween.rotate (confidenceIcon.gameObject, Vector3.zero, .125f).setDelay (.625f);
		LeanTween.scale (confidenceIcon.gameObject, new Vector3 (.75f, .75f, .75f), .125f).setDelay (.75f);
		LeanTween.scale (confidenceIcon.gameObject, new Vector3 (.25f, .25f, .25f), .825f);
		LeanTween.scale (confidenceIcon.gameObject, new Vector3 (1f, 1f, 1f), .125f).setDelay (1f);
		LeanTween.value (confidenceIcon.gameObject, Color.red, new Color(0.0f, 0.0f, 0.0f, 1.0f), 0.125f).setDelay (0.25f).setOnUpdate(( Color val) => {
			// For some reason gameObject.GetComponent<Image> grabs the parent instead of confidence.gameObject, so get child...
			gameObject.transform.GetChild(0).GetComponent<Image>().color = val;
		});
		LeanTween.value (confidenceIcon.gameObject, new Color(0.0f, 0.0f, 0.0f, 1.0f), Color.red, 0.125f).setDelay (1f).setOnUpdate(( Color val) => {
			// For some reason gameObject.GetComponent<Image> grabs the parent instead of confidence.gameObject, so get child...
			gameObject.transform.GetChild(0).GetComponent<Image>().color = val;
		});
	}

	/*
	 * 
	 * Following functions handle the addition/subtraction of confidence and renders the correct confidence 
	 * 
	 * */

	public void renderAddConfidence() {
		progressDataController.addSceneConfidence ();
		renderNewConfidence ("add");
	}

	public void renderSubConfidence() {
		progressDataController.subSceneConfidence ();
		progressDataController.addWrongAttempts ();
		renderNewConfidence ("sub");
	}

	// Function to render and set slider to the correct value
	public void renderNewConfidence(string addOrSub) {
		// sliderConfidence.value = progressDataController.getSceneProgress ().sceneConfidenceCurrent;
		float newConfidence = progressDataController.getSceneProgress().sceneConfidenceCurrent;
		float priorConfidence;
		// Track priorConfidence to allow for smooth Tweening
		if (addOrSub == "add") {
			priorConfidence = newConfidence - 10; 
		} else {
			priorConfidence = newConfidence + 10;
		}

		// Only Tween slider if the confidence is less than 50;
		if (newConfidence < 50) {
			LeanTween.value (sliderConfidence.gameObject, priorConfidence, newConfidence, 0.5f).setOnUpdate (( float val) => {
				sliderConfidence.value = val;
			});
		}
	}
}