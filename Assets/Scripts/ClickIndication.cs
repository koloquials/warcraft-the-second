using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickIndication : MonoBehaviour {

	public Transform cursorX;
	private Transform instantiatedCursor;
	bool cursorIsVisible;

//	public GameObject[] trees;

	// Use this for initialization
	void Start () {

//		if (trees == null) {
//			trees = GameObject.FindGameObjectsWithTag ("Tree");
//		}
		
	}
	
	// Update is called once per frame
	void Update () {
		Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

		float maxRayDist = 100f;

		Debug.DrawRay (mouseRay.origin, mouseRay.direction * maxRayDist, Color.green);

		RaycastHit mouseRayHit = new RaycastHit ();

		if (Physics.Raycast (mouseRay, out mouseRayHit, maxRayDist)) {
			cursorX.position = mouseRayHit.point;

			if (Input.GetMouseButtonDown (0)) {
				Transform instantiatedCursor = (Transform) Instantiate (cursorX, mouseRayHit.point, Quaternion.Euler (0f, 0f, 0f));
				cursorIsVisible = true;
			}
			if (Input.GetMouseButtonDown (0) && cursorIsVisible == true) {
				Destroy (instantiatedCursor);
			}
		}
	}

	void OnCollisionEnter (Collision onTree) {
		if (onTree.gameObject.tag == "TreePieces"){
			Debug.Log ("I'm on a Tree");
		}
	}
}
