using UnityEngine;
using System.Collections;

/* Base class - Movement AI
 * This base class will be uased to make multiple sub-classes of specific movements
 * The sub classes are a required component for enemy entities
 * 
 * The sub-classes of movement can have their own variables suited for different types
 * of movement
 * 
 * Movement AI gives enemy entities movement based on the given pattern inside the class.
 * The common functions of movement AI
 * 
 * move - inside the fixed update, this function will update
 *   the enemy's movement dictated by move_pattern function
 *   given by the sub-class
 * 
 * move_pattern - defines a movement pattern for the enemy entity.
 *   examples could include but not limited to: circle, chase, saw wave, etc.
 *   Initially empty for base class
 *   Customizable for sub-classes
 * 
 * 
 * The movement class can also be used as a list of movements
 * that are triggered by various state functions and booleans known as commands
 * 
*/


// If you decide to use "public abstract class", then you cannot use the base class
// directly and you need derived classes to provide inherited functions from the base class

public class movement_ai : MonoBehaviour {


	protected void move_pattern(){

	}

	protected void move(){

	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
