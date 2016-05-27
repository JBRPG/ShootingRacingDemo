using UnityEngine;
using System.Collections;

public class explosion_ai : MonoBehaviour {


	private float speed;

	private Rigidbody2D rb2d;

	private IEnumerator KillOnAnimationEnd(){
		yield return new WaitForSeconds (0.6f);
		Destroy(gameObject);
	}

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D>();
	
	}
	
	// Update is called once per frame
	void Update () {
		StartCoroutine(KillOnAnimationEnd());
	}

	void FixedUpdate(){
		rb2d.velocity = speed * transform.right;
	}

	public void setSpeed(float _speed){
		speed = _speed;
	}
}
