using UnityEngine;
using System.Collections;

public class trackLapTrigger : MonoBehaviour {

	// next line trigger
	public trackLapTrigger next;



	// when driver enters trigger, give next valid line trigger
	// and give an energy bonus

	void OnTriggerEnter2D(Collider2D other){
		lap_counter_driver lapCounter = other.gameObject.
			GetComponent<lap_counter_driver>();
		if (lapCounter){
			lapCounter.OnLapTrigger(next);
		}

	}

}
