using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoaderScript : MonoBehaviour {
	public Canvas loadingBar;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SwitchScene(){


		Transform hoveredButton = this.transform.GetChild(0);
		Text buttonText = hoveredButton.GetComponent<Text>();
		if (buttonText.text == "Begin") {
			SceneManager.LoadScene ("Intro Scene");
		}
		if (buttonText.text == "Join the Horde") {
			loadingBar.enabled = true;
			SceneManager.LoadScene ("Building_and_Unit_UI");

		}




	}


}
