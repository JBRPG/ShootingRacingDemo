using UnityEngine;
using System.Collections;

/* This is the base class for enemy entities
 * 
 * The enemy class will require the following elements - 
 * 
 * Internal states: Stores internal information important for the enemy
 *   ex: health, animation_state, transform, etc.
 * 
 * Movement: Allow enemy to traverse through the gameworld
 *           according to movement behavior(s)
 *    can be stored as single behavior or list of movement behaviors
 * 
 * Weapon: Allow enemy to shoot projectiles
 *         according to their behavior
 *    can be stored as single weapon or list of weapons
 * 
 * Animation: Collection of animations that are triggered by internal states
 *   Uses Unity's Animation class
 * 
 * 
 * 
 * Like Weapon and Movement class, this class benefits from having sub-classes
 *   to have specific entities like air and ground enemies, player-aiming,
 *   enemies without movement, enemies that give suicide bullets, etc.
 * 
 * 
*/


// If you decide to use "public abstract class", then you cannot use the base class
// directly and you need derived classes to provide inherited functions from the base class

public class enemy_ai : MonoBehaviour {

	// I have to figure out some way that I can require scripts as Classes
	// or else I will have to take advantage of Unity's component based system
	// by having them as private variables and then searching within the gameobject
	// that has the script components

	/*
	 * What I learned for getting components from C++ 
	 * 
	 * movement_ai enemy_move
	 * weapon enemy_weapon
	 * internal_state enemy_internal
	 * 
	*/


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
