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
    public GameObject building;
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
                if (placementRayHit.transform.name == "Ground")
                {
                    placement = placementRayHit.point;
                }



            }
            if (Input.GetMouseButtonDown(0))
            {
                Ray shootRay = Camera.main.ScreenPointToRay(Input.mousePosition);

                float maxRayDist = 10000f;

                Debug.DrawRay(shootRay.origin, shootRay.direction, Color.yellow);


		// TODO: what is this raycast doing? why? write comments
                RaycastHit shootRayHit = new RaycastHit();

                if (Physics.Raycast(shootRay, out shootRayHit, maxRayDist))
                {
		// TODO: what is this doing? write comments
                    if (shootRayHit.transform.gameObject != previousObject)
                    {
			// TODO: what is this doing? write comments
                        if (previousObject != null && previousObject.tag == "Unit")
                        {
                            SpencersnavAgent unitMove = previousObject.GetComponent<SpencersnavAgent>();
                            unitMove.canMove = false;
                        }
                    }
		// TODO: what is this doing? write comments
                    if (shootRayHit.transform.childCount > 0)
                    {
                        wireframe = shootRayHit.transform.GetChild(0).GetComponentInChildren<MeshRenderer>();
                    }
                    if (shootRayHit.transform.childCount == 0)
                    {
                        wireframe = shootRayHit.transform.GetComponent<MeshRenderer>();
                    }
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
		// TODO: what is this doing? write comments
                    mostRecent = wireframe;
                    previousObject = shootRayHit.transform.gameObject;
                    if (shootRayHit.transform.childCount > 0)
                    {
                        if (shootRayHit.transform.GetChild(0).name == "Wireframe")
                        {
                            wireframe.enabled = true;
                            if (shootRayHit.transform.tag == "Building")
                            {
				// TODO: what is this doing? write comments
                                if (buildPlace != Vector3.zero)
                                {
                                    SpencersnavAgent unitMove = builderUnit.GetComponent<SpencersnavAgent>();
                                    unitMove.chosen = true;
                                }
				// TODO: what is this doing? write comments
                                BuildingMovement checkIfBuilt = previousObject.GetComponent<BuildingMovement>();
                                if (checkIfBuilt.canCreate)
                                {
                                    UiController.Instance.uiMode = 1; // TODO: what does "uiMode = 1" mean?
                                }
                                if (!checkIfBuilt.canCreate)
                                {
                                    UiController.Instance.uiMode = 0.5f; // TODO: what does "uiMode = 0.5f" mean?
                                }
                            }
                            if (shootRayHit.transform.tag == "Unit")
                            {

                                UiController.Instance.uiMode = 2;
                            }
                        }
                    }

                }

            }
        }
       
    }
}
