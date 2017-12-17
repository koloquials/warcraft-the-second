using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndiscoveredUnitScript : MonoBehaviour {
	public Transform sightSphere;
	public Transform gOparent;
	GameObject gOSightSphere;
	// Use this for initialization
	void Start () {
		gOparent = this.transform.parent;
		sightSphere = gOparent.transform.GetChild (1);
		//sightSphere.gameObject.SetActive(false);
		//gOSightSphere.SetActive (false);
		Debug.Log(sightSphere);
	}

	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter(Collider collider){
		if (collider.gameObject.tag == "Grunt"||collider.gameObject.tag == "Peon") {
			sightSphere.gameObject.SetActive (true);
			Debug.Log ("TURN ON PLEASE");
			this.gameObject.SetActive (false);
		}
	}
}
