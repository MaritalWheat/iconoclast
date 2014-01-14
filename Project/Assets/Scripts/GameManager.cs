using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public static GameManager m_instance;

	public GameObject m_characterPrefab;
	public GameObject m_enemyPrefab;

	//Temp
	public List<GameObject> enemies; 
	//End Temp

	Squad m_squadA, m_squadB;
	int m_totalTurns = 0;
	bool m_turnComplete;

	void Start () {
		if (m_instance == null) {
			m_instance = this;
		}
		m_squadA = this.gameObject.AddComponent<Squad> ();
		m_squadA.Initialize ();

		//For now we will create mock enemies, later this will be replaced with network manager code to display actual enemies
		enemies.Add((GameObject)GameObject.Instantiate(m_enemyPrefab));
		enemies.Add((GameObject)GameObject.Instantiate(m_enemyPrefab));
		enemies.Add((GameObject)GameObject.Instantiate(m_enemyPrefab));
		enemies.Add((GameObject)GameObject.Instantiate(m_enemyPrefab));
		enemies.Add((GameObject)GameObject.Instantiate(m_enemyPrefab));
	}

	void Update () {
		//Wait for players to finish turns
		if(!m_squadA.IsTurnComplete() || m_turnComplete) {
			//waiting
		} else {
			//Process Turns
			m_totalTurns ++;
			m_turnComplete = true;
			NetworkManager.m_instance.FinishPlayerTurn();
		}
	}


	public void StartNextTurn() 
	{
		m_turnComplete = false;
		m_squadA.NextTurn();
	}


	public void MoveEnemy(int enemyNum, Vector2 newCoords) 
	{
		Debug.Log (enemyNum + " : " + newCoords);
		if (enemyNum < enemies.Count) {
			enemies[enemyNum].GetComponent<Enemy>().SetPosition(newCoords);
		}
	}

	public List<Vector2> GetCharacterPositions() {
		return m_squadA.GetCharacterPositions ();
	}

}
