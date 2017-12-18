﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryController : MonoBehaviour {
    public static VictoryController Instance = new VictoryController();
    public int gruntCount=0;
    public bool inLocation=false;
	// Use this for initialization
	void Start () {
        Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
        
            if (inLocation)//Gets turned on in the SpencersNavAgent Script
            {
                UiController.Instance.AllOff();
                UiController.Instance.winScreen.enabled = true;
            }
        
	}
}
