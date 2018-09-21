using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeScript : MonoBehaviour {

	public float GrenadeTimer = 3f;

	private Vector2 speed = new Vector2 (75, 0);
	private Rigidbody2D rbGrenade;

	// Use this for initialization
	void Start () {
		rbGrenade = GetComponent<Rigidbody2D>();
		rbGrenade.velocity = speed * this.transform.localScale.x;
		Destroy (gameObject, GrenadeTimer);

	}

	// Update is called once per frame
	void Update () {

	}
}