using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class navAgent : MonoBehaviour {

	//Player controlled unit moves to where mouse is clicked
	
	UnityEngine.AI.NavMeshAgent agent;

	void Start ()
	{
		agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
	}

	void Update () 
	{
		if(Input.GetMouseButtonDown (0))
		{
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;

			if(Physics.Raycast(ray, out hit, 100))
			{
				agent.SetDestination (hit.point);
			}
		}
	}
}
