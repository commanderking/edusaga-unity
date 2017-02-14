using UnityEngine;
using System.Collections;
using Fungus;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

	// Remember to put script on controller and drag flowchart on
	public Flowchart flowchart;

	public void loadScene1Review () {
		flowchart.SetBooleanVariable ("nightmare", true);
		//flowchart.ExecuteBlock ("wakeUp");
		//Application.LoadLevel ("1_Review");
	}

	// Function to load any scene
	public void loadCustomScene(string sceneToLoad) {
		SceneManager.LoadScene (sceneToLoad);
	}

	public void loadMenu() {
		loadCustomScene ("0_Main_Menu");
	}

	public void loadAirplane() {
		loadCustomScene ("1_Airplane");
	}

	public void loadAirport() {
		loadCustomScene ("1_Airport");
	}

	public void loadApartment() {
		loadCustomScene ("1_Apartment");
	}

	public void loadReview() {
		loadCustomScene ("1_Review");
	}

	public void loadDream() {
		loadCustomScene ("1_Dream");
	}
}