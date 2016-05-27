using UnityEngine;
using System.Collections;

/* Movement Player Follow derives from Movement AI
 * 
 * This will be applied to entities that will do the following:
 *   Catch up to player to defined far-away distance
 *   Move away from player at defined player proximity distance
 *   Allow movement patterns that complement the rules above
 *   (aka enemy entities move within the two distances)
 * 
*/

public class movement_PlayerFollow : movement_ai {

	// variables visible for inspector
	public float proximity_player; // if close to player, slow down until out of player proximity
	public float proximity_faraway; // if far away, speed up until inside far-away proximity


	// variables used for this class and their children
	protected int[] players; // if only 1 player, array size is 1
	protected Vector3 player_velocity;
	protected Vector3 enemy_velocity;

	
	// variables used internally



	// movement functions

	protected new void move(){


	}

	protected new void move_pattern(){


	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
