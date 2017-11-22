﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		UnitStatManager statManager = GetComponent<UnitStatManager>();

		// Die if killed.
		if (statManager.healthCurrent < 0) {
			Destroy (this.gameObject);
		}
	}
}
