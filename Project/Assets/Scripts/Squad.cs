using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Squad : MonoBehaviour {

	public const int SQUAD_SIZE = 5;

	List<GameObject> characters;

	// Use this for initialization
	void Start () {
		characters = new List<GameObject>();
		for (int i = 0; i < SQUAD_SIZE; i++) {
			Debug.Log("Character " + i);
			characters.Add((GameObject)GameObject.Instantiate(GameManager.singleton.characterPrefab));
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
