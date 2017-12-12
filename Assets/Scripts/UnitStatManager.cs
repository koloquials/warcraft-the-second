using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStatManager : MonoBehaviour { // ALL STATS MUST BE ASSIGNED IN INSPECTOR OR BY SCRIPT FOR EACH UNIT

	public string name; // unit's name. ie: Peon.
	public int level; // units level. (currently unsure exactly what this does?)

	public float healthCurrent; //current health. [THIS]/total
	public float healthTotal; // total health. current/[THIS]

	public float armor; // armor stat. (likely for calculating damage.)
	public float damageMin; // [THIS] - max. dmg operates on a scale. (i assume dmg is picked based on this scale and compared to target's armor?)
	public float damageMax; // min - [THIS].
	public float pierceDamage; // Damage that ignores armor. (True damage)
	public float range; // range. (likely affects how close they must be to attack/maybe harvest too)
	public float sight; // sight. (Affects detection radius [as well as fog of war?])
	public float speed; // speed. (likely walk speed.)

	public bool gotHurt = false;
	private bool isHurtCoroutineRunning = false;

	public Color normalColor;
	public Color hurtColor = Color.red;

	void Start() {

		// storing the original color
		normalColor = GetComponent<Renderer> ().material.color;

	}


	void Update () {
		if (Input.GetKeyDown (KeyCode.Tab)) {
			Debug.Log ("[UNIT STATS]\n" + name + "\nLevel: " + level + "\n" + healthCurrent + "/" + healthTotal + " HP\nArmor: " + armor + "\nDamage: " + damageMin + "-" + damageMax + "\nRange: " + range + "\nSight: " + sight + "\nSpeed: " + speed);
		}
			
		// Got hurt? Turn red!
		if (gotHurt) {
			StartCoroutine (HurtFeedback());
		} 

		// Die if killed.
		if (healthCurrent < 0) {
			Destroy (this.gameObject);
			Debug.Log (this.gameObject + " died!");
		}
		
	}

	//Coroutine for damage feedback
	IEnumerator HurtFeedback() {

		// Don't run if already running.
		if (isHurtCoroutineRunning) {
			yield break;
		}

		isHurtCoroutineRunning = true;
		
		gameObject.GetComponent<Renderer> ().material.color = hurtColor;
		//Debug.Log (this.gameObject + " turned red!");

		yield return new WaitForSeconds (.1f);

		gameObject.GetComponent<Renderer> ().material.color = normalColor;
		//Debug.Log (this.gameObject + " turned normal color!");

		gotHurt = false;
		isHurtCoroutineRunning = false;
	}

}
