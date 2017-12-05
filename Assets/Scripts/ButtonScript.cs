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
        Transform hoveredButton = this.transform.GetChild(0);
        Text buttonText = hoveredButton.GetComponent<Text>();
        ClickingUI.Instance.isClickingButton = true;//If they are hovering over it, the ui is not allowed to switch and they can click through the button
       if(buttonText.text=="Create Grunt")
        {
            Debug.Log("Cost: 800 Gold");
        }
        if (buttonText.text == "Create Peon")
        {
            Debug.Log("Cost: 400 Gold");
        }
        if (buttonText.text == "Build Barracks")
        {
            Debug.Log("Cost: 800 Gold, 450 Lumber");
        }
    }
    public void OnPointerExit(PointerEventData eventData)//Checks whether the mouse is no longer hovering over the button
    {
       
        ClickingUI.Instance.isClickingButton = false;

    }



   public void CreateUnit()
    {
		Transform hoveredButton = this.transform.GetChild(0);
		Text buttonText = hoveredButton.GetComponent<Text>();
		if (buttonText.text=="Create Grunt")
        {
            UiController.Instance.CreateUnit(ClickingUI.Instance.previousObject, "Grunt");
        }
		if (buttonText.text=="Create Peon")
        {
            UiController.Instance.CreateUnit(ClickingUI.Instance.previousObject, "Peon");
        }
		if (buttonText.text=="Create Troll Axethrower")
		{
			UiController.Instance.CreateUnit(ClickingUI.Instance.previousObject, "Troll");
		}

    }
    public void CreatePeon()
    {
        UiController.Instance.CreateUnit(ClickingUI.Instance.previousObject, "Peon");

    }

    public void CreateBuilding()
    {
        UiController.Instance.CreateBuilding();
    }


}
