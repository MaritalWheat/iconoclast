using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Squad : MonoBehaviour {

	public const int SQUAD_SIZE = 5;

	List<GameObject> characters;

	bool turnComplete = true;

	// Use this for initialization
	void Start () {
		characters = new List<GameObject>();
		for (int i = 0; i < SQUAD_SIZE; i++) {
			characters.Add((GameObject)GameObject.Instantiate(GameManager.singleton.characterPrefab));
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI () {
		if (!turnComplete) {
			if (GUI.Button(new Rect(0, 200, 100, 50), "Finish Turn")) {
				turnComplete = true;
			}
		}
	}

	public void startTurn() {
		turnComplete = false;
	}

	public bool isTurnComplete() {
		return turnComplete;
	}

}
