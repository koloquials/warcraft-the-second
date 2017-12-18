using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySightScript : MonoBehaviour {
	public Transform actualEnemy;
	public EnemyAI parentAI;
	// Use this for initialization
	void Start () {//This script is for enemies to spot the player units. It doesnt block the movement raycasts but still detects units
		actualEnemy=this.transform.parent;
		parentAI=actualEnemy.GetComponent<EnemyAI>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter(Collider other){
		if (other.tag != "Tree") {
			parentAI.shouldAggro = true;//
			parentAI.other = other.gameObject;
		}


	}
}
