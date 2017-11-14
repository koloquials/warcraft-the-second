using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpencersnavAgent : MonoBehaviour {

	//Player controlled unit moves to where mouse is clicked
	
	public UnityEngine.AI.NavMeshAgent agent;
    public bool chosen;//If they are chosen to build a building
    public bool canMove;//If they can move
    bool moveOverride;//Overrides thier ability to move, because the clickingUI script can get a unit to move when it shouldnt be able to
    Rigidbody unitRigidbody;
    Renderer unitRenderer;
    bool placedOutside = false;
    public Vector3 moveToSpot;
    private void Awake()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }
    void Start ()
	{
        unitRenderer = GetComponent<MeshRenderer>();
        unitRigidbody = this.GetComponent<Rigidbody>();
        
        canMove = false;
	}

	void Update () 
	{
        unitRigidbody.velocity = Vector3.zero;
        unitRigidbody.angularVelocity = Vector3.zero;
        /* if (this.transform.position == moveToSpot)
         {
             moveToSpot = Vector3.zero;
         }
         
         if (moveToSpot != Vector3.zero)
         {
            agent.SetDestination(moveToSpot);
         }*/
        if (ClickingUI.Instance.buildPlace == Vector3.zero||!chosen)//If there is not a building for them to build, they can move normally
        {
            if (Input.GetMouseButtonDown(1))//If the player clicks
            {
                if (canMove&&!moveOverride)//If they can move and are not overidden
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit, 100))
                    {

                        agent.SetDestination(hit.point);//Move to where the player clicks, pathfinding around obstacles


                    }
                }
            }
        }
        else//If this unit has a building to build
        {
            placedOutside = false;
            agent.SetDestination(ClickingUI.Instance.buildPlace);//Thier destination is the building location
            if (Mathf.Abs(this.transform.position.magnitude- ClickingUI.Instance.buildPlace.magnitude)<1)//If they are within 1 unit of the building reset the overall
            {
                Debug.Log("Close Enough");
                //building settings so other units can build buildings
                ClickingUI.Instance.buildPlace = Vector3.zero;
                chosen = false;
            }
        }
        }
    private void OnTriggerStay(Collider other)//When the unit enters the building, start building
    {
        if (other.tag == "Barracks")
        {
            BuildingMovement shouldBuild = other.GetComponent<BuildingMovement>();
            shouldBuild.shouldBuild=true;//Starts it building
            if (!shouldBuild.canCreate&&shouldBuild.placed)//If it is being build, dont let it move
            {
                unitRenderer.enabled = false;
                canMove = false;
                moveOverride = true;
            }
            if (shouldBuild.canCreate&&!placedOutside)//When its done, allow it to move
            {
                moveOverride = false;
                unitRenderer.enabled = true;
                agent.ResetPath();
                Vector2 unitPlacement2D = Random.insideUnitCircle.normalized;
                Vector3 unitPlacement3D = new Vector3(unitPlacement2D.x, 0.0f, unitPlacement2D.y) * 4f;
                this.transform.position =  other.transform.position + unitPlacement3D;
                placedOutside = true;
                
            }
        }
        if(other.tag=="Victory Circle")
        {
            if (this.tag == "Grunt")
            {
                VictoryController.Instance.inLocation = true;
            }
        }
    }
}
