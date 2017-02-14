using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class getValueDropdown : MonoBehaviour {

	public Dropdown dropdown;
	public string nationality; 

	public void dropdownValue() {
		nationality = dropdown.itemText.ToString();
		Debug.Log(nationality);
	}

}