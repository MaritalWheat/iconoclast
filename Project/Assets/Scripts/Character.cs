using UnityEngine;
using System.Collections;

public class Character : Moveable {

	public GameObject m_tile;
	
	private bool m_move = false;
	private bool m_two = false;

	public bool Selected {
		set;
		get;
	}

	void Update() 
	{
		if (m_move) {
			Tile moveTo = InputManager.GetTileClicked();
			if (moveTo != null) {
				SetPosition(moveTo.Coords);
			}
		}
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
}
