using UnityEngine;
using System.Collections;


/*
 * 
 * Goal: The radar will detect the player,
 * and make the turret rotate towards target
 * while shooting at the player's driver
 * 
 */

public class Turret_radar_target : MonoBehaviour {


	private Turret_target turret;
	private CircleCollider2D radar;

	// Use this for initialization
	void Start () {
		turret = GetComponentInParent<Turret_target> ();
		radar = gameObject.GetComponent<CircleCollider2D>(); // there should be only one circle collider
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// What if we don't need an enter?
	void onTriggerEnter2D(Collider2D col){
		if (col.CompareTag("Driver")){
			// If the driver is already in range, do not replace target
			// else if there is no driver in radar, add in the driver.

			if (turret.getDriver() == null){
				turret.setDriver(col.gameObject.GetComponent<player_driver>());
			}
		}

	}

	void onTriggerExit2D(Collider2D col){
		turret.setDriver(null);

	}

	void OnTriggerStay2D(Collider2D col){
		if (col.CompareTag("Driver")){

			if (turret.getDriver() == null){
				turret.setDriver(col.gameObject.GetComponent<player_driver>());
			}

			turret.rotateTowardsPlayer();
			turret.fireBullet();

			float dist = Vector2.Distance(gameObject.transform.position, turret.getDriver().gameObject.transform.position);

			if (dist > (radar.radius * 2) || dist < (radar.radius * -2)){
				turret.setDriver(null);
			}
		}

		// Now check if the target driver is outside
	}
}
