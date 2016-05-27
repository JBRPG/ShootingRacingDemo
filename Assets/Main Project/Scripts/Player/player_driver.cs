using UnityEngine;
using System.Collections;
using Rewired;

public class player_driver : MonoBehaviour {

	// public parameters (on the Editor)
	public float acceleration;
	public float steer;
	public GameObject defend_unit;
	
	
	// internal parameters
	private Rigidbody2D rb2d;
	private Transform tsfm;
	private GameObject _defender; // only one clone can spawn
	private Transform hitBox;
	private player_ID p_id;
	private Vector3 moveVector;
	private Player rewired_player;

	
	// now to add in stats for energy meter
	private float store_fill = 0.2f;
	private float charge_fill = 0.0f;
	private float speed_multiplier = 3;
	private float max_fill = 1.0f;
	
	//charge type booleans
	private bool charge_shoot = false;
	private bool charge_boost = false;
	private bool charge_shoot_prev = false;
	private bool charge_boost_prev = false;
	
	private bool isBoosting = false;
	private bool isShooting = false;
	
	// driver stats
	private float curr_speed = 0;
	private float min_speed = 1; // maximum speed is handled by store meter * speed multiplier
	private float speed_level = 0;
	
	// boost mode
	private float prev_speed = 0;
	private float boostMultiplier = 2.0f; // Might go public if wanting to make different stat-based cars
	private float boostSpeedMax = 8.0f;


	// Use this for initialization when application runs but after script is initialized
	void Start () {

		rb2d = gameObject.GetComponent<Rigidbody2D> ();
		tsfm = gameObject.GetComponent<Transform> ();
		hitBox = transform.FindChild ("Driver_Hitbox");
		p_id = GetComponent<player_ID>();
		speed_level = min_speed;
		rewired_player = ReInput.players.GetPlayer(p_id.player_number);
		
		makeDefender ();

	}
	
	// Update is called once per frame
	void Update () {

		adjustSpeed();

		// Rewired controls
		GetInput();
	
	}

	void FixedUpdate(){
		
		ProcessInputs();
		
		ChargeFillUpdate();
		updateMeters();
		_defender.transform.position = tsfm.position;

	}

	// Will test after successful test of driver movement
	void makeDefender(){
		_defender = (GameObject)Instantiate (defend_unit,tsfm.position,tsfm.rotation);
		_defender.gameObject.GetComponent<player_defender>().setDriver(
			gameObject.GetComponent<player_driver>());
		_defender.transform.localPosition = Vector3.zero;
		_defender.transform.localRotation = Quaternion.identity;
		_defender.transform.rotation = tsfm.rotation;
	}

	// functions involving energy meter

	private void adjustSpeed(){
		speed_level = min_speed + (speed_multiplier * store_fill);
	}

	void updateMeters(){
		if (store_fill < 0) store_fill = 0;
		else if (store_fill > max_fill) store_fill = max_fill;
		
		if (charge_fill < 0) store_fill = 0;
		else if (charge_fill > store_fill) charge_fill = store_fill;
	}

	
	// getters

	public Vector2 getVelocity(){
		return rb2d.velocity;
	}
	
	public float getSpeed(){
		return curr_speed;
	}
	
	public Transform getHitbox(){
		return hitBox;
	}
	
	public float getStoreFill(){
		return store_fill;
	}
	
	public float getChargeFill(){
		return charge_fill;
	}
	
	public int getPlayerID(){
		return p_id.player_number;
	}
	
	public void addEnergy(float energy){
		store_fill += energy;
		roundupEnergy();
	}
	
	
	public void subtractEnergy(float energy){
		store_fill -= energy;
		roundupEnergy();
	}
	
	void roundupEnergy(){
		store_fill = (Mathf.Round(store_fill * 1000f) / 1000f);
	}


	// Player controls functions

	private void GetInput(){

		// Steer Axis
		moveVector.x = rewired_player.GetAxis("Steer X");
		moveVector.y = rewired_player.GetAxis("Steer Y");


		// Reserved for Boost and Charge Buttons


	}

	private void ProcessInputs(){
		Steering ();
		Accelerate ();
	}

	// Player Actions

	private void Steering(){
		if (moveVector.x == 0 && moveVector.y == 0){
			return;
		}

		float steerAngle = Mathf.Atan2(moveVector.y, moveVector.x) * Mathf.Rad2Deg;

		tsfm.rotation = Quaternion.RotateTowards (tsfm.rotation, Quaternion.Euler(0,0,steerAngle), steer);

	}

	private void Accelerate(){

		if (curr_speed < speed_level) {
			curr_speed += acceleration;
		} else if (curr_speed > speed_level) {
			curr_speed -= acceleration;
		}
		
		
		rb2d.velocity = tsfm.right * curr_speed;
		rb2d.angularVelocity = 0.0f;
	}

	void ChargeFillUpdate(){
		
		// when player is firing or boosting, charging is disabled;
		//if (isBoosting || isShooting) return;
		
		charge_shoot = rewired_player.GetButton("Charge Shoot");
		charge_boost = rewired_player.GetButton("Charge Boost");
		
		/*
		 * When player holds down shoot button XOR boost button, charge meter fills up
		 * 
		 * When player lets go of either button, then go to the shoot function or boost function
		 * to consume storage energy and charge meter resets
		 * 
		 * When player is not holding either of them, the charge meter resets
		 * 
		 */
		
		if ((charge_shoot && !charge_boost) || (charge_boost && !charge_shoot)){
			charge_fill+= 0.005f;
		}
		else if (charge_shoot_prev && !charge_boost_prev){
			_defender.GetComponent<player_defender>().shootWeapon();
		}
		
		else if (charge_boost_prev && !charge_shoot_prev){
			// boost function
		}
		
		else{
			charge_fill = 0.0f;
		}
		
		// if previous frame button states are used, call the respective functions
		// and use the energy
		
		charge_boost_prev = charge_boost;
		charge_shoot_prev = charge_shoot;
		
		
	}

}
