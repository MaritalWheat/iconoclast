using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Squad : MonoBehaviour {

	public const int SQUAD_SIZE = 5;

	List<GameObject> m_characters;

	bool m_turnComplete = true;
	bool m_addedCharacterThisTurn;
	int m_numCharactersInPlay = 0;

	// Use this for initialization
	void Start () {
		m_characters = new List<GameObject>();
		for (int i = 0; i < SQUAD_SIZE; i++) {
			m_characters.Add((GameObject)GameObject.Instantiate(GameManager.singleton.characterPrefab));
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI () {
		if (!m_turnComplete) {
			if (GUI.Button(new Rect(0, 200, 100, 50), "Finish Turn")) {
				m_turnComplete = true;
			}
			if (GUI.Button(new Rect(0, 250, 100, 50), "Add Character")) {
				AddCharacterToField();
			}
		}
	}

	public void StartTurn() {
		m_turnComplete = false;
		m_addedCharacterThisTurn = false;
	}

	public bool IsTurnComplete() {
		return m_turnComplete;
	}

	void AddCharacterToField() {
		if (m_addedCharacterThisTurn || m_numCharactersInPlay >= m_characters.Count) {
			return;
		}
		m_addedCharacterThisTurn = true;
		m_characters[m_numCharactersInPlay].GetComponent<Character>().SetPosition(new Vector2(0,0));
		m_numCharactersInPlay++;
	}

}
