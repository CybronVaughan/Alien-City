using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

	public float BulletTimer = 3f;

	private Vector2 speed = new Vector2 (55, 0);
	private Rigidbody2D rbBullet;

	// Use this for initialization
	void Start () {
		rbBullet = GetComponent<Rigidbody2D>();
		rbBullet.velocity = speed * this.transform.localScale.x;
		Destroy (gameObject, BulletTimer);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}