using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/*
 * Each player gets their own energy meter, 
 * which will be displayed on seperate canvases
 * (aka different player screens)
 * 
 * This script will require the player driver
 * to properly represent the player's meter
 * 
*/


public class player_energy_meter : MonoBehaviour {

	public GameObject driverObject; // needed to get player infomration
	public Image MeterCharged;
	public Image MeterStored;

	private player_driver p_driver;

	// Use this for initialization
	void Start () {
		p_driver = driverObject.GetComponent<player_driver>();
	}
	
	// Update is called once per frame
	void Update () {
		MeterStored.fillAmount = p_driver.getStoreFill();
		MeterCharged.fillAmount = p_driver.getChargeFill();
	
	}
}
