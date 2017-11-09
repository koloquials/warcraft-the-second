using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour {
    public static UiController Instance = new UiController();

    public Canvas spawnUnit;
    public Canvas spawnBuilding;
    public Canvas buildStuff;
    public Text buildingProgress;
    public float uiMode; //0=none, 0.5=building being built, 1=buildings, 2=units
                         // Use this for initialization
    void Start()
    {
        Instance = this;
        uiMode = 0;
    }

    // Update is called once per frame
    void Update () {
        if (uiMode == 0)
        {
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


        }
        if (uiMode == 0.5f)
        {
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


            buildStuff.enabled = false;
            spawnBuilding.enabled = false;
            spawnUnit.enabled = true;


            if (Input.GetKeyDown(KeyCode.U))
            {

                //spawnUnit.interactable = true;
                CreateUnit(ClickingUI.Instance.previousObject);

            }
        }
        if (uiMode == 2)
        {
            if (spawnBuilding.enabled == false)
            {
                spawnBuilding.enabled = true;
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
        Vector2 unitPlacement2D = Random.insideUnitCircle.normalized;
        Vector3 unitPlacement3D = new Vector3(unitPlacement2D.x, 0.0f, unitPlacement2D.y) * 3f;
        GameObject madeUnit = Instantiate(ClickingUI.Instance.unit, currentlySelected.transform.position + unitPlacement3D, Quaternion.identity);
        Vector3 temp = madeUnit.transform.position;
        temp.y = 0.5f;
        madeUnit.transform.position = temp;
    }
    public void CreateBuilding()
    {
        Instantiate(ClickingUI.Instance.building, ClickingUI.Instance.placement, Quaternion.Euler(0, -90, 0));
    }

}
