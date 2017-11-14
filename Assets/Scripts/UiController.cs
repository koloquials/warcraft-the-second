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
    public Text names;
    public Text pauseText;
    public Text buildingProgress;
    public Text unitProgress;
    public float uiMode; //0=none, 0.5=building being built, 1=buildings, 2=units
                         // Use this for initialization
    void Start()
    {
        Instance = this;
        uiMode = 0;
    }

    // Update is called once per frame
    void Update () {
        
        //add comments please. 
        
        if (uiMode == 0)
        {
            names.text = "";
        }
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



        

            
            
        

        if (uiMode == 0.5f)
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
            buildingProgress.text = ("Progress: " + (checkIfBuilt.percentageBuilt * 100).ToString("F0") + "%");
            if (checkIfBuilt.canCreate)
            {
                uiMode = 1;
            }
        }
        if (uiMode == 1)
        {
            if (ClickingUI.Instance.previousObject != null)
            {
                names.text = "" + ClickingUI.Instance.previousObject.tag;
            }
            creatingUnit.enabled = false;
            buildStuff.enabled = false;
            spawnBuilding.enabled = false;
            spawnUnit.enabled = true;
            BuildingMovement isMakingUnit = ClickingUI.Instance.previousObject.GetComponent<BuildingMovement>();
            
           


            if (Input.GetKeyDown(KeyCode.U))
            {

                //spawnUnit.interactable = true;
                CreateUnit(ClickingUI.Instance.previousObject);

            }
        }
        if (uiMode== 1.5f){
          names.text="";
                spawnUnit.enabled = false;
                creatingUnit.enabled = true;
          
        }
        if (uiMode == 2)
        {
            if (ClickingUI.Instance.previousObject.tag == "Peon")
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
            buildStuff.enabled = false;
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
    public void CreateUnit(GameObject currentlySelected)
    {
        BuildingMovement shouldBuild = currentlySelected.GetComponent<BuildingMovement>();
        shouldBuild.CreateUnit();
    }
    public void CreateBuilding()
    {
        Instantiate(ClickingUI.Instance.building, ClickingUI.Instance.placement, Quaternion.Euler(0, -90, 0));
    }
    public void AllOff()
    {
        uiMode = 4;
        creatingUnit.enabled = false;
        buildStuff.enabled = false;
        spawnBuilding.enabled = false;
        spawnUnit.enabled = false;
        names.text = "";
    }
    public void Pause()
    {
        pauseText.text = "GAME PAUSED";
        Time.timeScale = 0f;
        pauseMenu.enabled = true;
        pauseButton.enabled = false;
    }
    public void UnPause()
    {
        pauseMenu.enabled = false;
        pauseButton.enabled = true;
        pauseText.text = "";
        Time.timeScale = 1.0f;
        pauseButton.enabled = true;
    }

}
