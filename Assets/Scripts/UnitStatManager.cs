using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStatManager : MonoBehaviour {

	public string name; //unit's name. ie: Peon.
	public int level; //units level. (currently unsure exactly what this does?)

	public int healthCurrent; //current health. [THIS]/total
	public int healthTotal; // total health. current/[THIS]

	public int armor; //armor stat. (likely for calculating damage.)
	public int damageMin; //[THIS] - max. dmg operates on a scale. (i assume dmg is picked based on this scale and compared to target's armor?)
	public int damageMax; //min - [THIS].
	public int range; //range. (likely affects how close they must be to attack/maybe harvest too)
	public int sight; //sight. (likely affects fog of war?)
	public int speed; //speed. (likely walk speed.)

	void Update () {
		if (Input.GetKeyDown (KeyCode.Tab)) {
			Debug.Log ("[UNIT STATS]\n" + name + "\nLevel: " + level + "\n" + healthCurrent + "/" + healthTotal + " HP\nArmor: " + armor + "\nDamage: " + damageMin + "-" + damageMax + "\nRange: " + range + "\nSight: " + sight + "\nSpeed: " + speed);
		}
		
	}
}
