using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class notebookVocabCard : MonoBehaviour {

	public string category;
	public Text mandarin;
	public Text pinyin;
	public Text english;
	public AudioSource wordAudio;
	public Image picture;
/*
	public void setImage(Texture2D texture){
		picture.sprite = Sprite.Create(texture, new Rect(0, 0, 1, 1), new Vector2(0.5f, 0.5f));
	}*/
}
