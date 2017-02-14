using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Fungus;
	
[CommandInfo("Scripting", 
             "Incorrect Answer",
			 "Add wrongAnswer xP, change level, and confidence")]

public class subConfidence : Command {
	//Grab the ConfidenceBar.cs script; No need for XP because wrong Answers do not deduct XP
	private ConfidenceBar confidenceBar;

	void Start () {
		// Get the Confidence Bar from the scene;
		confidenceBar = GameObject.Find ("Confidence Panel").GetComponent<ConfidenceBar> ();
	}

	public override void OnEnter() {
		confidenceBar.renderSubConfidence ();
		Continue ();
	}

}