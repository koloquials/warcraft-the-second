using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour {
    public static ResourceManager Instance = new ResourceManager();
	
	public int gold;
	public int wood;
	public int oil;
	public int maxFood;
    public int goldPerDrop = 100;
    public int woodPerDrop = 100;
	public int currentFood=0;
	public GameObject[] currentPeons;
	public GameObject[] currentGrunts;
	public GameObject[] currentTrolls;



	// Use this for initialization
	void Start () {
        Instance = this;
		gold = 3500;
		wood = 700;
		oil = 0;
		maxFood = 5;


	}
	
	// Update is called once per frame
	void Update () {
		currentPeons = GameObject.FindGameObjectsWithTag ("Peon");
		currentGrunts=GameObject.FindGameObjectsWithTag ("Troll");
		currentTrolls=GameObject.FindGameObjectsWithTag ("Grunt");
		//timerCount += Time.deltaTime;
		currentFood=currentPeons.Length+currentGrunts.Length+currentTrolls.Length;//The current food is equal to all the units
		
	}

	public void AddGold (){

		gold += goldPerDrop;

	}

	public void AddWood (){

		wood += woodPerDrop;

	}
}
