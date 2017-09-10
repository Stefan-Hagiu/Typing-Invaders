using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class flightControl : MonoBehaviour {
	int launchstage = 0;
	Quaternion initialQ, finalQ;
	public Rigidbody2D rigidBody;
	public GameObject target;
	public GameObject boom;
	float currentSpeed = (float)0.5;
	void Start () {
		(this.GetComponentInChildren<ParticleSystem> ()).Pause();
	}

	public void setTarget (GameObject t) {	
		target = t;
	}

	// Update is called once per frame
	void Update () {
		if (launchstage >= 2) {
			this.transform.up = target.transform.position - this.transform.position;
		}
		if (launchstage == 3) {
			if ((this.GetComponentInChildren<ParticleSystem> ()).isPaused) {
				(this.GetComponentInChildren<ParticleSystem> ()).Play ();
			}
			this.transform.position = Vector3.MoveTowards (this.transform.position, target.transform.position, currentSpeed);
			currentSpeed += (float)0.05;
		}
		if (this.transform.position.y < -10) {
			launchstage = 3;
		}
		if (rigidBody.IsTouching(target.GetComponents<Collider2D>()[0] as Collider2D)) {
			(this.GetComponentInChildren<ParticleSystem> ()).Stop ();
			(this.GetComponentInChildren<ParticleSystem> ()).transform.localScale *= 5;
			(this.GetComponentInChildren<ParticleSystem> ()).transform.parent = null;
			Instantiate<GameObject> (boom, this.transform.position,this.transform.rotation);
			Destroy(target);
			Destroy(this.gameObject);
		}
	}
	void FixedUpdate() {
		if (launchstage == 0) {
			launchstage = 1;
		} else if (launchstage == 1) {
			rigidBody.AddForce (new Vector2 (0, (float)100.7));
			if (rigidBody.velocity.magnitude > 6) {
				launchstage = 2;
			}
		} else if (launchstage == 2) {
			if (rigidBody.velocity.magnitude < 0.3) {
				launchstage = 3;
				rigidBody.gravityScale = 0;
			}
		}
	}
}
