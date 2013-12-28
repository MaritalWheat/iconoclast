using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager singleton;

	public GameObject characterPrefab;

	Squad squadA, squadB;

	// Use this for initialization
	void Start () {
		if (singleton == null) {
			singleton = this;
		}
		squadA = this.gameObject.AddComponent<Squad> ();
		squadB = this.gameObject.AddComponent<Squad> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
