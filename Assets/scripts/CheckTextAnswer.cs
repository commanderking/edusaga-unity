using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Fungus;

/* DEVELOPER NOTES
 * 
 * Use the checkAnswer() method to check if user input is correct.
 * 
 * For this to work, the following variables must be public in the Fungus flowchart:
 * - String answer : use the Fungus "Get Text" block to make this equal to the user input
 * - String correctAnswer : this is the correct answer that the user's input will be compared to
 * - Boolean verifiedCorrect : if this is marked true (after checkAnswer() runs), the user input was correct (with or with proper tone usage)
 * - Boolean tonesCorrect : if this is marked true (after checkAnswer() runs), the user input had proper use of tones
 */

public class CheckTextAnswer : MonoBehaviour {
	//Access flowchart to save to fungus variables
	public Flowchart flowchart;
	public RemoveTones removeTones;

	// Simple check
	public void simpleCheck() {
		// Remove all punctuation from inputted answer
		string userInput = flowchart.GetStringVariable ("answer").ToLower().Replace(" ", "").Replace("\n", "").Replace("?", "").Replace("!", "").Replace(".", "");
		string correctAnswer = flowchart.GetStringVariable ("correctAnswer").ToLower();
		string noToneAnswer = flowchart.GetStringVariable ("noToneAnswer").ToLower ();
		string characterAnswer = flowchart.GetStringVariable ("characterAnswer").ToLower ();

		// If user is completely correct using tones or characters
		if (userInput == correctAnswer || userInput == characterAnswer) {
			flowchart.SetBooleanVariable ("verifiedCorrect", true);
			flowchart.SetBooleanVariable ("tonesCorrect", true);
		} 

		// If only tones are incorrect
		else if (userInput == noToneAnswer) {
			flowchart.SetBooleanVariable ("verifiedCorrect", true);
			flowchart.SetBooleanVariable ("tonesCorrect", false);
		} 

		// If user attempted to use tones, but some are incorrect
		else if (removeTones.removePinyinTones (userInput) == noToneAnswer) {
			flowchart.SetBooleanVariable ("verifiedCorrect", true);
			flowchart.SetBooleanVariable ("tonesCorrect", false);
		}
		// If incorrect
		else {
			flowchart.SetBooleanVariable ("verifiedCorrect", false);
		}
	}

	//Check to see if answer is correct
	public void checkAnswer () {
		string userInput = flowchart.GetStringVariable ("answer").ToLower();
		string correctAnswer = flowchart.GetStringVariable ("correctAnswer").ToLower();

		Debug.Log ("Obtained variables");

		int ld = levenshteinDistance (userInput, userInput.Length, correctAnswer, correctAnswer.Length);
		Debug.Log ("levenshteinDistance function done...");
		int ldWithTones = levenshteinDistanceWithTones (userInput, userInput.Length, correctAnswer, correctAnswer.Length);
		Debug.Log ("LevenshteinDistance With Tones done...");

		//Compare user input and correct answer
		if ( ldWithTones == 0) { //0 is an exact match - can be changed to < 2 to accomodate for spelling mistakes
			flowchart.SetBooleanVariable ("verifiedCorrect", true);
		} else {
			flowchart.SetBooleanVariable ("verifiedCorrect", false);
		}

		Debug.Log ("Compared user input and correct answer...");

		//Check if tones used properly
		if (ld == 0)
			flowchart.SetBooleanVariable ("tonesCorrect", true);
		else if (ldWithTones == 0 & ld > 0)
			flowchart.SetBooleanVariable ("tonesCorrect", false);

		Debug.Log ("Checked if tones were used correctly...");
	}


	//Compares 2 chars - returns true if English character is equivalent to pinyin character with tone
	public static bool tonesMatch(char a, char b){
		char [,] tones = new char [,] {{'a', 'ā'},  {'a', 'á'}, {'a', 'ǎ'}, {'a', 'à'},
			{'e', 'ē'},  {'e', 'é'}, {'e', 'ě'}, {'e', 'è'},
			{'i', 'ī'},  {'i', 'í'}, {'i', 'ǐ'}, {'i', 'ì'},
			{'o', 'ō'},  {'o', 'ó'}, {'o', 'ǒ'}, {'o', 'ò'},
			{'u', 'ū'},  {'u', 'ú'}, {'u', 'ǔ'}, {'u', 'ù'},
			{'u', 'ǖ'},  {'u', 'ǘ'}, {'u', 'ǚ'}, {'u', 'ǜ'}};

		if (a == b) return true;

		for (int i = 0; i < tones.GetLength(0); i++) {
			if(tones[i, 0] == a && tones[i, 1] == b) 
				return true;
			else if (tones[i, 0] == b && tones[i, 1] == a) 
				return true;
		}

		return false;
	}

	//Compares 2 strings to see how similar they are (0 = same word)
	public static int levenshteinDistance(string s, int len_s, string t, int len_t){
		int cost;

		/* base case: empty strings */
		if (len_s == 0) return len_t;
		if (len_t == 0) return len_s;

		/* test if last characters of the strings match */
		if (s[len_s-1] == t[len_t-1])
			cost = 0;
		else
			cost = 1;

		/* return minimum of delete char from s, delete char from t, and delete char from both */
		return Mathf.Min(Mathf.Min(levenshteinDistance(s, len_s - 1, t, len_t) + 1,
			levenshteinDistance(s, len_s, t, len_t - 1) + 1), 
			levenshteinDistance(s, len_s - 1, t, len_t - 1) + cost);
	}

	//Compares 2 strings to see how similar they are (0 = same word)
	//Takes into account Chinese tones (i.e.  a == á)
	public static int levenshteinDistanceWithTones(string s, int len_s, string t, int len_t){
		int cost;

		/* base case: empty strings */
		if (len_s == 0) return len_t;
		if (len_t == 0) return len_s;

		/* test if last characters of the strings match */
		if (s[len_s-1] == t[len_t-1] || tonesMatch (s [len_s - 1], t [len_t - 1]))
			cost = 0;
		else
			cost = 1;

		/* return minimum of delete char from s, delete char from t, and delete char from both */
		return Mathf.Min(Mathf.Min(levenshteinDistanceWithTones(s, len_s - 1, t, len_t) + 1,
			levenshteinDistanceWithTones(s, len_s, t, len_t - 1) + 1), 
			levenshteinDistanceWithTones(s, len_s - 1, t, len_t - 1) + cost);
	}

}