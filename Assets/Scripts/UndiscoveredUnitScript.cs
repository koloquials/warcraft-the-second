using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndiscoveredUnitScript : MonoBehaviour {
	public Transform sightSphere;
	public Transform gOparent;
	GameObject gOSightSphere;

	// Use this for initialization
	void Start () {//This script is for undiscovered units, so they dont reveal the map before they are discovered
			  	   //And they do reveal the map when they are discovered

		gOparent = this.transform.parent;
		sightSphere = gOparent.transform.GetChild (1);
		Debug.Log(sightSphere);
	}
		
	void OnTriggerEnter(Collider collider){
		if (collider.gameObject.tag == "Grunt"||collider.gameObject.tag == "Peon") {
			sightSphere.gameObject.SetActive (true);

			this.gameObject.SetActive (false);

		}
	}
}
