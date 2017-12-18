using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingMovement : MonoBehaviour {

   
    Color transparentRed =new Color (0, 0, 0, 0.8f);//Starting opacity for the building should be 20%
    Color addRed = new Color(0, 0, 0, 0.01f);//Incrementation of opacity
	Material tempWorkingColor;
    Material buildColor;
	public Material finalBuildMaterial;//These are all colors that change how the building looks before and after it is built
	public Material inProgressColor;
    public float percentageBuilt;

    public GameObject[] unitList;//List of units buildings can make

	bool paidFor = false;
	bool isCoroutineRunning = false;
	public bool placing;//Whether it is still being placed
	public bool placed;//Whether it has been placed
    public bool shouldMakeUnit;
	public bool canCreate;//Whether it can create units
	public bool creating;
	public bool shouldBuild=false;//Whether it should be getting built
    public bool makingUnit = false;
    public bool isStartingGreatHall = false;//Dont put it in build mode if its the starting great hall
    // Use this for initialization
    void Start () {
        
        placing = true;
        buildColor = this.GetComponent<Renderer>().material;
        buildColor.color -= transparentRed;//Sets the opacity of the building
        canCreate = true;
		if (this.tag == "Barracks"||this.tag=="Pig Farm"||this.tag=="Lumber Mill") {
			tempWorkingColor=inProgressColor;//Sets the in progress material
			this.GetComponent<Renderer> ().material = tempWorkingColor;
		}
    }

    // Update is called once per frame
	
	
    void Update() {
        if (!isStartingGreatHall)
        {
            shouldMakeUnit = false;
            if (placing)
            {
                canCreate = false;
                placed = false;
                UiController.Instance.spawnBuilding.enabled = false;//While it is being selected, the building follows the mouse so it can be placed
                Vector3 temp = this.transform.position;
                temp.x = ClickingUI.Instance.placement.x;
                temp.z = ClickingUI.Instance.placement.z;
                temp.y = 0.5f;
                this.transform.position = temp;
            }

            //shouldn't this be Input.GetMouseButtonDown(0) && placing?
            if (Input.GetMouseButtonDown(0) && !placed)//When the player clicks, place the building where the mouse is
			{
                placing = false;
                UiController.Instance.spawnBuilding.enabled = true;
                ClickingUI.Instance.buildPlace = this.transform.position;//Set the build place for teh worker to move to so he can build the building
                ClickingUI.Instance.buildBuilding = this.gameObject;
                placed = true;
            }
            if (Input.GetMouseButtonDown(1) && !placed)
            {
                Destroy(this.gameObject);
            }
            if (!canCreate && !placing && shouldBuild)//If it isnt being placed and should be getting built, increment the opacity
            {
				
                if (this.tag == "Barracks" && !paidFor)//Pays the cost of the buildings
                {
                    ResourceManager.Instance.gold -= 700;
                    ResourceManager.Instance.wood -= 450;
                    paidFor = true;
                }
				if (this.tag == "Pig Farm" && !paidFor)
				{
					ResourceManager.Instance.gold -= 500;
					ResourceManager.Instance.wood -= 250;

					paidFor = true;
				}
				if (this.tag == "Lumber Mill" && !paidFor)
				{
					ResourceManager.Instance.gold -= 600;
					ResourceManager.Instance.wood -= 450;

					paidFor = true;
				}
                percentageBuilt = (buildColor.color.a - 0.2f) / 0.8f;
                if (buildColor.color.a < 1.0)
                {
                    buildColor.color += addRed;
                }
                else
                {
					if (this.tag == "Barracks"||this.tag == "Pig Farm"||this.tag == "Lumber Mill") {
						this.GetComponent<Renderer> ().material=finalBuildMaterial;//Sets the real material
						if (this.tag == "Pig Farm") {
							ResourceManager.Instance.maxFood += 4;
						}
					}
                    canCreate = true;//Passes into the unit script, allowing it to move again, and allow for it to build units
                }

            }

        }
	}
	public void CreateUnit (int goldCost, int lumberCost, int unitToMake)//Takes the cost of teh unit and where it lies on the array and creates the unit
    {
        makingUnit = true;
        ResourceManager.Instance.gold -= goldCost;
		ResourceManager.Instance.wood -= lumberCost;
       // Debug.Log("here");
        // bool unitCreated = false;
        shouldMakeUnit = true;
        UiController.Instance.uiMode = 1.5f;
        StartCoroutine(WaitTime(unitToMake));
       
           
        }
    
	IEnumerator WaitTime(int unitToMake)
    {
        shouldMakeUnit = true;
        if (isCoroutineRunning)
        {
          
            yield break;
            
        }
        isCoroutineRunning = true;
        for (float buildTime=0; buildTime < 5f; buildTime += Time.deltaTime)
        {
            UiController.Instance.unitProgress.text = "% Completed: " + ((buildTime/5f)*100).ToString("F0");
            yield return 0;
           
        }
        shouldMakeUnit = true;
        if (shouldMakeUnit)
           
        {
            Vector2 unitPlacement2D = Random.insideUnitCircle.normalized;
			Vector3 unitPlacement3D = new Vector3(unitPlacement2D.x, 0.0f, unitPlacement2D.y) * 5f;//Places the unit outside of the building
            VictoryController.Instance.gruntCount++;
            GameObject madeUnit = (GameObject)Instantiate(unitList[unitToMake], this.transform.position + unitPlacement3D, Quaternion.identity);

        }
        //shouldMakeUnit = false;
        isCoroutineRunning = false ;
        UiController.Instance.uiMode = 1f;
        makingUnit = false;
    }
	void OnTriggerEnter(Collider collider){
		if (collider.tag == "Peon") {
			if (!canCreate && !placing) {
				shouldBuild = true;
			}
		}
	}
}
