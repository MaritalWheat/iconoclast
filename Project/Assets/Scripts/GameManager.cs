using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public static GameManager m_instance;

	public GameObject m_characterPrefab;
	public GameObject m_enemyPrefab;

	//Temp
	public List<GameObject> enemies; 

	Squad m_squadA, m_squadB;
	int m_totalTurns = 0;

	void Start () {
		if (m_instance == null) {
			m_instance = this;
		}
		m_squadA = this.gameObject.AddComponent<Squad> ();
		m_squadA.Initialize ();

		//For now we will create mock enemies, later this will be replaced with network manager code to display actual enemies
		enemies.Add((GameObject)GameObject.Instantiate(m_enemyPrefab));

		enemies[0].GetComponent<Enemy>().SetPosition(new Vector2(9,3));
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
