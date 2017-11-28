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

	void Update () {
		if (Input.GetKeyDown (KeyCode.Tab)) {
			Debug.Log ("[UNIT STATS]\n" + name + "\nLevel: " + level + "\n" + healthCurrent + "/" + healthTotal + " HP\nArmor: " + armor + "\nDamage: " + damageMin + "-" + damageMax + "\nRange: " + range + "\nSight: " + sight + "\nSpeed: " + speed);
		}
		
	}
}
