using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocket : MonoBehaviour {
	public float speed = 10;
	public float rotSpeed = 10;
	public Transform target;
	public float radius = 10;
	public float power = 10;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Quaternion rot = Quaternion.LookRotation (target.position - transform.position);
		this.transform.rotation = Quaternion.Slerp (transform.rotation, rot, rotSpeed * Time.deltaTime);
		this.transform.position += transform.forward * speed * Time.deltaTime;

	}

	void OnCollisionEnter(Collision col){
		print (col.transform.name);
		if (col.transform.tag == "Player") {
			col.transform.GetComponent<PlayerManager> ().Health -= 20;
		}
		Vector3 explosionPos = transform.position-transform.forward*7;
		Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
		foreach (Collider hit in colliders) {
			Rigidbody rb = hit.GetComponent<Rigidbody>();

			if (rb != null)
				rb.AddExplosionForce(power, explosionPos, radius, 3.0f);

		}
		Destroy (this.gameObject);
	}
}
