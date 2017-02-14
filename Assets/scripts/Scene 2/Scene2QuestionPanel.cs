using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;


public class Scene2QuestionPanel : MonoBehaviour {

	//Grab script QuestionPanel 
	private QuestionPanel questionPanel;

	// Use this for initialization
	void Awake () {
		questionPanel = QuestionPanel.Instance ();
	}
	
	// Send these questions to the questionPanel when called by Fungus
	public void question1 () {
		questionPanel.createQuestionPanel ("What should I say to greet her?", "Nǐ hǎo", "ni hao", "你好"); 

	}
	public void question2 () {
		questionPanel.createQuestionPanel ("What should I say to her?", "Jīnlóng càichǎng zài nǎlǐ", "jinlong caichang zai nali", "金龙菜场在哪里");

		//questionPanel.createQuestionPanel ("What should I say to her?", "Jīnlóng càichǎng zài nǎlǐ");
	}
}
