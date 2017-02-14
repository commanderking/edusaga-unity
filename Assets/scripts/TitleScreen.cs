using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour {
	public GameObject clouds;
	public GameObject cloudsReflection;
	public GameObject titleMenu;
	public GameObject secondTitleScreen;
	public AudioSource oceanSource;

	// Use this for initialization
	void Start () {
		// Play Audio
		oceanSource.Play();
		// Clouds should move indefinitely
		float cloudsY = clouds.transform.position.y;
		LeanTween.move(clouds, new Vector3(-1000f, cloudsY, 0f), 300.0f) .setEase( LeanTweenType.easeOutQuad );
		float cloudsReflectionY = cloudsReflection.transform.position.y;
		LeanTween.move(cloudsReflection, new Vector3(-1000f, cloudsReflectionY, 0f), 300.0f) .setEase( LeanTweenType.easeOutQuad );

		// Make each of the titleMenu elements fade in
		// Currently assumes logo is the first Child
		GameObject logo = titleMenu.transform.GetChild (0).gameObject; 
		LeanTween.alpha (logo.GetComponent<RectTransform> (), 1f, 2f).setDelay (0.5f);

		// Fade red start game button in
		GameObject redDot = titleMenu.transform.GetChild (1).GetChild(0).gameObject;
		LeanTween.alpha (redDot.GetComponent<RectTransform> (), 1f, 2f).setDelay (0.5f);

		// Currently assumes Start Button is second child
		GameObject startButton = titleMenu.transform.GetChild (1).GetChild(1).gameObject;
		GameObject startButtonText = startButton.transform.GetChild (0).gameObject;
		LeanTween.textAlpha (startButtonText.GetComponent<RectTransform> (), 1f, 2f).setDelay (0.5f);
	}

	// Call this onSubmit Button when user clicks the start Button
	public void startClicked() {
		secondTitleScreen.SetActive (true);
		LeanTween.alpha(secondTitleScreen.GetComponent<RectTransform>(), 1f, 0.2f).setDelay(0f);
		Invoke ("loadGame", 2);
	}

	// Load first scene in game
	public void loadGame() {
		SceneManager.LoadScene ("1_Airplane");
	}
}
