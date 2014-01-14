using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Character : Moveable {

	public GameObject m_tile;
	List<Tile> neighbors;

	private bool m_move = false;
	private bool m_attack = false;
	private bool m_two = false;
	private int m_actionsPoints;
	private const int MAX_APS = 2;
	private const int NUM_SKILLS = 4;
	private Enemy targeted;
	private int m_attackCost = 1;

	//Attributes
	private int m_life = 100;
	private int m_defense = 10;
	private Speed m_speed = Speed.normal;
	private float m_range = 1.5f;
	private int m_power = 10;

	public bool InPlay {
		get;
		set;
	}

	public bool Selected {
		set;
		get;
	}

	public bool Depleted {
		set;
		get;
	}

	void Start() 
	{
		neighbors = new List<Tile>();
		m_actionsPoints = MAX_APS;
	}

	void Update() 
	{
		for (int i = 0; i < neighbors.Count; i++) {
			if (neighbors[i] != null) {
				PlayField.HighlightObjects[i].SetActive(false);
			}
		}

		HighlightAdjacentTiles();

		if (m_move) {
			Move ();
		}

		if (m_attack) {
			Attack();
		}

	}

	void OnGUI() {
		if (Selected) {
			GUI.Box(new Rect(0, 0, 200, 200), "");
			GUILayout.BeginArea(new Rect(0, 0, 200, 200));
			m_move = GUI.Toggle(new Rect(0, 0, 200, 50), m_move,  "Move");
			m_attack = GUI.Toggle(new Rect(0, 50, 200, 50), m_attack,  "Attack");
			if (m_move) {
				if (m_attack) {
					Debug.LogError("Can't attack and move");
					m_attack = false;
				}
			}
			GUI.Box(new Rect(0, 75, 200, 50), "Actions Points: " + m_actionsPoints);
			GUILayout.EndArea();
		}
	}

	void HighlightAdjacentTiles() 
	{
		if (!m_move  && !m_attack) return;
		if (Depleted) return;

		neighbors = new List<Tile>();
		neighbors.Add(PlayField.GetTile(new Vector2(Coords.x - 1, Coords.y)));
		neighbors.Add(PlayField.GetTile(new Vector2(Coords.x + 1, Coords.y)));
		neighbors.Add(PlayField.GetTile(new Vector2(Coords.x, Coords.y - 1)));
		neighbors.Add(PlayField.GetTile(new Vector2(Coords.x, Coords.y + 1)));
		neighbors.Add(PlayField.GetTile(new Vector2(Coords.x - 1, Coords.y - 1)));
		neighbors.Add(PlayField.GetTile(new Vector2(Coords.x - 1, Coords.y + 1)));
		neighbors.Add(PlayField.GetTile(new Vector2(Coords.x + 1, Coords.y - 1)));
		neighbors.Add(PlayField.GetTile(new Vector2(Coords.x + 1, Coords.y + 1)));
		for (int i = 0; i < neighbors.Count; i++) {
			if (neighbors[i] != null) {
				PlayField.HighlightObjects[i].SetActive(true);
				PlayField.HighlightObjects[i].GetComponent<HighlightedTile>().SetPosition(neighbors[i].Coords);
			}
		}
	}

	void Move() 
	{
		Tile moveTo = InputManager.GetTileClicked();
		if (moveTo != null && neighbors.Contains(moveTo)) {
			if (UseActionPoints(1)) {
				SetPosition(moveTo.Coords);
				m_move = false;
			}
		}
	}

	/// <summary>
	/// This does not acutally do the damage, it just sets which enemy this character will attack once the turn is processed.
	/// </summary>
	void Attack()
	{
		Enemy toAttack = InputManager.GetEnemyClicked();
		if (toAttack != null) {
			Debug.Log((toAttack.Coords - this.Coords).magnitude);
			if (Mathf.Abs( (toAttack.Coords - this.Coords).magnitude) < m_range) {
				if (UseActionPoints(m_attackCost)) {
					targeted = toAttack;
				}
			}
		}
	}

	void UndoAttack() {
		if(targeted != null) {
			targeted = null;
			m_actionsPoints += m_attackCost;
		}
	}

	public void ResetActionPoints() 
	{
		m_actionsPoints = MAX_APS;
		Depleted = false;
	}

	public bool UseActionPoints(int points)
	{
		if (m_actionsPoints - points < 0) return false; //points not available;
		m_actionsPoints -= points;
		if (m_actionsPoints == 0) Depleted = true;
		return true;
	}

	public float GetRange() 
	{
		return m_range;
	}

	//Attribute getters and setters
	public int GetPower() 
	{
		return m_power;
	}

	public void SetPower(int newPower)
	{
		m_power = newPower;
	}
}

public enum Speed {
	slow,
	normal,
	fast
}
