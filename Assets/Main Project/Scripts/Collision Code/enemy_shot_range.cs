using UnityEngine;
using System.Collections;

/* This collision object will detect players to shoot bullets at the target
 * It is the basic template for any enemy or its weapons that will 
 * 
*/

public class enemy_shot_range : MonoBehaviour {

	private flying_enemy_straight shootingEnemy;

	// Use this for initialization
	void Start () {

		shootingEnemy = gameObject.GetComponentInParent<flying_enemy_straight>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay2D(Collider2D col){
		if (col.CompareTag("Driver")){
			shootingEnemy.fireWeapon();
		}

	}
}
