using UnityEngine;
using System.Collections;

/* Movement Player Follow - Circle derives from Movement Player Follow
 * 
 * Summary: The enemy entity will chase towards the player until its close enough to
 *   move around the player in a circle
 * 
 * Technical:
 * This derived class will allow the affected enemy entity to
 *   move towards the player, and when between two proximity radii (player and faraway),
 *   The enemy will follow the target circle (aka transform object) that moves in a circle
 *   relative to the player object at a set radius away from origin of player object
 *   
 *   The Enemy entity will, using its own velocity, move towards the target circle
 *   to give illusion of enemy moving around player like a circle
 * 
*/

public class movement_PlayerFollow_circle : movement_PlayerFollow {

	// Any public and protected variables inherited from Movement Player Follow
	// are accessible for this derived class. Refer to the parent class for the variables



	// variables used for inspector
	public float target_circle_radius;


	// variables used internally
	private Transform target_circle; // used for enemy to follow, and attaches to player at distance away




	// movement functions

	protected new void move(){


	}

	// for player follow circle, calculate distance between target cirle and enemy
	// when inside the faraway radius but not close to player radius
	protected new void move_pattern(){


	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
