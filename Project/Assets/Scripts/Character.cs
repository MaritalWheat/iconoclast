using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Character : Moveable {

	public GameObject m_tile;
	List<Tile> neighbors;

	private bool m_move = false;
	private bool m_two = false;
	private int m_actionsPoints;
	private const int MAX_APS = 2;

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
				neighbors[i].gameObject.renderer.material.SetColor("_Color", Color.white);
			}
		}

		HighlightAdjacentTiles();

		if (m_move) {
			Move ();
		}
	}

	void OnGUI() {
		if (Selected) {
			GUI.Box(new Rect(0, 0, 200, 200), "");
			GUILayout.BeginArea(new Rect(0, 0, 200, 200));
			m_move = GUI.Toggle(new Rect(0, 0, 200, 50), m_move,  "Move");
			GUI.Box(new Rect(0, 75, 200, 50), "Actions Points: " + m_actionsPoints);
			GUILayout.EndArea();
		}
	}

	void HighlightAdjacentTiles() 
	{
		if (!m_move) return;
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
				neighbors[i].gameObject.renderer.material.SetColor("_Color", Color.yellow);
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

	public bool UseActionPoints(int points)
	{
		if (m_actionsPoints - points < 0) return false; //points not available;
		m_actionsPoints -= points;
		if (m_actionsPoints == 0) Depleted = true;
		return true;
	}


}
