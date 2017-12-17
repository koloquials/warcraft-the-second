using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpencersnavAgent : MonoBehaviour
{

	//Player controlled unit moves to where mouse is clicked
	
	public UnityEngine.AI.NavMeshAgent agent;
    public bool chosen=false;//If they are chosen to build a building
    public bool canMove;//If they can move
    public bool moveOverride;//Overrides thier ability to move, because the clickingUI script can get a unit to move when it shouldnt be able to
    Rigidbody unitRigidbody;
    Renderer unitRenderer;
    MeshRenderer wireframe;
    bool placedOutside = false;
    public Vector3 moveToSpot;
    public int currentlyCarrying=0;
    public int carryingCapacity;
    public bool inResourceLoop;
    public string resource;
    public bool inCoroutine;
    public RaycastHit hit;
    public Collider unitCollider;
	public bool sentRecently;
    public bool returningToResource=false;
    public GameObject treeChopping,goldMine,closestHall;
	 AudioSource thisThingAudio;
	public AudioClip[] clickingSound;
    GameObject[] townHalls;
    private void Awake()
    {
		GameObject findSoundGuy = GameObject.FindGameObjectWithTag ("Sound Guy");
		thisThingAudio = findSoundGuy.GetComponent<AudioSource> ();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		UnitStatManager statManager = GetComponent<UnitStatManager>();
		Transform visionCircle =this.transform.GetChild(1);
		SphereCollider sightRadius = visionCircle.GetComponent<SphereCollider>();

		// Size of detection collider based on sight stat. (Tweak based on feel?)
		sightRadius.radius = statManager.sight * 0.05f;


		unitCollider = this.gameObject.GetComponent<BoxCollider>();
		carryingCapacity = 2;
		wireframe = this.gameObject.transform.GetChild(0).GetComponent<MeshRenderer>();
		unitRenderer = GetComponent<MeshRenderer>();
		unitRigidbody = this.GetComponent<Rigidbody>();

		canMove = false;
    }
    void Start ()
	{
       
	}

	void Update () 
	{
       // Debug.Log(carryingCapacity);
        unitRigidbody.velocity = Vector3.zero;
        unitRigidbody.angularVelocity = Vector3.zero;

        if (ClickingUI.Instance.buildPlace == Vector3.zero||!chosen)//If there is not a building for them to build, they can move normally
        {
            if (Input.GetMouseButtonDown(1))//If the player clicks
            {
                if (canMove&&!moveOverride)//If they can move and are not overidden
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    

                    if (Physics.Raycast(ray, out hit, 100))
                    {
						if (this.tag == "Grunt") {
							//thisThingAudio.clip = clickingSound [Random.Range (0, clickingSound.Length)];
							thisThingAudio.PlayOneShot(clickingSound [Random.Range (0, clickingSound.Length)]);
						}
						if (this.tag == "Troll"||this.tag=="Zul'Jin") {
							//thisThingAudio.clip = clickingSound [Random.Range (0, clickingSound.Length)];
							thisThingAudio.PlayOneShot(clickingSound [Random.Range (0, clickingSound.Length)]);
						}
                        if (this.tag == "Peon")
                        {
							//thisThingAudio.clip = clickingSound [Random.Range (0, clickingSound.Length)];
							thisThingAudio.PlayOneShot(clickingSound [Random.Range (0, clickingSound.Length)]);
                            if (hit.transform.gameObject.tag == "Gold Mine")//4 seconds
                            {
                                carryingCapacity = 2;
                                resource = "Gold";

                                inResourceLoop = true;
                                //   Debug.Log("Going to Get Gold");
                                GoldLoop();
                            }

                            if (hit.transform.gameObject.tag == "Tree")//34 seconds to chop down a tree
                            {
                                carryingCapacity = 33;
                                resource = "Wood";
								sentRecently = true;
                                Debug.Log("Going to Get trees");
                                inResourceLoop = true;
                                GoldLoop();
                            }

                        }

						if (hit.transform.gameObject.tag == "Enemy") { 
							agent.SetDestination (hit.transform.gameObject.transform.position); 
							//Debug.Log ("Going to " + hit.transform.gameObject); 
						} 

						if(hit.transform.gameObject.tag != "Tree"&& hit.transform.gameObject.tag != "Gold Mine" && hit.transform.gameObject.tag != "Enemy")
                        {
                         //   Debug.Log("Get Away from resource");
                            inResourceLoop = false;
                            returningToResource = false;
							resource = "Nothing";
                            agent.SetDestination(hit.point);//Move to where the player clicks, pathfinding around obstacles
							//Debug.Log( "Going to" + hit.point);
                        }
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
		if (other.tag == "Barracks"||other.tag == "Pig Farm"||other.tag == "Lumber Mill")
        {

            BuildingMovement shouldBuild = other.GetComponent<BuildingMovement>();
          //  shouldBuild.shouldBuild=true;//Starts it building
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
				unitRenderer.enabled = true;
                Vector2 unitPlacement2D = Random.insideUnitCircle.normalized;
                Vector3 unitPlacement3D = new Vector3(unitPlacement2D.x, 0.0f, unitPlacement2D.y) * 4f;
                this.transform.position =  other.transform.position + unitPlacement3D;
                placedOutside = true;
                
            }
        }
        if(other.tag=="Victory Circle")
        {
            if (this.tag == "Zul'Jin")
            {
                VictoryController.Instance.inLocation = true;
            }
        }
        if(other.tag=="Gold Mine"&&resource=="Gold")
        {
            if (inResourceLoop)
            {
                canMove = false;
                //  moveOverride = true;

                StartCoroutine(GetResources());
            }
        }
        if (other.tag == "Tree" && resource == "Wood")
        {
            if (inResourceLoop)
            {
                // Debug.Log("In Resource Loop");
                if (currentlyCarrying == 0)
                {
                    this.agent.SetDestination(this.transform.position);
                }
                treeChopping = other.gameObject;
                carryingCapacity = 33;
               // canMove = false;
                //  moveOverride = true;

                StartCoroutine(GetResources());
            }
        }
		if(other.tag=="Great Hall"||other.tag=="Lumber Mill")
        {
			Debug.Log ("Smacking Great Hall");
            if (currentlyCarrying > 0)
            {
				Debug.Log ("Hitting the Great Hall");
                unitCollider.enabled = false;
                wireframe.enabled = false;
               //unitRenderer.enabled = false;
                canMove = false;
                //moveOverride = true;
    
                StartCoroutine(DroppingResources());
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Gold Mine")
        {
           // UiController.Instance.uiMode = 0;
        }
		if(other.tag=="Great Hall")
		{
			Debug.Log ("Smacking Great Hall");
			if (currentlyCarrying > 0)
			{
				Debug.Log ("Hitting the Great Hall");
				unitCollider.enabled = false;
				wireframe.enabled = false;
				//unitRenderer.enabled = false;
				canMove = false;
				//moveOverride = true;

				StartCoroutine(DroppingResources());
			}
		}
    }
    public void GoldLoop()
    {
		
        if ( hit.collider != null && hit.collider.gameObject != null)
        {
             goldMine = hit.transform.gameObject;
             closestHall = null;
            townHalls = GameObject.FindGameObjectsWithTag("Great Hall");
            foreach (GameObject hall in townHalls)
            {
                if (hall != null)
                {
                    if (closestHall == null)
                    {
                        closestHall = hall;
                    }
                    if ((goldMine.transform.position - hall.transform.position).magnitude < (goldMine.transform.position - closestHall.transform.position).magnitude)
                    {
                        closestHall = hall;
                    }
                }
            }
        }
		if (resource == "Wood") {
			GameObject[] lumberMills = GameObject.FindGameObjectsWithTag ("Lumber Mill");
			foreach (GameObject mill in lumberMills) {
				if (mill != null) {

					if ((goldMine.transform.position - mill.transform.position).magnitude < (goldMine.transform.position - closestHall.transform.position).magnitude) {
						closestHall = mill;
					}
				}
			}
		}
        if (inResourceLoop)
        {
            if (currentlyCarrying == 0)
            {
                if (resource == "Wood")
                {
					if (!sentRecently)
                    {
                          Debug.Log("Searching for trees");
                        GameObject[] forest;
                        GameObject closestTree = null;
                        forest = GameObject.FindGameObjectsWithTag("Tree");
                        foreach (GameObject tree in forest)
                        {
                            if (tree != null)
                            {
                                if (closestTree == null)
                                {
                                    closestTree = tree;
                                }
                                if ((closestHall.transform.position - tree.transform.position).magnitude < (closestHall.transform.position - closestTree.transform.position).magnitude)
                                {
                                    closestTree = tree;
                                }
                            }
                        }
                        treeChopping = closestTree;
                        agent.SetDestination(closestTree.transform.position);
					Debug.Log (closestTree);
					Debug.Log ("Going to chop tree");
                    }
					if (sentRecently) {
						agent.SetDestination (hit.transform.position);
						if (hit.transform.tag == "Tree") {
							treeChopping = hit.transform.gameObject;
						}
					}
					
                }
                else
                {
                    agent.SetDestination(goldMine.transform.position);
                }

            }
            if (currentlyCarrying >= carryingCapacity)
            {
                Debug.Log("Going home");
                unitRenderer.enabled = true;
                canMove = true;
                moveOverride = false;
                agent.SetDestination(closestHall.transform.position);
            }
        }



    }
     IEnumerator GetResources()
    {
        if (inCoroutine)
        {
            yield break;
        }
        inCoroutine = true;
       // returningToResource = false;
		sentRecently=false;
        Debug.Log("Getting Resources");
        for (int collectTime = 0; collectTime < carryingCapacity; collectTime++)
        {
            if (!inResourceLoop)
            {
                if (currentlyCarrying != carryingCapacity)
                {
                   // Debug.Log("Resetting");
                    currentlyCarrying = 0;
                }
                inCoroutine = false;
                yield break;
            }
            if (this.resource == "Gold")
            {
                unitCollider.enabled = false;
                moveOverride = true;
               // unitRenderer.enabled = false;
                wireframe.enabled = false;
            }
            if (currentlyCarrying >= carryingCapacity)
            {
               // unitCollider.enabled = false;
				unitCollider.enabled = true;
                unitRenderer.enabled = true;
                inCoroutine = false;
                moveOverride = false;
                if (resource == "Wood")
                {
                    Debug.Log("Destroying Tree");
                    Destroy(treeChopping.gameObject);
                }
                yield break;
            }
            currentlyCarrying++;
			yield return new WaitForSecondsRealtime(1);
        }
		unitRenderer.enabled = true;
		unitCollider.enabled = true;
        inCoroutine = false;

        GoldLoop();
        yield return 0;
    }
    IEnumerator DroppingResources()
    {
        Debug.Log("Dropping it Off");
        if (inCoroutine)
        {
          //  Debug.Log("BREAK");
            yield break;
        }
       // Debug.Log("Dropping it Off");
        inCoroutine = true;
        currentlyCarrying = 2;
        carryingCapacity = 2;
        for (int dropTime = currentlyCarrying; dropTime > 0; dropTime--)
        {
            moveOverride = true;
            currentlyCarrying -=1;
			yield return new WaitForSeconds((1));
            if (currentlyCarrying == 0)
               
            {
                moveOverride = false;
                if (resource == "Gold")
                {
                    ResourceManager.Instance.AddGold();
                }
                if (resource == "Wood")
                {
                    ResourceManager.Instance.AddWood();
                }
            }
        }
        returningToResource = true;
        unitCollider.enabled = false;
        unitRenderer.enabled = true;
        GoldLoop();
        inCoroutine = false;
        yield return 0;
    }
}
