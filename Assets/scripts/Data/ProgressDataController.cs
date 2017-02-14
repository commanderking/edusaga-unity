using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * 
 * Script that acts as controller between Game Object "Views" and ProgressData.cs Model
 * Attaches to Game Data Script
 * 
 * */

public class ProgressDataController : MonoBehaviour {

	public static ProgressDataController Instance;

	// Stores a list of scenes and high scores in those
	public List<SceneProgress> savedSceneProgressRecord = new List<SceneProgress>();

	// Stores current Scene progress
	public SceneProgress savedSceneProgress;

	// Stores the overall Game Progress 
	public GameProgress savedGameProgress;

	// Persist through levels; Destroy other instance of this if it exists
	void Awake () {
		if (Instance == null) {
			DontDestroyOnLoad (gameObject);
			Instance = this;
			savedSceneProgress.sceneConfidenceCurrent = 30;
		} else if (Instance != this) {
			Destroy (gameObject);
		}
	}

	// Game should feed in the current inSceneProgress
	public void saveSceneProgress(SceneProgress inSceneProgress) {
		savedSceneProgress = inSceneProgress;
	}

	// Game should feed in the current gameProgress 
	public void saveSceneProgress (GameProgress gameProgress) {
		savedGameProgress = gameProgress;
	}

	// Called by other scripts to get the saved Scene Progress
	public SceneProgress getSceneProgress () {
		// Default level start
		if (savedSceneProgress == null) {
			savedSceneProgress = new SceneProgress ();
			savedSceneProgress.sceneLevel = 1;
		} 
		// TODO: Trigger on new player and set level correctly; Should load from file first also..
		// Hack for now, if sceneLevel is 0, that means it's a new player
		if (savedSceneProgress.sceneLevel == 0) {
			savedSceneProgress.sceneLevel = 1;
		}
		return savedSceneProgress;
	}

	public GameProgress getGameProgress () {
		// If first time player, create a new Game Progress
		if (savedGameProgress == null) {
			savedGameProgress = new GameProgress ();
			savedGameProgress.level = 1;
			savedGameProgress.gameXP = 0;
		}
		return savedGameProgress;
	}

	// Adjust the XP in SceneProgress
	public void addSceneXPtoSceneProgress (float XP) {
		savedSceneProgress.sceneXPEarned += XP;
	}

	// Update the Level, return the new level of the player in the scene
	public int levelUpInScene() {
		savedSceneProgress.sceneLevel += 1;
		return savedSceneProgress.sceneLevel;
	}

	/*
	 * 
	 * Functions related to Confidence
	 * Always return the currentConfidence level to functions that call it
	 * 
	 * 
	 * */

	public void addSceneConfidence() {
		// Add confidence if there's still confidence to add
		if (savedSceneProgress.sceneConfidenceCurrent <= 40) {
			savedSceneProgress.sceneConfidenceCurrent += 10;
		}
	}

	public void subSceneConfidence() {
		savedSceneProgress.sceneConfidenceCurrent -= 10;
	}

	/*
	 * 
	 * Functions related to Wrong Attempts
	 * 
	 * */

	public void addWrongAttempts() {
		savedSceneProgress.wrongAttemptsCurrent++;
		savedSceneProgress.wrongAttemptsScene++;
	}

	// Reset should be called after a correct answer
	public void resetWrongAttempts() {
		savedSceneProgress.wrongAttemptsCurrent = 0;
	}

	public int getWrongAttemptsCurrent() {
		return savedSceneProgress.wrongAttemptsCurrent;
	}
}
