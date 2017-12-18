using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoaderScript : MonoBehaviour {
	public Canvas loadingBar;
	public AudioSource speechSpeaker;
	// Use this for initialization
	void Start () {
		speechSpeaker = GameObject.FindGameObjectWithTag ("Sound Guy").GetComponent<AudioSource>();
	}


	public void SwitchScene(){
		Transform hoveredButton = this.transform.GetChild(0);
		Text buttonText = hoveredButton.GetComponent<Text>();
		if (buttonText.text == "Begin") {
			SceneManager.LoadScene ("Intro Scene");
		}
		if (buttonText.text == "Join the Horde") {
			loadingBar.enabled = true;
			speechSpeaker.Stop ();
			SceneManager.LoadScene ("Building_and_Unit_UI");
		}
	}
}
