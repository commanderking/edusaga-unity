using UnityEngine;
using System.Collections;

public class BringToFront : MonoBehaviour {
	//When called, make sure it's the last child in the UI panel so that it will sit on top of UI.
	void OnEnable () {
		transform.SetAsLastSibling ();
	}
}
