using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Squad : MonoBehaviour {

	public const int SQUAD_SIZE = 5;

	List<GameObject> characters;

	bool turnComplete = true;

	Character m_selectedCharacter;
	
	void Start () {
		characters = new List<GameObject>();
		for (int i = 0; i < SQUAD_SIZE; i++) {
			characters.Add((GameObject)GameObject.Instantiate(GameManager.m_instance.m_characterPrefab));
		}
	}

	void Update () {
		HandleSelectedCharacter();
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

	void HandleSelectedCharacter() 
	{
		Character selected = InputManager.GetCharacterClicked();
		if (selected != null) {
			if (m_selectedCharacter != null) {
				m_selectedCharacter.Selected = false; //unselect previously selected
			}
			m_selectedCharacter = selected;
			m_selectedCharacter.Selected = true;
		}
	}
}
