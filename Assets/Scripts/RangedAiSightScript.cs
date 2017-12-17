using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAiSightScript : MonoBehaviour {
	Transform actualEnemy;
	EnemyRangedAI parentAI;
	// Use this for initialization
	void Start () {
		actualEnemy=this.transform.parent;
		parentAI=actualEnemy.GetComponent<EnemyRangedAI>();
	}

	// Update is called once per frame
	void Update () {

	}
	void OnTriggerEnter(Collider other){
		if (other.tag != "Tree") {
			parentAI.shouldAggro = true;
			parentAI.other = other.gameObject;
		}


	}
}
