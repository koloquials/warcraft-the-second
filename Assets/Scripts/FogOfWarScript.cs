using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWarScript : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Fog Killer"){Destroy(this.gameObject);}//If is hits a units sight sphere, kill this fog
    }

}
