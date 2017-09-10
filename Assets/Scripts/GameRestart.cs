using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameRestart : MonoBehaviour {
	public GameObject globalScript;
	float cooldown = 3;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (globalScript == null) {
			cooldown -= Time.deltaTime;
			if (Input.anyKey && cooldown <= 0) {
				SceneManager.LoadScene (0);
			}
		}
	}
}
