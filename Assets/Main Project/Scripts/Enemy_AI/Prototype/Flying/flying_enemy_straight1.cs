using UnityEngine;
using System.Collections;

/*
 * For this prototype, we will have a different flying unit that
 * shoots bullets that take the velocity of the driver from the current frame only
 *
*/


public class flying_enemy_straight1 : MonoBehaviour {

	public Transform driver_player;
	public float speed;
	public int health;
	public int shotLimit;
	public float shotDelay;
	public float shotSpawnTime;
	public float weaponCooldown;

	public float bullet_speed;

	
	private int shotCounter = 0;
	private float delayTimer = 0;
	private float cooldownTimer = 0;
	private bool isFiring = false;

	private float target_angle = 0;
	private Vector2 driver_velocity;

	private Rigidbody2D rb2d;
	private Transform tsfm;
	private player_ID p_id;


	// use that for instantiation
	void Awake(){
		rb2d = GetComponent<Rigidbody2D>();
		tsfm = GetComponent<Transform>();
		p_id = GetComponent<player_ID>();
	}
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update (){
		readyToFire();
	}

	void FixedUpdate(){
		rb2d.velocity = speed * tsfm.right;
	}


	public void fireWeapon(){
		// if target bullet enabled, pass down the transform of the Driver object
		// so that the bullets can get the velocity of the driver to create
		// the illusion of bullets flying relative to scrolling screen

		if (!isFiring){
			shotCounter = shotLimit;
			isFiring = true;
		}

	}

	void fireShot(){
		if (cooldownTimer != 0) return;

		if (delayTimer <= 0){

			targetPlayer();
			GameObject bullet_clone = Instantiate(Resources.Load("Prefabs/Bullets/bullet_enemy_snap"),
			                                      tsfm.position, tsfm.rotation) as GameObject;
			bullet_velocitySnap bullet_clone_bullet = bullet_clone.GetComponent<bullet_velocitySnap>();
			bullet_clone_bullet.speed = bullet_speed;
			bullet_clone_bullet.spawn_time = shotSpawnTime;
			bullet_clone_bullet.setSnapVelocity(driver_velocity);
			bullet_clone.transform.rotation = Quaternion.Euler(0,0,target_angle);
			shotCounter--;
			delayTimer = shotDelay;

		}
		if (shotCounter == 0){
			cooldownTimer = weaponCooldown;
			delayTimer = 0;
		}

	}

	void cooldownShot(){
		if (delayTimer == 0) return;
		delayTimer -= Time.deltaTime;

		if (delayTimer <= 0){
			delayTimer = 0;
		}
	}

	void cooldownWeapon(){
		if (cooldownTimer == 0) return;

		cooldownTimer -= Time.deltaTime;

		if (cooldownTimer <= 0){
			cooldownTimer = 0;
			isFiring = false;
		}
	}

	void readyToFire(){
		if (cooldownTimer == 0 && shotCounter != 0){
			fireShot();
			cooldownShot();
		} else {
			cooldownWeapon();
		}
	}



	void targetPlayer(){
		Vector3 targetDir = driver_player.position - tsfm.position;
		target_angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
		driver_velocity = driver_player.GetComponent<Rigidbody2D>().velocity;
	}

	void OnTriggerEnter2D (Collider2D col){
		if (col.CompareTag("Bullet")){

			if ((col.gameObject.GetComponent<bullet>()) != null){
				bullet _bullet = col.gameObject.GetComponent<bullet>();
				if ((_bullet.getPlayerID()) != p_id.player_number){
					Destroy(col.gameObject);
					makeExplosion();
					Destroy(gameObject);
					
				}
				
			}
		}

	}

	void makeExplosion(){
		GameObject _explode = Instantiate(Resources.Load("Prefabs/Explosion/Explosion"),
		                                  tsfm.position, tsfm.rotation) as GameObject;
		_explode.GetComponent<explosion_ai>().setSpeed(speed);
	}
}
