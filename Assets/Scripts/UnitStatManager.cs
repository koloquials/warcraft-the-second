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

	private bool isDieCoroutineRunning = false;


	public Color normalColor;
	public Color hurtColor = Color.red;

	//Audio Stuff
	private AudioSource[] soundClips;
	public AudioSource hurtSound; // Assign in inspector
	public AudioSource dieSound; // Assign in inspector
	public AudioClip plsDieSound;


	void Start() {

		// Storing the original color
		normalColor = GetComponent<Renderer> ().material.color;

		soundClips = GetComponents<AudioSource> ();

		// Assigning the audio clips into the array
		hurtSound = soundClips [0];
		dieSound = soundClips [1];
	}


	void Update () {
		if (Input.GetKeyDown (KeyCode.Tab)) {
			Debug.Log ("[UNIT STATS]\n" + name + "\nLevel: " + level + "\n" + healthCurrent + "/" + healthTotal + " HP\nArmor: " + armor + "\nDamage: " + damageMin + "-" + damageMax + "\nRange: " + range + "\nSight: " + sight + "\nSpeed: " + speed);
		}
			
		// Got hurt? Turn red!
		if (gotHurt) {
			StartCoroutine (HurtFeedback ());
		} 

		// Die if killed.
		if (healthCurrent < 0) {
			
			// Fall down if killed
			transform.Rotate (Vector3.forward * 90f);

			StartCoroutine (DieWhenKilled ());
//			Destroy (this.gameObject);
//			Debug.Log (this.gameObject + " died!");
		}
		
	}

	// Coroutine for damage feedback
	IEnumerator HurtFeedback() {

		// Don't run if already running.
		if (isHurtCoroutineRunning) {
			yield break;
		}

		isHurtCoroutineRunning = true;

		hurtSound.Play ();

		gameObject.GetComponent<Renderer> ().material.color = hurtColor;
		//Debug.Log (this.gameObject + " turned red!");

		yield return new WaitForSeconds (.1f);

		gameObject.GetComponent<Renderer> ().material.color = normalColor;
		//Debug.Log (this.gameObject + " turned normal color!");

		gotHurt = false;
		isHurtCoroutineRunning = false;
	}

	// Coroutine for dying
	IEnumerator DieWhenKilled() {

		// Don't run if already running.
		if (isDieCoroutineRunning) {
			yield break;
		}
			
		isDieCoroutineRunning = true;

		// Play another die sound if you're an enemy unit (ALSO DOESN'T WORK)
		if (gameObject.tag == "Enemy") {
			dieSound.PlayOneShot (plsDieSound, 1f);
			//Debug.Log (this.gameObject + " played dying sound!");
		}
			

		// Play die sound (NOT WORKING IF AN ENEMY FOR SOME REASON)
		dieSound.Play ();
		//Debug.Log (this.gameObject + " played dying sound!");


		yield return new WaitForSeconds (.5f);


		Destroy (this.gameObject);
		//Debug.Log (this.gameObject + " died!");

		isDieCoroutineRunning = false;
	}

}
