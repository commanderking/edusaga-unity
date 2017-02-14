using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Fungus;

[CommandInfo("Scripting", 
             "Correct Answer",
             "Add xP, change level, and confidence")]

public class addConfidence : Command {
	//Grab the XPBar.cs and ConfidenceBar.cs scripts
	public XPBar xpBar;
	public ConfidenceBar confidenceBar;
	public int points;
	private int wrongAttempts;

	void Start () {
		if (xpBar == null) {
			xpBar = GameObject.Find ("XP Slider").GetComponent<XPBar> ();
		}

		if (confidenceBar == null) {
			confidenceBar = GameObject.Find ("Confidence Panel").GetComponent<ConfidenceBar> ();
		}
	}

	public override void OnEnter() {
		xpBar.updateXP (points);
		confidenceBar.renderAddConfidence ();
		Continue ();
	}
}

/*
public class addConfidence : Command {
	//Grab the confidenceBar.cs script
	public confidenceBar confidenceBarScript;
	public int points;

	public override void OnEnter() {
		//addHealth is a function defined in health.cs which deducts points 
		confidenceBarScript.handleAnswers (true, points);
		Continue ();
	}
}
*/
