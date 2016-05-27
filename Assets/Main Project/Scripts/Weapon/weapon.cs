using UnityEngine;
using System.Collections;

/* This is the base class for weapons
 * 
 * A weapon class consist of a list of projectiles (aka bullets)
 * to be fired based on the given behavior
 * 
 * Common properties of an individual weapon includes - 
 *  - List of bullets/projectiles (gameObjects)
 *  - List of bullet delay time (?)
 *  - 2D List of bullet collection (?)
 *  - Cooldown-time (float)
 *  - State-triggered (boolean)
 *  - Trigger State Function (param: bool)
 *  - 
 *
 *
 * If a weapon list was used, then it would have the following - 
 *  - Shoot all (iterate through all elements upon shoot all)
 *  - Curr Weapon
 *  - Next Weapon
 *  - Prev Weapon
 *  - Get weapon (access current element to retrieve weapon data)
 *  - Set weapon (only for initialization purpose)
 * 
 * 
*/

// If you decide to use "public abstract class", then you cannot use the base class
// directly and you need derived classes to provide inherited functions from the base class

public class weapon : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
