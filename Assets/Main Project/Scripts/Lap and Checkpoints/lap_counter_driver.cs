using UnityEngine;
using System.Collections;

public class lap_counter_driver : MonoBehaviour {

	public trackLapTrigger startLine;

	private trackLapTrigger next;

	private int _lap = 0;
	private float energyBonus = 0.1f; // When driver crosses correct trigger, then give bonus energy
	private trackLapTrigger curr;


	// Use this for initialization
	void Start () {
		SetNextTrigger(startLine.next);
	
	}

	// when lap trigger is entered
	public void OnLapTrigger(trackLapTrigger _trigger){
		if (_trigger == next) {
			player_driver _driver = gameObject.GetComponent<player_driver>();
			if (startLine.next == next) {
				_lap++;

				if (_driver){
					_driver.addEnergy(energyBonus * 2);
				}
			}
			else{
				
				if (_driver){
					_driver.addEnergy(energyBonus);
				}
			}
			SetNextTrigger(next);
		}
	}

	private void SetNextTrigger (trackLapTrigger _trigger){
		next = _trigger.next;
	}
}
