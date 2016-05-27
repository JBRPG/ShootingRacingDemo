using UnityEngine;
using System.Collections;

/*
 * The hitbox will be used to do the following:
 * Serve as primary aim-based target from enemy and bullet entities
 * Reduce the player's energy meter and speed upon hit
 */

public class player_hitbox : MonoBehaviour {
	
	
	private player_driver driver;
	private player_ID p_id;
	
	// Use this for initialization
	void Start () {
		driver = gameObject.GetComponentInParent<player_driver>();
		p_id = gameObject.GetComponentInParent<player_ID>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	// will work on later after doing energy meter
	void OnTriggerEnter2D(Collider2D col){
		// If enemy bullets hit this, player's energy reduced
		
		if (col.CompareTag("Bullet")){
			if ((col.gameObject.GetComponent<bullet>()) != null){
				bullet _bullet = col.gameObject.GetComponent<bullet>();

				if ((_bullet.getPlayerID()) != getPlayerID()){
					driver.subtractEnergy(_bullet.energy_val);
					Destroy(col.gameObject);
					
				}
				
			}
			Destroy(col.gameObject);
		}
		
	}
	
	public int getPlayerID(){
		return p_id.player_number;
	}
}

