using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingMovement : MonoBehaviour {

    public bool placing;//Whether it is still being placed
    public bool placed;//Whether it has been placed


	
    bool placing;//Whether it is still being placed
    bool placed;//Whether it has been placed

   public bool canCreate;//Whether it can create units
   public bool creating;
   public bool shouldBuild;//Whether it should be getting built
    Color transparentRed =new Color (0, 0, 0, 0.8f);//Starting opacity for the building should be 20%
    Color addRed = new Color(0, 0, 0, 0.01f);//Incrementation of opacity
    Material buildColor;
    public float percentageBuilt;
    public GameObject[] unitList;
    public bool shouldMakeUnit;
    bool isCoroutineRunning = false;
    // Use this for initialization
    void Start () {
        
        placing = true;
        buildColor = this.GetComponent<Renderer>().material;
        buildColor.color -= transparentRed;//Sets the opacity of the building
        canCreate = true;
    }

    // Update is called once per frame
	
	
    void Update() {
        shouldMakeUnit = false;
        if (placing) {
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
        if (Input.GetMouseButtonDown(0)&&!placed)//When the player clicks, place the building where the mouse is
        {

            placing = false;
            UiController.Instance.spawnBuilding.enabled = true;
            ClickingUI.Instance.buildPlace = this.transform.position;//Set the build place for teh worker to move to so he can build the building
            ClickingUI.Instance.buildBuilding = this.gameObject;
            placed = true;
            
        }
        if (!canCreate&&!placing&&shouldBuild)//If it isnt being placed and should be getting built, increment the opacity
        {
	//setting your build percentage with colours feels really janky. Should be other way around.
		
            percentageBuilt = (buildColor.color.a - 0.2f) / 0.8f;
            if (buildColor.color.a < 1.0)
            {
                buildColor.color += addRed;
            }
            else
            {
                canCreate = true;//Passes into the unit script, allowing it to move again, and allow for it to build units
            }
            
        }
	}
    public void CreateUnit()
    {
        Debug.Log("here");
        // bool unitCreated = false;
        shouldMakeUnit = true;
        UiController.Instance.uiMode = 1.5f;
        StartCoroutine(WaitTime());
       
           
        }
    
    IEnumerator WaitTime()
    {
        if (isCoroutineRunning)
        {
            Debug.Log("Broke");
            yield break;
            
        }
        isCoroutineRunning = true;
        for (float buildTime=0; buildTime < 2f; buildTime += Time.deltaTime)
        {
            UiController.Instance.unitProgress.text = "% Completed: " + ((buildTime/2f)*100).ToString("F0");
            yield return 0;
           
        }
        shouldMakeUnit = true;
        if (shouldMakeUnit)
            Debug.Log("Made Unit");
        {
            Vector2 unitPlacement2D = Random.insideUnitCircle.normalized;
            Vector3 unitPlacement3D = new Vector3(unitPlacement2D.x, 0.0f, unitPlacement2D.y) * 4f;
            VictoryController.Instance.gruntCount++;
            GameObject madeUnit = (GameObject)Instantiate(unitList[0], this.transform.position + unitPlacement3D, Quaternion.identity);

        }
        shouldMakeUnit = false;
        isCoroutineRunning = false ;
        UiController.Instance.uiMode = 1f;
    }
}
