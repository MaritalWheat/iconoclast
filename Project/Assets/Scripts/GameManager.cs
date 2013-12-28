using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager singleton;

	public GameObject characterPrefab;

	Squad squadA, squadB;
	int totalTurns = 0;
	// Use this for initialization
	void Start () {
		if (singleton == null) {
			singleton = this;
		}
		squadA = this.gameObject.AddComponent<Squad> ();
		squadA.startTurn ();
	}
	
	// Update is called once per frame
	void Update () {
		//Wait for players to finish turns
		if(!squadA.isTurnComplete()) {
			//waiting
		} else {
			//Process Turns
			totalTurns ++;
			Debug.Log("This is where we process what happened for turn number " + totalTurns);
			//Start the next round of turns
			squadA.startTurn();
		}
	}
}
