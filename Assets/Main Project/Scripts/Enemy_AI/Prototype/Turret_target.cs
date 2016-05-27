using UnityEngine;
using System.Collections;

/* Stationary enemies like the Turret will shoot the bullet with the given velocity
 * because trying to shoot bullet that keeps track of the player's velocity
 * will make it seem very awkward



 * This turret will rotate the cannon to target the closest player
 * When player in range, the turret will fire bullets continously.
 */

/*
 * For this demo, we have a turret with the following
 * Turret base is parent
 * Turret head is child (child (0))
 * Turret cannon is grandchild (child(0) . child(0))
 * 
 */

public class Turret_target : MonoBehaviour {

	
	// for inspector
	public CircleCollider2D radar;
	public float bullet_delay;
	public float rotate_speed;
	public GameObject bullet;
	public float bullet_speed;
	public float bullet_spawn_time;

	// for internal use
	private Transform cannon;
	private Transform shot_spawn;
	private float bullet_delay_timer;
	private GameObject bullet_clone;
	private player_driver driver;

	// Use this for initialization
	void Start () {
		cannon = transform.GetChild(0).GetChild(0);
		shot_spawn = cannon.GetChild(0);
		bullet_delay_timer = bullet_delay;
	}
	
	// Update is called once per frame
	void Update () {

	}

	// FixedUpdate is called once per physics step
	void FixedUpdate(){


	}


	
	// if player is inside the radar,
	// take its coordinates and rotate the cannon towards target

	// Never mind.. I now know it works. I had to make the radius bigger
	public void rotateTowardsPlayer(){
		Vector3 targetDir = driver.gameObject.transform.position - cannon.position;
		float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
		cannon.rotation = Quaternion.RotateTowards (cannon.rotation, Quaternion.Euler(0,0,angle), rotate_speed);


	}

	// after cooldown, shoot a single bullet and repeat cooldown
	public void fireBullet(){
		if ((bullet_delay_timer -= Time.deltaTime) <= 0){

			// create bullet clone
			bullet_clone = Instantiate(bullet, shot_spawn.position, shot_spawn.rotation) as GameObject;
			bullet_clone.gameObject.GetComponent<player_ID>().player_number = -1;
			bullet clone_bullet = bullet_clone.GetComponent<bullet>();
			clone_bullet.speed = bullet_speed;
			clone_bullet.spawn_time = bullet_spawn_time;

			// restart the cooldown
			bullet_delay_timer = bullet_delay;
		}
	}

	public void setDriver(player_driver _driver){
		driver = _driver;
	}

	public player_driver getDriver(){
		return driver;
	}


}





/*
 * 
 * Will re-work the target turret to detect the driver that first steps into the scanner
 * 
 * 
*/

