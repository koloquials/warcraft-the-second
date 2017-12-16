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
			UiController.Instance.displayCost.text=("Create Grunt   Cost: 800 Gold");
        }
        if (buttonText.text == "Create Peon")
        {
			UiController.Instance.displayCost.text=("Create Peon   Cost: 400 Gold");
        }
        if (buttonText.text == "Build Barracks")
        {
			UiController.Instance.displayCost.text=("Build Barracks  Cost: 800 Gold, 450 Lumber");
        }
		if (buttonText.text == "Build Pig Farm")
		{
			UiController.Instance.displayCost.text=("Build Pig Farm  Cost: 500 Gold, 250 Lumber");
		}
		if (buttonText.text == "Build Lumber Mill")
		{
			UiController.Instance.displayCost.text=("Build Lumber Mill  Cost: 600 Gold, 450 Lumber");
		}
		if (buttonText.text == "Create Troll Axethrower")
		{
			UiController.Instance.displayCost.text=("Create Troll Axethrower   Cost: 500 Gold, 50 Lumber");
		}
    }
    public void OnPointerExit(PointerEventData eventData)//Checks whether the mouse is no longer hovering over the button
    {
       
        ClickingUI.Instance.isClickingButton = false;
		UiController.Instance.displayCost.text = ("");

    }



   public void CreateUnit()
    {
		Debug.Log (this.transform.childCount);
		Transform hoveredButton = this.transform.GetChild(0);
		Text buttonText = hoveredButton.GetComponent<Text>();
		if (buttonText.text=="Create Grunt")
        {
			UiController.Instance.CreateGenericUnit(ClickingUI.Instance.previousObject, "Grunt");
        }
		if (buttonText.text=="Create Peon")
        {
            UiController.Instance.CreateGenericUnit(ClickingUI.Instance.previousObject, "Peon");
        }
		if (buttonText.text=="Create Troll Axethrower")
		{
			UiController.Instance.CreateGenericUnit(ClickingUI.Instance.previousObject, "Troll");
		}

    }
    public void CreatePeon()
    {
        UiController.Instance.CreateGenericUnit(ClickingUI.Instance.previousObject, "Peon");

    }

    public void CreateBuilding()
    {
		Transform hoveredButton = this.transform.GetChild(0);
		Text buttonText = hoveredButton.GetComponent<Text>();
		if (buttonText.text=="Build Barracks")
		{
			UiController.Instance.CreateBuilding("Barracks");
		}
		if (buttonText.text=="Build Pig Farm")
		{
			UiController.Instance.CreateBuilding("Pig Farm");
		}
		if (buttonText.text=="Build Lumber Mill")
		{
			UiController.Instance.CreateBuilding("Lumber Mill");
		}
        
    }


}
