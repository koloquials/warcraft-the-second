using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickingUI : MonoBehaviour {
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

        Ray placementRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        float rayDist = 10000f;

        Debug.DrawRay(placementRay.origin, placementRay.direction, Color.yellow);


        RaycastHit placementRayHit = new RaycastHit();

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



                RaycastHit shootRayHit = new RaycastHit();

                if (Physics.Raycast(shootRay, out shootRayHit, maxRayDist))
                {
                    if (shootRayHit.transform.gameObject != previousObject)
                    {
                        if (previousObject != null && previousObject.tag == "Unit")
                        {
                            SpencersnavAgent unitMove = previousObject.GetComponent<SpencersnavAgent>();
                            unitMove.canMove = false;
                        }
                    }
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
                    mostRecent = wireframe;
                    previousObject = shootRayHit.transform.gameObject;
                    if (shootRayHit.transform.childCount > 0)
                    {
                        if (shootRayHit.transform.GetChild(0).name == "Wireframe")
                        {
                            wireframe.enabled = true;
                            if (shootRayHit.transform.tag == "Building")
                            {

                                if (buildPlace != Vector3.zero)
                                {
                                    SpencersnavAgent unitMove = builderUnit.GetComponent<SpencersnavAgent>();
                                    unitMove.chosen = true;
                                }

                                BuildingMovement checkIfBuilt = previousObject.GetComponent<BuildingMovement>();
                                if (checkIfBuilt.canCreate)
                                {
                                    UiController.Instance.uiMode = 1;
                                }
                                if (!checkIfBuilt.canCreate)
                                {
                                    UiController.Instance.uiMode = 0.5f;
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
