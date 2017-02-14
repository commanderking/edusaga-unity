using UnityEngine;
using System.Collections;

public class OpenURL : MonoBehaviour {

	// Open URL in new tab
	public void openURL(string website) {
		//Application.OpenURL(website);
		Application.ExternalEval("window.open('" + website + "','_blank')");
	}

	public void openFeedbackLink() {
		openURL ("https://goo.gl/5zQgvJ");
	}

	public void visitEduSaga() {
		openURL ("http://www.edusaga.com");
	}

}
