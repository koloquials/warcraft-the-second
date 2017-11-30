using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWarScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Kill Me");
        Debug.Log(other.name);
        if (other.gameObject.tag == "Fog Killer")
        {
           
            Destroy(this.gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
     //   Debug.Log("Kill Me");
    }
}
