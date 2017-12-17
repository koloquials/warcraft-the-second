using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickingUI : MonoBehaviour {
	// TODO: comment these variables!!! are all of these assigned in Inspector? which ones aren't?
    public static ClickingUI Instance = new ClickingUI();
    public MeshRenderer mostRecent;
    public MeshRenderer wireframe;
    public GameObject previousObject;
    public GameObject builderUnit;
    public Vector3 placement;
    public GameObject barracks;
	public GameObject pigFarm;
	public GameObject lumberMill;

    public GameObject unit;
    public bool isClickingButton;
    public Vector3 buildPlace = Vector3.zero;
    public GameObject buildBuilding;
	// Use this for initialization
	void Start () {
        Instance = this;
        
	}

    // Update is called once per frame
    void Update()
    {

        // Debug.Log(uiMode);
	    
	    // TODO: write comments!!! what is this doing?

        Ray placementRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        float rayDist = 10000f;

        Debug.DrawRay(placementRay.origin, placementRay.direction, Color.yellow);


        RaycastHit placementRayHit = new RaycastHit();

	    // TODO: where is "isClickingButton" assigned? your group members don't know!!! TELL US MORE ABOUT IT
        if (!isClickingButton)
        {
            if (Physics.Raycast(placementRay, out placementRayHit, rayDist))
            {
                if (placementRayHit.transform.tag == "Ground")//|| placementRayHit.transform.tag == "Tree")
                {
                    placement = placementRayHit.point;
                }

            }
            if (Input.GetMouseButtonDown(0))
            {
               // for(int i=0; i < previousObject.Length; i++)
               // {
              //      previousObject[i] = null;
               // }
                
                Ray shootRay = Camera.main.ScreenPointToRay(Input.mousePosition);

                float maxRayDist = 10000f;

                Debug.DrawRay(shootRay.origin, shootRay.direction, Color.yellow);


		// This raycast checks what the player is clicking on
                RaycastHit shootRayHit = new RaycastHit();

                if (Physics.Raycast(shootRay, out shootRayHit, maxRayDist))
                {
		// If the last thing you clicked on was a unit, and its not what you are currently clicking on, disallow that previous unit from movement
                    if (shootRayHit.transform.gameObject != previousObject)
                    {
						if (previousObject != null &&( previousObject.tag == "Peon"||  previousObject.tag == "Grunt"||  previousObject.tag == "Troll"||  previousObject.tag == "Zul'Jin"))
                        {
                            SpencersnavAgent unitMove = previousObject.GetComponent<SpencersnavAgent>();
                            unitMove.canMove = false;
							unitMove.chosen = false;
                        }
                    }
		// IF the thing has children, set the child of the thing to the wireframe object so it can be turned on
                    if (shootRayHit.transform.childCount > 0)
                    {
                        wireframe = shootRayHit.transform.GetChild(0).GetComponentInChildren<MeshRenderer>();
                    }
                    //If it has no children, set the wireframe object to the thing itself so as to not have any null object errors
                    if (shootRayHit.transform.childCount == 0)
                    {
                        wireframe = shootRayHit.transform.GetComponent<MeshRenderer>();
                    }
                    

                    //Allows the player to deselect the currently selected object
                    if (previousObject != null)
                    {
                        if (previousObject.transform.childCount > 0)
                        {
                            if (previousObject.transform.GetChild(0).name == "Wireframe")
                            {
                                if (wireframe != mostRecent && mostRecent != null)
                                {

                                    mostRecent.enabled = false;
                                    UiController.Instance.uiMode = 0;

                                }
                            }
                        }
                    }



		// If the thing you are clicking on is a barracks, turn on the wireframe
                    mostRecent = wireframe;
                    previousObject = shootRayHit.transform.gameObject;
                    if (shootRayHit.transform.childCount > 0)
                    {
                        if (shootRayHit.transform.GetChild(0).name == "Wireframe")
                        {
                            wireframe.enabled = true;
							if (shootRayHit.transform.tag == "Barracks"|| shootRayHit.transform.tag == "Great Hall"|| shootRayHit.transform.tag == "Gold Mine"|| shootRayHit.transform.tag == "Pig Farm"|| shootRayHit.transform.tag == "Lumber Mill")
                            {
                                Debug.Log("Hitting a building");
                                if (shootRayHit.transform.tag == "Gold Mine")
                                {
                                    UiController.Instance.uiMode = 1;
                                }
								else if (shootRayHit.transform.tag == "Barracks"|| shootRayHit.transform.tag == "Great Hall"|| shootRayHit.transform.tag == "Pig Farm"|| shootRayHit.transform.tag == "Lumber Mill")
                                {
                                    // If the barracks is being built, send the worker to build the barracks and allow it to move (chosen means it is the builder peon, so it has to move to the building while other can move other places)
                                    if (buildPlace != Vector3.zero)
                                    {
                                        SpencersnavAgent unitMove = builderUnit.GetComponent<SpencersnavAgent>();
                                        unitMove.chosen = true;
                                    }
                                    // Checks the status of the builidng, if it is being built or creating units, it needs special UI

									BuildingMovement checkIfBuilt = previousObject.GetComponent<BuildingMovement>();
                                    if (checkIfBuilt.canCreate)
                                    {
                                        UiController.Instance.uiMode = 1; // uiMode 1 is the regular building ui mode
                                    }
                                    if (!checkIfBuilt.canCreate)
                                    {
                                        UiController.Instance.uiMode = 0.5f; //uiMode 0.5 is the "building being built" uiMode
                                    }
                                    if (checkIfBuilt.makingUnit)
                                    {
                                        UiController.Instance.uiMode = 1.5f; //uiMode 1.5 is the "creating unit" uiMode
                                    }
									if (checkIfBuilt.canCreate&&previousObject.tag == "Pig Farm" ||checkIfBuilt.canCreate&& previousObject.tag == "Lumber Mill") {
										Debug.Log ("Switching Modes in Clicking UI");
										UiController.Instance.uiMode = 3;//uimode 3 is for just displaying the name and icon, which is all we need for the pig farm and lumber mill
									}
                                }
                            }
							if (shootRayHit.transform.tag == "Peon"|| shootRayHit.transform.tag == "Grunt"||  previousObject.tag == "Troll"||  previousObject.tag == "Zul'Jin")//If you are clicking on a peon or a grunt, go into uiMode 2
                            {

                                UiController.Instance.uiMode = 2;//uiMode 2 is the moveable unit mode
                            }
                        }
                    }

                }

            }
        }
       
    }
}
