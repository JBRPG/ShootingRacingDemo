using UnityEngine;
using System.Collections;


/*
 * 
 * This is the bullet base class
 * 
*/

public class bullet : MonoBehaviour {

	// for inspector
	public float speed;
	public float spawn_time; // despawn if time reaches 0
	public float energy_val;
	
	
	// for use in child classes
	protected float spawn_timer;
	protected Rigidbody2D rb2d;
	protected Transform tsfm;
	protected player_ID ID_player;


	// Use this for initialization

	public virtual void Awake(){
		
		initializeBullet();
	}

	protected void initializeBullet(){
		spawn_timer = spawn_time;
		rb2d = gameObject.GetComponent<Rigidbody2D>();
		tsfm = gameObject.GetComponent<Transform>();
		ID_player = gameObject.GetComponent<player_ID>();

	}

	// Update is called once per frame
	public virtual void Update () {
		checkSpawnTimer();
		
	}
	
	// FixedUpdate is called once per physics step
	public virtual void FixedUpdate(){
		rb2d.velocity = tsfm.right * speed;
	}
	
	
	protected void checkSpawnTimer(){
		spawn_timer -= Time.deltaTime;
		if (spawn_timer <= 0){
			Destroy(gameObject);
		}
	}
	
	
	// we want the collision from the bullet to be simple
	// the other collision triggers will happen 
	protected void OnTriggerEnter2D(Collider2D col){

		if (col.CompareTag("Bullet")){
			
			if ((col.gameObject.GetComponent<player_ID>().player_number) != getPlayerID()){
				Destroy(col.gameObject);
			}
			
		}
		
		
	}
	
	public int getPlayerID(){
		return ID_player.player_number;
	}

	public void setPlayerID(int _num){
		ID_player.player_number = _num;
	}

}



/*
 * Lessons Learned:
 * 
 * When an object is instantiated: Awake is called first.
 * After the object is instantiated, then Start is called.
 * For the rest of the object's life, Update is called until destroyed.
 * 
 * 
 * 
 */


