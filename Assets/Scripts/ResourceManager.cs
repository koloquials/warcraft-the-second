using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour {
    public static ResourceManager Instance = new ResourceManager();
	//it might make sense to make this class static. If the player needs to regularly access its functions/values
	//otherwise looks good
	
	public int gold;
	public int wood;
	public int oil;
	public int maxFood;
    public int goldPerDrop = 100;
    public int woodPerDrop = 100;
	public int foodRate = 1;
	public int currentFood=0;
	public GameObject[] currentPeons;
	public GameObject[] currentGrunts;
	public GameObject[] currentTrolls;

//	public float timerCount = 0f;

	// Use this for initialization
	void Start () {
        Instance = this;
		gold = 3500;
		wood = 700;
		oil = 0;
		maxFood = 5;

	//	InvokeRepeating ("addFood", 5, 5);

	}
	
	// Update is called once per frame
	void Update () {
		currentPeons = GameObject.FindGameObjectsWithTag ("Peon");
		currentGrunts=GameObject.FindGameObjectsWithTag ("Troll");
		currentTrolls=GameObject.FindGameObjectsWithTag ("Grunt");
		//timerCount += Time.deltaTime;
		currentFood=currentPeons.Length+currentGrunts.Length+currentTrolls.Length;
		
	}

	public void AddGold (){

		gold += goldPerDrop;

	}

	public void AddWood (){

		wood += woodPerDrop;

	}

	public void AddOil (){

		oil += 25;

	}

	public void AddUnit (){

		//if (food < totalMen) {
		currentFood++;
		//}
	}

	public void AddFarm (){

		maxFood += 5;

	}


}
