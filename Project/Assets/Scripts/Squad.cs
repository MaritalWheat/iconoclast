using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Squad : MonoBehaviour {

	public const int SQUAD_SIZE = 5;

	List<GameObject> m_characters;

	bool m_turnComplete = true;
	bool m_addedCharacterThisTurn;
	int m_numCharactersInPlay = 0;

	Character m_selectedCharacter;
	
	void Start () {
		m_characters = new List<GameObject>();
		for (int i = 0; i < SQUAD_SIZE; i++) {
			m_characters.Add((GameObject)GameObject.Instantiate(GameManager.m_instance.m_characterPrefab));
		}
	}

	void Update () {
		HandleSelectedCharacter();
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
		GameObject characterToAdd = m_characters [m_numCharactersInPlay];
		characterToAdd.GetComponent<Character>().SetPosition(new Vector2(0,0));
		characterToAdd.GetComponent<Character>().InPlay = true;
		m_numCharactersInPlay++;
	}

	void HandleSelectedCharacter() 
	{
		Character selected = InputManager.GetCharacterClicked();
		if (selected != null && selected.InPlay) {
			if (m_selectedCharacter != null) {
				m_selectedCharacter.Selected = false; //unselect previously selected
			}

			m_selectedCharacter = selected;
			m_selectedCharacter.Selected = true;
		}
	}
}
