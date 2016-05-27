using UnityEngine;
using System.Collections;
using Rewired;

public class player_defender : MonoBehaviour {
	
	public float turn_rate;
	
	
	private Transform tsfm;
	private Transform shot_spawn;
	private Vector3 moveVector;
	private Player rewired_player;

	private player_ID p_id;
	private player_driver driver;
	
	// Use this for initialization
	void Start () {
		
		tsfm = gameObject.GetComponent<Transform> ();
		shot_spawn = tsfm.GetChild(0);
		p_id = GetComponent<player_ID>();


		if (driver != null){
			p_id.player_number = driver.getPlayerID();
			rewired_player = ReInput.players.GetPlayer(p_id.player_number);
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		GetInput();

	}
	
	void FixedUpdate(){
		
		ProcessInputs();
		
	}

	// Rewired controls

	private void GetInput(){
		moveVector.x = rewired_player.GetAxis("Aim X");
		moveVector.y = rewired_player.GetAxis("Aim Y");


	}

	private void ProcessInputs(){
		aim();

	}

	// Will redo with ReWired input controls

	void aim(){

		if (moveVector.x == 0 && moveVector.y == 0)
			return;
		
		float aimAngle = Mathf.Atan2 (moveVector.y, moveVector.x) * Mathf.Rad2Deg;
		
		tsfm.rotation = Quaternion.RotateTowards (tsfm.rotation, Quaternion.Euler (0, 0, aimAngle), turn_rate);


	}
	
	// will be used to generate shots depending on charge level
	public void shootWeapon(){
		// shoot bullets based on charge level
		float chargeLevel = driver.getChargeFill();
		if (chargeLevel >= 0.2f){
			produceBullets(chargeLevel);
		}
		
	}
	
	// for protoype test, we will have a classic spread shot as weapon
	//
	
	void produceBullets(float chargeVal){
		float chargeStrength = 5 * chargeVal;
		int weaponLevel = (int)chargeStrength; // truncating on purpose for level range
		if (weaponLevel == 1){
			makeShot(0);
		}
		else if (weaponLevel == 2){
			makeShot(-5f);
			makeShot(5f);
		}
		else if (weaponLevel == 3){
			makeShot(0);
			makeShot(-7.5f);
			makeShot(7.5f);
		}
		else if (weaponLevel == 4){
			makeShot(-2.5f);
			makeShot(2.5f);
			makeShot(-7.5f);
			makeShot(7.5f);
		}
		else if (weaponLevel == 5){
			makeShot(0);
			makeShot(-5f);
			makeShot(5f);
			makeShot(-10f);
			makeShot(10f);
		}
		
		float chargeCost = ((float) weaponLevel) / 5;
		driver.subtractEnergy(chargeCost);
		
		
	}

	// Will redo after making subclasses from bullets

	void makeShot(float angle){
		//*
		GameObject bullet_clone = Instantiate(Resources.Load("Prefabs/Bullets/bullet_player"),
		                                      shot_spawn.position, shot_spawn.rotation) as GameObject;
		bullet_velocitySnap bullet_clone_bullet = bullet_clone.GetComponent<bullet_velocitySnap>();
		bullet_clone_bullet.speed = driver.getSpeed();
		bullet_clone_bullet.spawn_time = 5;
		bullet_clone.transform.Rotate(0,0,angle);
		bullet_clone_bullet.setSnapVelocity(driver.gameObject.GetComponent<Rigidbody2D>().velocity);
		bullet_clone_bullet.setPlayerID(driver.getPlayerID());

		//*/
		
	}

	void OnTriggerEnter2D(Collider2D col){
		//*
		if (col.CompareTag("Bullet")){
			
			if ((col.gameObject.GetComponent<bullet>()) != null){
				bullet _bullet = col.gameObject.GetComponent<bullet>();
				if ((_bullet.getPlayerID()) != getPlayerID()){
					driver.addEnergy(_bullet.energy_val);
					Destroy(col.gameObject);

				}

			}
			
		}

		
	}
	
	public int getPlayerID(){
		return p_id.player_number;
	}

	public void setPlayerID(int _num){
		p_id.player_number = _num;
	}
	
	public void setDriver(player_driver driver_behavior){
		driver = driver_behavior;
	}

}
