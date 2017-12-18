using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextMovementScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 temp = this.transform.position;
		temp.y += 0.75f;
		this.transform.position = temp;
	}
}
