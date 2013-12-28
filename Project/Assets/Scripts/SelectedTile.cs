using UnityEngine;
using System.Collections;

public class SelectedTile : Moveable {

	private Tile m_selected;

	void Update () {
		Tile tileUnderMouse = InputManager.GetTileClicked();
		if (tileUnderMouse != null && tileUnderMouse != m_selected) {
			m_selected = tileUnderMouse;
			SetPosition(tileUnderMouse.Coords);
		}
	}
}