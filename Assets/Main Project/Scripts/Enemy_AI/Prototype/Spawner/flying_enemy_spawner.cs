using UnityEngine;
using System.Collections;

/* This simple spawner will create flying enemies
 * when the player passes by the spawner and
 * and the spawner is inactive
 * 
 * if active, it will produce flying enemies
 * until the unit's spawn limit is reached;
 * making it inactive and ready for trigger again. 
 * 
 */

public class flying_enemy_spawner : MonoBehaviour {
	
	public GameObject flyingEnemy;
	public float launchAngle;
	public float launchSpeed;
	public float launchDelay;
	public int spawnLimit;
	public float spawnDelay;
	
	private bool spawning_enemies = false;
	private int spawn_count = 0;
	private float spawn_delay_time = 0;
	private GameObject flying_enemy_clone;
	private float launch_wait_time = 0;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		EnemyLaunchPeriod();
	}

	// If spawning is active, then do spawn count until it reaches zero
	void Spawn_enemy(){
		if (!spawning_enemies) return;

		// create enemy with the given parameters
		if (spawn_delay_time <= 0){
			flying_enemy_clone = Instantiate(flyingEnemy, transform.position, transform.rotation) as GameObject;

			if (flying_enemy_clone.GetComponent<flying_enemy_straight>() != null){
				
				flying_enemy_straight fesObj = flying_enemy_clone.GetComponent<flying_enemy_straight>();
				fesObj.speed = launchSpeed;
				flying_enemy_clone.transform.rotation.Set(0,0,launchAngle,0);
				fesObj.driver_player = GameObject.FindWithTag("Driver").transform;
			}

			else if (flying_enemy_clone.GetComponent<flying_enemy_straight1>() != null){
				
				flying_enemy_straight1 fesObj = flying_enemy_clone.GetComponent<flying_enemy_straight1>();
				fesObj.speed = launchSpeed;
				flying_enemy_clone.transform.rotation.Set(0,0,launchAngle,0);
				fesObj.driver_player = GameObject.FindWithTag("Driver").transform;
			}

			
			spawn_count--;
			if (spawn_count <= 0){
				spawning_enemies = false;
				launch_wait_time = launchDelay;
			}else{
				spawn_delay_time = spawnDelay;
			}

		}

	}

	public void ProduceFlyingEnemy(){
		if (spawning_enemies || launch_wait_time > 0) return;
		spawning_enemies = true;
		launch_wait_time = 0;
		spawn_delay_time = 0;
		spawn_count = spawnLimit;

	}

	void EnemyLaunchPeriod(){

		if (launch_wait_time <= 0){
			Spawn_enemy();
			SpawnDelayPeriod();
		}
		else {
			launch_wait_time -= Time.deltaTime;
		}

	}

	void SpawnDelayPeriod(){
		if (spawn_delay_time <= 0){
			spawn_delay_time = 0;
		} else {
			spawn_delay_time -= Time.deltaTime;
		}
	}

}


/* Enemy launch system
 * 
 * When player crosses line
 * 
 * if spawner enters cooldown, do not activate enemies
 * 
 */


