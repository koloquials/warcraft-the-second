using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCoroutine : MonoBehaviour {
    bool isCoroutineRunning=false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
          //  Debug.Log("hitting Space");
            StartCoroutine(MoveSphereCoroutine(5f));
        }
	}
    IEnumerator MoveSphereCoroutine(float durationOfSeconds)
    {
        if (isCoroutineRunning)
        {
            yield break;
        }
        Debug.Log("Running");
        isCoroutineRunning = true;
        for(float t=0; t<durationOfSeconds; t += Time.deltaTime)
        {
            transform.Translate(0, 0, Time.deltaTime * 5f);
            yield return 0;
        }
        isCoroutineRunning = false;
    }

}
