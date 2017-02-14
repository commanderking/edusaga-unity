using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* 
 * 
 * This is the data structure that stores all level, XP, and confidence for each level
 * 
 * */

[System.Serializable]
public class SceneProgress {
	public int sceneNumber; // Tracks whether this is for Scene 1, Scene 2, Scene 3, etc...
	public int sceneLevel; // Level in the scene
	public float sceneXPEarned; // Tracks total XP earned in just this scene
	public int sceneConfidenceCurrent;
	public int wrongAttemptsScene; // Total wrong attempts in the scene
	public int wrongAttemptsCurrent; // Number of wrong attempts in a row since last right answer
}

public class GameProgress {
	public int level;
	public float gameXP; 
}