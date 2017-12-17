using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySightScript : MonoBehaviour {
	public Transform actualEnemy;
	public EnemyAI parentAI;
	// Use this for initialization
	void Start () {
		actualEnemy=this.transform.parent;
		parentAI=actualEnemy.GetComponent<EnemyAI>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter(Collider other){
		
		if (other.tag != "Tree") {
			Debug.Log ("OMG IM HITTING SOMETHING");
			parentAI.shouldAggro = true;
			parentAI.other = other.gameObject;
		}


	}
}
