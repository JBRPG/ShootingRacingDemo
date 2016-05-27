using UnityEngine;
using System.Collections;

/*
 * With this behavior, I want to test out this kind of bullet
 * to create the illusion that bullets fly relative
 * to the player's velocity, which is a universal element
 * in the scrolling shooting games where the
 * screen and terrain move in the opposite direction
 * of the player and the bullets are launched
 * with their fixed velocities
 * 
*/

public class bullet_velocityTarget : bullet {

	
	private Transform driver_tsfm = null;

	public override void Awake(){
		base.Awake ();
	}
	
	public override void Update(){
		base.Update();
	}

	public override void FixedUpdate(){
		updateVelocity();
	}

	void updateVelocity(){
		
		rb2d.velocity = speed * tsfm.right;
		rb2d.velocity += driver_tsfm.GetComponent<Rigidbody2D>().velocity;
		
	}

	// this function must be called after inital creation of object
	public void setDriverTSFM(Transform _tsfm){
		driver_tsfm = _tsfm;
		
	}

}
