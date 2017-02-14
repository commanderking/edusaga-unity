using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using Fungus;


public class Scene1DreamQ : MonoBehaviour {

	// Grab script QuestionPanel 
	private QuestionPanel questionPanel;
	public Flowchart flowchart;

	// Use this for initialization
	void Awake () {
		questionPanel = QuestionPanel.Instance ();
	}


	// Send these questions to the questionPanel when called by Fungus
	public void question1 () {
		questionPanel.createQuestionPanel ("How do I greet her?", "nǐ hǎo", "ni hao", "你好"); 
	
	}

	public void question2 () {
		// We need to grab the player's first name to check whether he/she typed their name
		string firstName = flowchart.GetStringVariable("firstName");
		questionPanel.createQuestionPanel ("How should I introduce myself?", "wǒ jiào " + firstName, "wo jiao " + firstName, "我叫 " + firstName);
	}

	/* 
	 * 
	 * These questions are related to the final task 
	 * 
	 * */

	// Greeting prompt
	public void question3() {
		questionPanel.createQuestionPanel ("How do I greet him?", "nǐ hǎo", "ni hao", "你好"); 
	}

	// Name prompt 
	public void question4() {
		string firstName = flowchart.GetStringVariable("firstName");
		questionPanel.createQuestionPanel ("How should I introduce myself?", "wǒ jiào " + firstName, "wo jiao " + firstName, "我叫 " + firstName);
	}

	// Ask for Name prompt
	public void question5() {
		questionPanel.createQuestionPanel ("How can I ask for his name?", "Nǐ jiào shénme míngzì", "ni jiao shenme mingzi", "你叫什么名字");
	}

	// Question 6 and 7 are picture selections located in Scene1DreamPC

	// Bye prompt 
	public void question8() {
		questionPanel.createQuestionPanel ("I should say...", "Zàijiàn", "zaijian", "再见"); 
	}

	/* 
	 * 
	 * These questions are related to the Party Dream Sequence
	 * 
	 * */

	// Taxi Driver asks you questions
	public void question9() {
		string firstName = flowchart.GetStringVariable("firstName");
		questionPanel.createQuestionPanel ("How should I respond?", "wǒ jiào " + firstName, "wo jiao " + firstName, "我叫 " + firstName);
	}

	public void question10() {
		questionPanel.createQuestionPanel ("How can I ask for his name?", "Nǐ jiào shénme míngzì", "ni jiao shenme mingzi", "你叫什么名字", true);
	}

	public void question11() {
		questionPanel.createQuestionPanel ("How can I ask for her name?", "Nǐ jiào shénme míngzì", "ni jiao shenme mingzi", "你叫什么名字", true);
	}

}
