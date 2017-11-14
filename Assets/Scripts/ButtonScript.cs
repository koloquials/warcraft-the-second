using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler{
   // public Button button;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
    }

    public void OnPointerEnter(PointerEventData eventData)//Checks whether the mouse is hovering over the button
    {
        
        ClickingUI.Instance.isClickingButton = true;//If they are hovering over it, the ui is not allowed to switch and they can click through the button
    }
    public void OnPointerExit(PointerEventData eventData)//Checks whether the mouse is no longer hovering over the button
    {
       
        ClickingUI.Instance.isClickingButton = false;

    }



   public void CreateUnit()
    {
        UiController.Instance.CreateUnit(ClickingUI.Instance.previousObject);
    
    }

    public void CreateBuilding()
    {
        UiController.Instance.CreateBuilding();
    }


}
