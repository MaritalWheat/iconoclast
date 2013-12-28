using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Character : Moveable {

	public GameObject m_tile;
	List<Tile> neighbors;

	private bool m_move = false;
	private bool m_two = false;

	public bool InPlay {
		get;
		set;
	}

	public bool Selected {
		set;
		get;
	}

	void Start() 
	{
		neighbors = new List<Tile>();
	}

	void Update() 
	{
		for (int i = 0; i < neighbors.Count; i++) {
			if (neighbors[i] != null) {
				neighbors[i].gameObject.renderer.material.SetColor("_Color", Color.white);
			}
		}

		if (m_move) {
			Tile moveTo = InputManager.GetTileClicked();
			if (moveTo != null) {
				SetPosition(moveTo.Coords);
			}
		}
		HighlightAdjacentTiles();
	}

	void OnGUI() {
		if (Selected) {
			GUI.Box(new Rect(0, 0, 200, 200), "");
			GUILayout.BeginArea(new Rect(0, 0, 200, 200));
			m_move = GUI.Toggle(new Rect(0, 0, 200, 50), m_move,  "Move");
			m_two = GUI.Toggle(new Rect(0, 75, 200, 50), m_two, "");
			GUILayout.EndArea();
		}
	}

	void HighlightAdjacentTiles() 
	{
		if (!m_move) return;

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
}
