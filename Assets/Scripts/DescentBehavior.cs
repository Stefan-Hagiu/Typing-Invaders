using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescentBehavior : MonoBehaviour {
	int behaviorType;
	public GameObject globalScript;
	int targeted = 0;
	float speed;
	float horizontalSpeed;
	float changeDirectionCooldown = 0;
	float initialCooldown = 2;
	int direction;
	float maxHorizontalSpeed;
	public void setGlobalScript (GameObject gs) {
		globalScript = gs;
	}
	public void onTargeted() {
		targeted = 1;
	}
	void Start () {
		behaviorType = Random.Range (2, 3);
		speed = (float) Random.Range (1, 101) / 10000;
		maxHorizontalSpeed = (float)Random.Range (50, 301) / 10000;
		horizontalSpeed = maxHorizontalSpeed;
		direction = Random.Range (0, 2);
		if (direction == 0) {
			direction = -1;
		}
	}
		
	void Update () {
		changeDirectionCooldown -= (float)0.01;
		if (behaviorType == 0) {
		} else if (behaviorType == 1) {
			this.transform.Translate (new Vector3 (0, -speed));
		} else if (behaviorType == 2) {
			this.transform.Translate (new Vector3 (direction * horizontalSpeed, -speed));
			if (horizontalSpeed < maxHorizontalSpeed) {
				horizontalSpeed += (float)0.04;
			}
			if (direction == -1 && !this.GetComponentInChildren<MeshRenderer>().isVisible && changeDirectionCooldown <= 0) {
				horizontalSpeed = -horizontalSpeed;
				changeDirectionCooldown = initialCooldown;
			}
			else if (direction == 1 && !this.GetComponentInChildren<MeshRenderer>().isVisible && changeDirectionCooldown <= 0) {
				horizontalSpeed = -horizontalSpeed;
				changeDirectionCooldown = initialCooldown;
			}
			if (horizontalSpeed < 0) {
				horizontalSpeed = -horizontalSpeed;
				direction = -direction;
			}
			if (horizontalSpeed > maxHorizontalSpeed) {
				horizontalSpeed -= (float)0.01;
			}
		}
		if (this.gameObject.transform.position.y <= (float)-4.5 && targeted == 0 && globalScript != null) {
			globalScript.SendMessage ("gameOver");
		}
	}
}
