using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FetchingLoopGold : MonoBehaviour { // Peon Fetching Loop for Gold Mine

	// Peon walks to Gold Mine
	// Peon disappears for 2 seconds
	// Peon reappears carrying 100 gold
	// Peon walks to Town Hall
	// Peon deposits 100 gold in resources and disappears for 2 seconds
	// Peon reappears holding nothing
	// Repeat 


	UnityEngine.AI.NavMeshAgent agent;

	public bool FetchGold; // Are they assigned to fetching Gold?
	public bool hasGold; // Are they carrying Gold?
	public bool inGoldMine = false; // Are they in a Gold Mine?
	public bool inTownHall = false; // Are they in the Town Hall?
	public bool inBuilding = false; // Are they in a Building?

	public Renderer rend;

	// Use this for initialization
	void Start () {
		agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		rend = GetComponent<Renderer>();

		GameObject ResourceManagerObject = GameObject.Find ("Resource Manager");
		ResourceManager resourceScript = ResourceManagerObject.GetComponent<ResourceManager>();
	}
	
	// Update is called once per frame
	void Update () {

		if (FetchGold) { // Do stuff if assigned to fetch Gold
			
			if (hasGold == false) {
				//agent.SetDestination (previousObject.transform.position); // Go to selected gold mine (HELP)
			}

			if (hasGold == true) {
				//agent.SetDestination (TownHall.transform.position); // Go to Town Hall (HELP)
			}

		}
	}

	private void OnTriggerEnter(Collider other) { // When the unit enters the building

		if (other.tag == "GoldMine") {
			
			bool inBuilding = true;
			bool inGoldMine = true;

			StartCoroutine ( GoldFetchingCoroutine() );
		}

		if (other.tag == "TownHall") {
			bool inBuilding = true;
			bool inTownHall = true;

			StartCoroutine ( GoldFetchingCoroutine() );
		}

		if (other.tag == "TownHall" && hasGold) { // Add fetched gold to resources
			GameObject ResourceManagerObject = GameObject.Find ("Resource Manager");
			ResourceManager resourceScript = ResourceManagerObject.GetComponent<ResourceManager>();
			resourceScript.gold += 100;
		}
	}

	private void OnTriggerExit(Collider other) { // When the unit exits a building
		
		if (other.tag == "GoldMine") {

			bool inBuilding = false;
			bool inGoldMine = false;
			bool hasGold = true;
		}

		if (other.tag == "TownHall") {
			bool inBuilding = false;
			bool inTownHall = false;
			bool hasGold = false;
		}
			
	}

	IEnumerator GoldFetchingCoroutine(){ 
		Debug.Log ("GoldFetchingCoroutine started!");

		float t = 0f;

		while ( inBuilding && t < 2f) { // Make Unit disappear for 2 seconds while in building
			t += Time.deltaTime;
			rend.enabled = false;
			yield return 0;
		}

		if (inGoldMine) {
			//agent.SetDestination (TownHall.transform.position); // Go to Town Hall (HELP)
		}

		if (inTownHall) {
			//agent.SetDestination (previousObject.transform.position); // Go to selected Gold Mine (HELP)
		}

		rend.enabled = true;
		Debug.Log ("GoldFetchingCoroutine ended!");
	}
}
