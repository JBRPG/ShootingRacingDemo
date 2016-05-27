using UnityEngine;
using System.Collections;

/* This derived enemy class is used only for testing movement behaviors
 * 
 * This enemy entity type cannot be destroyed nor act against the player
 * 
 * 
*/


public class enemy_movement_test : enemy_ai {

	/* Only movement is the required component from this enemy subclass
	 * 
	 * For movements derived from movement_ai, player reference is needed to set target at player position
	 * 
	*/


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
