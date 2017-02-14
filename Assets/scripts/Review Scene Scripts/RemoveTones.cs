using UnityEngine;
using System.Collections;

public class RemoveTones : MonoBehaviour {

	public string removePinyinTones(string pinyinWithTones) {
		string pinyinWithoutTones = "";
		foreach (char c in pinyinWithTones) {
			// Replace the tones with the non toned letter
			if (c < 128) {
				pinyinWithoutTones += c;
			} else {
				switch (c) {
				case 'ā': 
				case 'á': 
				case 'ǎ':
				case 'à': 
					pinyinWithoutTones += 'a';
					break;
				case 'ē':
				case 'é':
				case 'ě':
				case 'è':
					pinyinWithoutTones += 'e';
					break;
				case 'ī':
				case 'í':
				case 'ǐ':
				case 'ì': 
					pinyinWithoutTones += 'i';
					break;
				case 'ō':
				case 'ó':
				case 'ǒ':
				case 'ò': 
					pinyinWithoutTones += 'o';
					break;
				case 'ū':
				case 'ú':
				case 'ǔ':
				case 'ù':
				case 'ǖ':
				case 'ǘ':
				case 'ǚ':
				case 'ǜ':
					pinyinWithoutTones += 'u';
					break;
				default: 
					pinyinWithoutTones += c;
					break;
				}

			}
		}
		return pinyinWithoutTones;
	}
}
