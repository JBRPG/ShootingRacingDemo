using UnityEngine;
using System.Collections;

public class camera_follow : MonoBehaviour {
	
	// public
	public float smoothTimeX;
	public float smoothTimeY;

	public bool bounds;

	public Vector3 minCamPos;
	public Vector3 maxCamPos;

	public GameObject player;


	// private
	private Vector2 velocity;



	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		float posX = Mathf.SmoothDamp (transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);
		float posY = Mathf.SmoothDamp (transform.position.y, player.transform.position.y, ref velocity.y, smoothTimeY);
		
		transform.position = new Vector3 (posX, posY, transform.position.z);
		
		
		
		if (bounds) {
			transform.position = new Vector3(Mathf.Clamp(transform.position.x, minCamPos.x, maxCamPos.x),
			                                 Mathf.Clamp(transform.position.y, minCamPos.y, maxCamPos.y),
			                                 Mathf.Clamp(transform.position.z, minCamPos.z, maxCamPos.z));
			
		}
	
	}

	public void SetMinCamPosition(){
		minCamPos = gameObject.transform.position;
		
	}
	
	public void SetMaxCamPosition(){
		maxCamPos = gameObject.transform.position;
		
	}
}
