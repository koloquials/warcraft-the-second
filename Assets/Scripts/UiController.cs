using System.Collections;
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
    public Text goldText, woodText, oilText;
    public Text names;
    public Text pauseText;
    public Text buildingProgress;
    public Text unitProgress;
    public Text badResources;
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
        goldText.text = "Gold: " + ResourceManager.Instance.gold;
        woodText.text = "Wood: " + ResourceManager.Instance.wood;
        oilText.text = "Oil: " + ResourceManager.Instance.oil;

        if (uiMode == 0)//uiMode 0 is the mode when you have nothing selected, or clicked on the ground/trees
        {//It turns off basically all the uiElements
            badResources.text = "";
            names.text = "";

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
            }
            }
            creatingUnit.enabled = false;
            buildStuff.enabled = false;
            spawnBuilding.enabled = false;
            spawnUnit.enabled = true;

            if (Input.GetKeyDown(KeyCode.U))
            {

                //spawnUnit.interactable = true;
                CreateUnit(ClickingUI.Instance.previousObject, "Grunt");

            }
        }
        if (uiMode == 1.5f) {//Creating a unit, show the progress and hide the button to make a new one
            names.text = "";
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
            if (ClickingUI.Instance.previousObject != null)
            {
                names.text = "" + ClickingUI.Instance.previousObject.tag;
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
    public void CreateUnit(GameObject currentlySelected, string unitToMake)//Creates a unit around the currently selected building
    {
        BuildingMovement shouldBuild = currentlySelected.GetComponent<BuildingMovement>();
        bool canMakeUnit = true;
        int goldCost=0;
        if (unitToMake == "Grunt") { goldCost = 600; }
        if (unitToMake == "Peon") { goldCost = 400; }
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

                shouldBuild.CreateUnit(goldCost);
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
                Instantiate(ClickingUI.Instance.building, ClickingUI.Instance.placement, Quaternion.Euler(0, -90, 0));
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
