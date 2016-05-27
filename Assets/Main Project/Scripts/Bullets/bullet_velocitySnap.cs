using UnityEngine;
using System.Collections;


/*
 * 
 * Velocity Snap bullets: derived from bullet class
 * 
*/


public class bullet_velocitySnap : bullet {

	private Vector2 velocity_snap;

	public override void Awake(){
		base.Awake ();
	}

	public override void Update(){
		base.Update();
	}

	public override void FixedUpdate(){
		updateVelocity();
	}

	private void updateVelocity(){
		
		rb2d.velocity = speed * tsfm.right;
		rb2d.velocity += velocity_snap;
		
	}
	
	
	public void setSnapVelocity(Vector2 _snapVelocity){
		velocity_snap = _snapVelocity;
	}

}
