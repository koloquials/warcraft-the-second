using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAI : MonoBehaviour { // Once player units move within radius, move to unit. 
											// Once close do damage in timed intervals.
											// Only follow for a certain amount of time

	// combat: ( range[min damage, max damage] - opponent armor) + piercing damage = maxPosDamage done
	// maxPosDamage * range(.5 , 1) = DAMAGE DONE

	// TODO: Stop following after certain amount of time. (Currently fights to the death.)

	UnityEngine.AI.NavMeshAgent agent;

	private float damageDealt;

	public bool canAttack = false;
	private bool isCombatCoroutineRunning = false;

	// Use this for initialization
	void Start () {
		
		agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

	}
	
	// Update is called once per frame
	void Update () {
		
		UnitStatManager statManager = GetComponent<UnitStatManager>();
		SphereCollider visionRadius = GetComponent<SphereCollider>();

		// Size of detection collider based on sight stat. (Tweak based on feel?)
		visionRadius.radius = statManager.sight * 1.5f;

		// Die if killed.
		if (statManager.healthCurrent < 0) {
			Destroy (this.gameObject);
		}

	}

	void OnTriggerStay (Collider other){ // Detect unit within radius, navigate to unit, if close enough do damage.
		
		//Debug.Log("Spotted by enemy!");

		UnitStatManager statManager = GetComponent<UnitStatManager>();
		UnitStatManager otherStatManager = other.GetComponent<UnitStatManager> ();

		// Outside of your range? Move within your range!
		if ( (Mathf.Abs (other.transform.position.x - transform.position.x)) > statManager.range
			|| (Mathf.Abs (other.transform.position.y - transform.position.y)) > statManager.range
			|| (Mathf.Abs (other.transform.position.z - transform.position.z)) > statManager.range) { 

			//Debug.Log ("Enemy incoming!");
			agent.SetDestination (other.transform.position);

		} else { // Stop once within your range.
			
			//Debug.Log ("Enemy halted!");
			agent.SetDestination (transform.position);

		}

		// Do damage when within range.
		if ((Mathf.Abs (other.transform.position.x - transform.position.x)) < statManager.range
			&& (Mathf.Abs (other.transform.position.y - transform.position.y)) < statManager.range
			&& (Mathf.Abs (other.transform.position.z - transform.position.z)) < statManager.range) {

			if (canAttack) {

				// Combat equation.
				damageDealt = ( Random.Range(statManager.damageMin, statManager.damageMax) - otherStatManager.armor ) 
					+ statManager.pierceDamage;

				// Rounds to smallest integer greater or equal to damageDealt. (Tweak base on feel?)
				damageDealt = Mathf.Ceil( damageDealt * Random.Range (.5f, 1f) );

				// Deal damage.
				if (damageDealt > 0) {
					otherStatManager.healthCurrent = otherStatManager.healthCurrent - damageDealt;
				}

				Debug.Log (this.gameObject + " dealt " + damageDealt + " damage to " + other);
			}

			// Sets attack rate.
			StartCoroutine (CombatRefractory ());

				
		}
			

	}

	// Coroutine for attack rate.
	IEnumerator CombatRefractory(){

		// Don't run if already running.
		if (isCombatCoroutineRunning) {
			yield break;
		}

		//Debug.Log ("Running coroutine");
		isCombatCoroutineRunning = true;

		canAttack = false;
		yield return new WaitForSeconds (.5f); // Define attack rate. (Tweak base on feel?)
		canAttack = true;

		isCombatCoroutineRunning = false;
		//Debug.Log ("Ending coroutine");
	}
		

}
