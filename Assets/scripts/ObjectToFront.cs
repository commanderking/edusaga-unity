using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

public class ObjectToFront: MonoBehaviour, IPointerDownHandler
{

	public void OnPointerDown(PointerEventData eventData)
	{	
		// This is specifically meant for pinyinpart right now
		// Brings the grandparent to the front so that the pinyin part does not hide
		// behind the practice button zone when dragging from drop zone back to practice button zone
		gameObject.transform.parent.parent.SetAsLastSibling ();

	}
}