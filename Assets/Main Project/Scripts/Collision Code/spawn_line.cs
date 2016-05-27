using UnityEngine;
using System.Collections;

/* This will call its spawner object to create enemies
 * and the spawner will handle enemu generation afterwards
 *
 * This is a child to a spawner object so
 * 
*/

public class spawn_line : MonoBehaviour {



	private flying_enemy_spawner spawner;

	// Use this for initialization
	void Start () {
		spawner = gameObject.GetComponentInParent<flying_enemy_spawner>();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// when driver enters the boundary line, summon enemies from spawner
	void OnTriggerEnter2D(Collider2D col){
		if (col.CompareTag("Driver")){
			spawner.ProduceFlyingEnemy();

		}

	}


}
