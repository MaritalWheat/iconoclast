using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Server : MonoBehaviour {

	private List<Vector2> m_hostCharacterLocations, m_clientCharacterLocations, m_proposedHostCharacterLocations, m_proposedClientCharacterLocations;
	private bool m_waitingForClient = true, m_waitingForHost = true;

	// Use this for initialization
	void Start () {
		Vector2 startingPos = new Vector2 (-1, -1);
		m_hostCharacterLocations = new List<Vector2> ();
		m_proposedHostCharacterLocations = new List<Vector2> ();
		m_clientCharacterLocations = new List<Vector2> ();
		m_proposedClientCharacterLocations = new List<Vector2> ();

		//Initialize the character positions
		for (int i = 0; i < Squad.SQUAD_SIZE; i++) {
			m_hostCharacterLocations.Add(startingPos);
			m_clientCharacterLocations.Add(startingPos);
			m_proposedHostCharacterLocations.Add(startingPos);
			m_proposedClientCharacterLocations.Add(startingPos);
		}
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log ("CLient ready = " + !m_waitingForClient);
		Debug.Log ("Host ready = " + !m_waitingForHost);
		if (!m_waitingForClient && !m_waitingForHost) {
			ProcessTurn();
			m_waitingForHost = m_waitingForClient = true;
			NetworkManager.m_instance.StartTurns ();
		}
	}

	void ProcessTurn() 
	{
		//For now we will assume all characters have the same speed. Conflicts will be decided by a coin flip.
		for (int i = 0; i < Squad.SQUAD_SIZE; i ++) {
			if (m_proposedHostCharacterLocations[i].x != -1 && m_proposedHostCharacterLocations[i].y != -1) {
				if (m_proposedClientCharacterLocations.Contains(m_proposedHostCharacterLocations[i])) {
					//We have found a conflict, flip the coin and move one back to where it was
					Debug.Log("Conflict");
					if (Random.Range(-1.0f, 1.0f) > 0) {
						MoveCharacter(false, m_proposedHostCharacterLocations.IndexOf(m_proposedHostCharacterLocations[i]));
					} else {
						MoveCharacter(true, i);
					}
				}
			}
		}
	}
	
	//This function will move a proposed position, and check if it affects anything else. 
	void MoveCharacter(bool moveHost, int characterNum) 
	{	
		List<Vector2> proposed, previous, otherProposed;
		if (moveHost) {
			proposed = m_proposedHostCharacterLocations;
			previous = m_hostCharacterLocations;
			otherProposed = m_proposedClientCharacterLocations;
		} else {
			proposed = m_proposedClientCharacterLocations;
			previous = m_clientCharacterLocations;
			otherProposed = m_proposedHostCharacterLocations;
		}
		//See if we stepped on our own
		if (proposed[characterNum].x != -1 && proposed[characterNum].y != -1) {
			if (proposed.Contains(previous[characterNum])) {
				MoveCharacter(moveHost, proposed.IndexOf(previous[characterNum]));
			} else if(otherProposed.Contains(previous[characterNum])) {
				MoveCharacter(!moveHost, otherProposed.IndexOf(previous[characterNum]));
			}
			proposed[characterNum] = previous[characterNum];
		}
	}

	public void SetProposedLocation(bool isHost, int characterNum, Vector2 coords)
	{
		List<Vector2> proposedLocations;
		if (isHost) {
			proposedLocations = m_proposedHostCharacterLocations;
		} else {
			proposedLocations = m_proposedClientCharacterLocations;
		}
		Debug.Log (characterNum + ":" + proposedLocations.Count);
		proposedLocations[characterNum] = coords;
	}

	public void FinishTurn(bool isHost) 
	{
		if (isHost) {
			m_waitingForHost = false;
		} else {
			m_waitingForClient = false;
		}
	}


}
