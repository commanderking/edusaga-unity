using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Fungus;

public class XPBar : MonoBehaviour {
	public Slider slider;
	// public PlayerPreferences playerPreferences;
	private float maxXPforLevel;
	public Text levelText;
	private Text XPTextIcon;

	// Scripts to talk to Controller which talks to Data
	private ProgressDataController progressDataController;

	// Variables to assign data grabbed from the progressDataController
	private SceneProgress sceneProgressData;
	private GameProgress gameProgressData;

	// Use this for initialization
	void Start () {
		// Grab the XP Test Icon from the XP Test Icon in the child of parent 
		XPTextIcon = gameObject.transform.parent.transform.GetComponentInChildren<Text>();

		// Make sure Game Data gameObject and Progress Data Controller script exist
		if (!progressDataController) {
			progressDataController = GameObject.Find ("Game Data").GetComponent<ProgressDataController> ();
		} else {
			Debug.Log ("No Game Data Object fund in scene");
		}

		gameProgressData = progressDataController.getGameProgress ();
		sceneProgressData = progressDataController.getSceneProgress ();

		slider.value = gameProgressData.gameXP + sceneProgressData.sceneXPEarned - 50 - (sceneProgressData.sceneLevel - 2) * 30;
		Debug.Log (slider.value);
			//PlayerPrefs.GetFloat ("XP");
		maxXPforLevel = 50.0f + 30.0f * (sceneProgressData.sceneLevel - 1);
			//PlayerPrefs.GetInt("level") - 1);
		slider.maxValue = maxXPforLevel;

		// Render the player's level into the scene
		renderNewLevelText();
	}

	// On correct answer, add appropriate xP Based on number of attempts
	public void updateXP(float points) {
		// In the case where we forget points, default to 10 points
		if (points == 0) {
			points = 10;
		}
		// Add points based on total point total of questions, counting for wrong answers
		float pointsEarned = points / Mathf.Pow (2, (progressDataController.getWrongAttemptsCurrent()));

		// Add pointsEarned and update through Controller
		progressDataController.addSceneXPtoSceneProgress(pointsEarned);

		// Grab the new sceneXPEarned;
		float newXP;
		if (sceneProgressData.sceneLevel == 1) {
			newXP = sceneProgressData.sceneXPEarned;
		} else {
			newXP = gameProgressData.gameXP + sceneProgressData.sceneXPEarned - 50 - (sceneProgressData.sceneLevel - 2) * 30;
			progressDataController.saveSceneProgress (sceneProgressData);
		}
		// Check for level up by player
		if (newXP >= maxXPforLevel) {
			Debug.Log ("Leveled up!");
			int newLevel = progressDataController.levelUpInScene ();

			// Tween to the maximum XP level
			LeanTween.value (slider.gameObject, newXP - pointsEarned, maxXPforLevel - 1, 0.5f).setOnUpdate ((float val) => {
				slider.value = val;
			});

			// Add any extra XP to the new level and set correct XP
			float leftoverXP = newXP - maxXPforLevel;
			newXP = leftoverXP;

			// Highlight new Level through scaling Text
			Invoke("renderNewLevelText", 0.5f);
			LeanTween.scale(levelText.gameObject, new Vector3(1.4f, 1.4f, 1.4f), 0.25f).setDelay(0.5f);
			LeanTween.scale(levelText.gameObject, Vector3.one, 0.25f).setDelay(0.75f);

			// Set slider's maxValue to its new value 
			maxXPforLevel = 50.0f + 30.0f * (newLevel - 1);
			Invoke("renderNewMaxValue", 1.0f);

			// Set slider from 0 to the new XP
			LeanTween.value (slider.gameObject, 0, leftoverXP, 0.5f).setOnUpdate ((float val) => {
				slider.value = val;
			}).setDelay(1.0f);
		} else {
			// Slowly tween slider to value and set the slider value
			LeanTween.value (slider.gameObject, newXP - pointsEarned, newXP, 1.0f).setOnUpdate ((float val) => {
				slider.value = val;
			});
		}

		// Tween the XP Text Icon to pulse
		Vector3 increaseSize = new Vector3(1.4f, 1.4f, 1.4f);
		LeanTween.scale (XPTextIcon.gameObject, increaseSize, 0.5f);
		LeanTween.scale (XPTextIcon.gameObject, Vector3.one, 0.5f).setDelay(0.5f);
	}

	// renders the level text in the menu icon
	public void renderNewLevelText() {
		levelText.text = progressDataController.savedSceneProgress.sceneLevel.ToString ();
			// gameProgressData.level.ToString ();
	}

	// Increase the max value of slider to its new value
	public void renderNewMaxValue() {
		slider.maxValue = maxXPforLevel;
	}
}