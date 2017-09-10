using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Global : MonoBehaviour {
	int score = 0;
	string currentWord;

	public GameObject flyingSaucer;
	public TextAsset wordList;
	public GameObject rocket;
	public GameObject gameOverText;
	public GameObject writeCurrentWord;
	GameObject currentFlyingSaucer;

	public GameObject barrel1;
	public GameObject barrel2;
	public GameObject barrel3;
	public GameObject barrel4;
	public GameObject barrel5;
	public GameObject barrel6;
	GameObject[] barrels = new GameObject[6];

	float respawnTime = (float) 1.5;
	float timeUntilRespawn;
	List <string> words = new List<string>();
	List <GameObject> generatedFlyingSaucers = new List <GameObject> ();
	void readWords() {
		string s;
		string currentWord = "";
		s = wordList.ToString();
		int i;
		for (i = 0; i < s.Length; i++) {
			if (s [i] != '\n') {
				currentWord += s [i];
			} else {
				words.Add (currentWord);
				currentWord = "";
			}
		}
	}
	void Start () {
		barrels [0] = barrel1;
		barrels [1] = barrel2;
		barrels [2] = barrel3;
		barrels [3] = barrel4;
		barrels [4] = barrel5;
		barrels [5] = barrel6;
		readWords ();
		timeUntilRespawn = respawnTime;
	}
	void generateNewSaucer() {
		TextMesh attachedText;
		string currentText = words[Random.Range (0, words.Count)];
		GameObject currentFlyingSaucer = Instantiate (flyingSaucer,
			new Vector3 ( (float) flyingSaucer.transform.position.x + (float) Random.Range(100, 1800) / 100,
								(float) flyingSaucer.transform.position.y,
									(float) flyingSaucer.transform.position.z), 
			flyingSaucer.transform.rotation);
		attachedText = currentFlyingSaucer.GetComponentInChildren<TextMesh> ();
		attachedText.text = currentText;
		currentFlyingSaucer.SendMessage ("setGlobalScript", this.gameObject);
		generatedFlyingSaucers.Add (currentFlyingSaucer);
	}

	void readInput() {
		if (Input.GetKeyDown (KeyCode.Space)) {
			for (int i = 0; i < generatedFlyingSaucers.Count; i++) {
				currentFlyingSaucer = generatedFlyingSaucers [i];
				if (currentFlyingSaucer.GetComponentInChildren<TextMesh> ().text == currentWord) {
					launchRocket (currentFlyingSaucer);
					generatedFlyingSaucers.Remove (currentFlyingSaucer);
					currentWord = "";
					break;
				}
			}
			currentWord = "";
		} else if (Input.GetKeyDown ("return")) {
			
		} else if (Input.GetKeyDown(KeyCode.Backspace)) {
			currentWord = currentWord.Remove (currentWord.Length - 1);
		} else {
			if (Input.inputString != "\b") {
				currentWord += Input.inputString;
			}
		}
		(writeCurrentWord.GetComponent<TextMesh> ()).text = currentWord;
	}

	void Update () {
		timeUntilRespawn -= Time.deltaTime;
		if (timeUntilRespawn <= 0) {
			timeUntilRespawn = respawnTime;
			generateNewSaucer ();
		}
		readInput ();
	}

	void launchRocket (GameObject target) {
		score++;
		GameObject chosenBarrel;
		int barrelNumber = Random.Range (0, 6);
		chosenBarrel = barrels [barrelNumber];
		GameObject currentRocket = Instantiate <GameObject> (rocket, chosenBarrel.transform.position, chosenBarrel.transform.rotation);
		currentRocket.SendMessage ("setTarget", target);
	}
	void rocketFailed (GameObject flyingSaucer) {
		generatedFlyingSaucers.Add (flyingSaucer);
	}
	public void gameOver () {
		(writeCurrentWord.GetComponent<TextMesh> ()).text = "";
		(gameOverText.GetComponent<TextMesh> ()).text = "Game over. Score:" + score.ToString () + ". Press any key to restart";
		Destroy (this.gameObject);
	}
}
