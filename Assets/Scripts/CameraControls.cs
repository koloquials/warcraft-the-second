using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour {
	// Camera restraints : X -75.2 - 68.3
	//					 : Z -105.3 - 18.7


    //public Transform target;
	//public GameObject unit; // Used to move Camera with Unit
	//private Vector3 offset; // Used to move Camera with Unit

	public int arrowCameraSpeed = 20; // Adjust in Inspector
	public int mouseCameraSpeed = 50; // Adjust in Inspector
	public bool moveWithMouse=false;
	private float mousePosX;
	private float mousePosY;

	public float fps; // Frames per second

    //Camera cam;
    //float height = Screen.height;
    //float width = Screen.width;

    void Start()
    {
       
        //cam = GetComponent<Camera>();

		//offset = transform.position - unit.transform.position;
    }

	void Update() {

		fps = 1.0f / Time.deltaTime;
		//Debug.Log ("Current FPS is " + fps );
	}

    void LateUpdate() //LateUpdate is called after all Update functions have been called
    {
        //Debug.Log("Height: " + height + "\n Width: " + width);
        //Vector3 screenPos = cam.WorldToScreenPoint(Input.mousePosition);
        //Debug.Log("ScreenPos X: " + screenPos.x + "\n ScreenPos Y: "+ screenPos.x+ "    ScreenPos Z: " + screenPos.z);

		//Move camera position with unit position
		//transform.position = unit.transform.position + offset; 

		//Move Camera with Arrow Keys (Direct Movement) [instead of inverse]
		if (Input.GetKey (KeyCode.UpArrow) && transform.position.z < 13f) {
			transform.position += Vector3.back * arrowCameraSpeed * Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.DownArrow) && transform.position.z > -92.5f) {
			transform.position += Vector3.forward * arrowCameraSpeed * Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.LeftArrow) && transform.position.x > -78f) {
			transform.position += Vector3.right * arrowCameraSpeed * Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.RightArrow) && transform.position.x < 37f) {
			transform.position += Vector3.left * arrowCameraSpeed * Time.deltaTime;
		}
		if (moveWithMouse) {
			//Move Camera with Mouse
			mousePosX = Input.mousePosition.x / Screen.width;
			//Debug.Log("MousePosX =" + mousePosX);
			mousePosY = Input.mousePosition.y / Screen.height;
			//Debug.Log("MousePosY =" + mousePosY);

			if (mousePosY < 0.08f && transform.position.z > -92.5f) {
				transform.position += Vector3.forward * mouseCameraSpeed * Time.deltaTime;
				//Debug.Log("Move Camera Down");
			}
			if (mousePosY > 0.92f && transform.position.z < 13f) {
				transform.position += Vector3.back * mouseCameraSpeed * Time.deltaTime;
				//Debug.Log("Move Camera Up");
			}
			if (mousePosX < 0.29f && mousePosX > 0.24f && transform.position.x > -78f) {
				transform.position += Vector3.right * mouseCameraSpeed * Time.deltaTime;
				//Debug.Log("Move Camera Left");
			}
			if (mousePosX > 0.92f && transform.position.x < 37f) {
				transform.position += Vector3.left * mouseCameraSpeed * Time.deltaTime;
				//Debug.Log("Move Camera Right");
			}
		}
    } 
}
