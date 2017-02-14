using UnityEngine;
using System.Collections;
using System.Collections.Generic;


// Contains all the information about vocab words for a review/practice scene
[System.Serializable]
public class sceneVocabWord {//Object that contains english & pinyin
	public string english;
	public string pinyin;
	public string hanzi;
	public string soundkey;
	public string category;
	public List<string> filledSlots = new List<string>(); // Holds the pinyinPart GameObject for attempted answers during Review Scene
	public bool correct = false; // When answer is correct, this becomes true, relevant to review scene

	//Constructor
	public sceneVocabWord (string eng, string piny, string mand, string sound, string cat, List<string> fillslots, bool corr) {
		english = eng;
		pinyin = piny;
		hanzi = mand;
		soundkey = sound;
		category = cat;
		filledSlots = fillslots;
		correct = corr;
	}
}

public class Scene1VocabList : MonoBehaviour {
	// Attach script that stores all data (AllVocabData); This should be found on the Vocab Controller of each scene;
	public AllVocabData allVocabData;
	// Attach animation script that happens whenever new Vocab is learned
	public VocabLearned vocablearned;
	// Attach Update Notebook; Every time new vocab is learned, the notebook should be updated to reflect the new word learned
	public notebookPanel notebookPanelScript;

	// Stores all the vocabWord classes in a list;
	// TODO: Possibly should just be renamed to Game Vocab List if it's a running tally of game's vocab
	public List<sceneVocabWord> sceneVocabList = new List<sceneVocabWord>();

	void Start() {
		// Find AllVocabData script if null
		if (!allVocabData) {
			allVocabData = GameObject.Find ("Vocab Controller").gameObject.GetComponent<AllVocabData>();
		}
	}

	// Loads appropriate vocabulary for scene1 based on player preferences
	public void loadScene1Vocab() {
		
		// Load the base Scene 1 Vocab (Greetings)
		// addScene1Vocab ();

		// Load Appropriate Player Prefs based on which scenes visited
		if (PlayerPrefs.GetString ("AFM") == "true") {
			//addScene1AFMVocab ();
			Debug.Log ("AFM Added");
		}
		if (PlayerPrefs.GetString("TCR") == "true") {
			// addScene1TCRVocab ();
			Debug.Log ("TCR Added");
		}
	}

	// Takes care of adding the word the gameVocab, sceneVocab, animating, and updating in notebook panel
	public void addWordToAllVocabLists(sceneVocabWord vocab) {
		allVocabData.gameVocabList.Add (vocab);
		allVocabData.sceneVocabList.Add (vocab);
		vocablearned.newVocabLearnedAnimate (vocab.hanzi);
		notebookPanelScript.UpdateVocabCards ();
	}

	public void addnihao() {
		sceneVocabWord nihao = new sceneVocabWord ("Hi", "nǐ hǎo", "你好", "nihao", "Greetings", new List<string> (), false);
		addWordToAllVocabLists (nihao);
	}

	public void addnijiaoshenme() {
		sceneVocabWord nijiaoshenme = new sceneVocabWord ("What's your name?", "nǐ jiào shén me míng zì", "你叫什么名字",
		"nijiaoshenmemingzi", "Greetings", new List<string> (), false);		
		addWordToAllVocabLists (nijiaoshenme);
	}

	public void addwojiao() {
		sceneVocabWord wojiao = new sceneVocabWord ("My name is", "wǒ jiào", "我叫", "wojiao", "Greetings", new List<string> (), false);
		addWordToAllVocabLists (wojiao);
	}

	public void addlaiyige() {
		sceneVocabWord laiyige = new sceneVocabWord ("Get me a...", "lái yī gè", "来一个", "laiyige", "Food", new List<string> (), false);
		addWordToAllVocabLists (laiyige);

	}

	public void addzaijian() {
		sceneVocabWord zaijian = new sceneVocabWord ("Bye", "zài jiàn", "再见", "zaijian", "Greetings", new List<string> (), false);
		addWordToAllVocabLists (zaijian);
	}

	public void addzhazhupai() {
		sceneVocabWord zhazhupai = new sceneVocabWord ("Fried pork", "zhà zhū pái", "炸猪排", "zhazhupai", "Food", new List<string> (), false);
		addWordToAllVocabLists (zhazhupai);
	}

	public void addzhajichi () {
		sceneVocabWord zhajichi = new sceneVocabWord ("Fried chicken", "zhà jī chì", "炸鸡翅", "zhajichi", "Food", new List<string> (), false);
		addWordToAllVocabLists (zhajichi);
	}

	public void addchunjuan () {
		sceneVocabWord chunjuan = new sceneVocabWord ("Spring Rolls", "chūn juǎn", "春卷", "chunjuan", "Food", new List<string> (), false);
		addWordToAllVocabLists (chunjuan);
	}

	public void addshutiao () {
		sceneVocabWord shutiao = new sceneVocabWord ("French Fries", "shǔ tiáo", "薯条", "shutiao", "Food", new List<string> (), false);
		addWordToAllVocabLists (shutiao);
	}

	public void addbingqilin () {
		sceneVocabWord bingqilin = new sceneVocabWord ("Ice Cream", "bīng qí lín", "冰淇淋", "bingqilin", "Food", new List<string> (), false);
		addWordToAllVocabLists (bingqilin);
	}

	public void addpai () {
		sceneVocabWord pai = new sceneVocabWord ("Pie", "pài", "派", "pai", "Food", new List<string> (), false);
		addWordToAllVocabLists (pai);
	}
		
 	public void addTCRVocab() {
		addzhajichi ();
		addzhazhupai ();
		addchunjuan ();
		addlaiyige ();
	}

	public void addAFMVocab() {
		addshutiao ();
		addbingqilin ();
		addpai ();
		addlaiyige ();
	}

	/*
	 * 
	 * Methods to get Scene or Game Vocabulary
	 * 
	 * */

	public List<sceneVocabWord> getSceneVocab() {
		Debug.Log (allVocabData.sceneVocabList);
		return allVocabData.sceneVocabList;
	}

}
