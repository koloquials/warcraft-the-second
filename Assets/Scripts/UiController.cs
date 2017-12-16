﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour {

    //please capitalise the 'i' in UI. It would make me so so happy. 

    public static UiController Instance = new UiController();

    public Canvas spawnUnit;
    public Canvas spawnBuilding;
    public Canvas buildStuff;
    public Canvas creatingUnit;
    public Canvas winScreen;
    public Canvas pauseMenu;
    public Canvas pauseButton;
	public Button trollButton;
	public Button unitButton;
    public Text goldText, woodText, oilText;
    public Text names;
    public Text buttonText;
    public Text pauseText;
    public Text buildingProgress;
    public Text unitProgress;
    public Text badResources;
	public Text displayCost;
	 Image unitImage;
	public Sprite peonImage;
	public Sprite gruntImage;
	public Sprite trollImage;
	public Sprite barracksImage;
	public Sprite greatHallImage;
	public Image selectedUnitImage;
    public float uiMode; //0=none, 0.5=building being built, 1=buildings, 1.5=building unit, 2=units
                         // Use this for initialization
    void Start()
    {
        UnPause();
        Instance = this;
        uiMode = 0;
    }

    // Update is called once per frame
    void Update() {
        goldText.text = "" + ResourceManager.Instance.gold;
        woodText.text = "" + ResourceManager.Instance.wood;
        oilText.text = "" + ResourceManager.Instance.oil;

        if (uiMode == 0)//uiMode 0 is the mode when you have nothing selected, or clicked on the ground/trees
        {//It turns off basically all the uiElements
            badResources.text = "";
            names.text = "";
			selectedUnitImage.enabled = false;

            if (spawnUnit.enabled == true)
            {
                spawnUnit.enabled = false;
            }
            if (spawnBuilding.enabled == true)
            {
                spawnBuilding.enabled = false;
            }
            if (buildStuff.enabled == true)
            {
                buildStuff.enabled = false;
            }
            if (creatingUnit.enabled == true)
            {
                creatingUnit.enabled = false;
            }
        }

        if (uiMode == 0.5f)//For when a building is being built, it shows the building progress
        {
            names.text = "";
			selectedUnitImage.enabled = false;
            if (buildStuff.enabled == false)
            {
                buildStuff.enabled = true;
            }
            if (spawnUnit.enabled == true)
            {
                spawnUnit.enabled = false;
            }
            if (spawnBuilding.enabled == true)
            {
                spawnBuilding.enabled = false;
            }

            BuildingMovement checkIfBuilt = ClickingUI.Instance.previousObject.GetComponent<BuildingMovement>();
            buildingProgress.text = ("Progress: " + (checkIfBuilt.percentageBuilt * 100).ToString("F0") + "%");//Gets the building progress
            if (checkIfBuilt.canCreate)//If it can make units, go to the standard building ui
            {
                uiMode = 1;
            }
        }
        if (uiMode == 1)//Standard building ui
        {
            if (ClickingUI.Instance.previousObject != null)
            {
                if (ClickingUI.Instance.previousObject.tag != "Ground"){
                names.text = "" + ClickingUI.Instance.previousObject.tag;
					selectedUnitImage.enabled = true;
					if(ClickingUI.Instance.previousObject.tag=="Barracks"){selectedUnitImage.sprite=barracksImage;}
					if(ClickingUI.Instance.previousObject.tag=="Great Hall"){selectedUnitImage.sprite=greatHallImage;}
            }
            }
            if (ClickingUI.Instance.previousObject.tag =="Great Hall")
            {
                buttonText.text = "Create Peon";
				unitButton.GetComponent<Image> ().sprite=peonImage;
			
				trollButton.gameObject.SetActive (false);
            }
            if (ClickingUI.Instance.previousObject.tag == "Barracks")
            {
                buttonText.text = "Create Grunt";
				unitButton.GetComponent<Image> ().sprite=gruntImage;
				trollButton.gameObject.SetActive (true);
            }
            
                creatingUnit.enabled = false;
            buildStuff.enabled = false;
            spawnBuilding.enabled = false;
			if (ClickingUI.Instance.previousObject.tag != "Gold Mine") {
				spawnUnit.enabled = true;
			}

          
        }
        if (uiMode == 1.5f) {//Creating a unit, show the progress and hide the button to make a new one
            names.text = "";
			selectedUnitImage.enabled = false;
            spawnUnit.enabled = false;
            creatingUnit.enabled = true;

        }
        if (uiMode == 2)//Unit movement ui
        {
            if (ClickingUI.Instance.previousObject.tag == "Peon")//If the unit is a peon, allow the option to make a barracks
            {
                if (spawnBuilding.enabled == false)
                {
                    spawnBuilding.enabled = true;
                }
            }
			if (ClickingUI.Instance.previousObject.tag != "Peon") {
				if (spawnBuilding.enabled == true)
				{
					spawnBuilding.enabled = false;
				}
			}
            if (ClickingUI.Instance.previousObject != null)
            {
                names.text = "" + ClickingUI.Instance.previousObject.tag;
				selectedUnitImage.enabled = true;
				if(ClickingUI.Instance.previousObject.tag=="Peon"){selectedUnitImage.sprite=peonImage;}
				if(ClickingUI.Instance.previousObject.tag=="Grunt"){selectedUnitImage.sprite=gruntImage;}
				if(ClickingUI.Instance.previousObject.tag=="Troll"){selectedUnitImage.sprite=trollImage;}
		
            }
            buildStuff.enabled = false;//Allow the unit to move
            spawnUnit.enabled = false;
            SpencersnavAgent unitMove = ClickingUI.Instance.previousObject.GetComponent<SpencersnavAgent>();
            unitMove.canMove = true;
            ClickingUI.Instance.builderUnit = ClickingUI.Instance.previousObject;
            if (Input.GetKeyDown(KeyCode.B))
            {
                CreateBuilding();
            }
        }
    }
    public void CreateGenericUnit(GameObject currentlySelected, string unitToMake)//Creates a unit around the currently selected building
    {
        BuildingMovement shouldBuild = currentlySelected.GetComponent<BuildingMovement>();
        bool canMakeUnit = true;
        int goldCost=0;
		int lumberCost = 0;
		int unitToMakeInt=0;
        if (unitToMake == "Grunt") { goldCost = 600; }
        if (unitToMake == "Peon") { goldCost = 400; }
		if(unitToMake=="Troll"){goldCost =500; lumberCost = 50; unitToMakeInt=1;}
        if (!shouldBuild.makingUnit)
        {
            if (ResourceManager.Instance.gold < goldCost)
            {
                StartCoroutine(NotEnoughResources("Gold"));
                badResources.text = "";
                canMakeUnit = false;
            }
            if (canMakeUnit)
            {
                //  badResources="Not Enough Gold"

				shouldBuild.CreateUnit(goldCost, lumberCost, unitToMakeInt);
            }
        }
    }
    public void CreateBuilding()//Creates a building
    {
        bool canMakeUnit = true;
        
            if (ResourceManager.Instance.gold < 700)
            {
                if (ResourceManager.Instance.wood < 450)
                {
                    StartCoroutine(NotEnoughResources("Resources"));
                }
                else
                {
                    StartCoroutine(NotEnoughResources("Gold"));
                }
                badResources.text = "";
                canMakeUnit = false;
            }
            else if (ResourceManager.Instance.wood < 450)
            {
                StartCoroutine(NotEnoughResources("Wood"));
                badResources.text = "";
                canMakeUnit = false;
            }
            if (canMakeUnit)
            {
			Instantiate(ClickingUI.Instance.building, ClickingUI.Instance.placement, Quaternion.identity);
            }
        
    }
        public void AllOff()//Turns all the ui off
        {
            uiMode = 4;//Sets the ui to 4 so it doesnt immediatly turn some ui elements back on. uiMode 4 is not an actual ui State
            creatingUnit.enabled = false;
            buildStuff.enabled = false;
            spawnBuilding.enabled = false;
            spawnUnit.enabled = false;
            names.text = "";
        }
        public void Pause()//Pauses the game
        {
            pauseText.text = "GAME PAUSED";
            Time.timeScale = 0f;
            pauseMenu.enabled = true;
            pauseButton.enabled = false;
        }
        public void UnPause()//Resumes the game
        {
            pauseMenu.enabled = false;
            pauseButton.enabled = true;
            pauseText.text = "";
            Time.timeScale = 1.0f;
            pauseButton.enabled = true;
        }

    
    IEnumerator NotEnoughResources(string notEnoughOf)
    {
        for (float time = 0; time <180; time++)
        {
            Debug.Log(time);
            badResources.text = "Not Enough " + notEnoughOf;
            yield return 0;
        }
        badResources.text = "";


    }
}
