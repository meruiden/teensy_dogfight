using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour {
	public float speed = 10;
	// Use this for initialization
	void Start () {
		Destroy (this.gameObject, 10);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += transform.forward * Time.deltaTime * speed;
	}
}
