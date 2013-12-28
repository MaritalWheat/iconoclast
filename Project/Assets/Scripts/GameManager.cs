using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager m_instance;

	public GameObject m_characterPrefab;

	Squad m_squadA, m_squadB;
	int m_totalTurns = 0;

	void Start () {
		if (m_instance == null) {
			m_instance = this;
		}
		m_squadA = this.gameObject.AddComponent<Squad> ();
		m_squadA.StartTurn ();
	}

	void Update () {
		//Wait for players to finish turns
		if(!m_squadA.IsTurnComplete()) {
			//waiting
		} else {
			//Process Turns
			m_totalTurns ++;
			Debug.Log("This is where we process what happened for turn number " + m_totalTurns);
			//Start the next round of turns
			m_squadA.NextTurn();
		}
	}
}
