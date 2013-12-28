using UnityEngine;
using System.Collections;

public class Selector : Moveable {

	private Tile m_hovered;

	void Update () {
		Tile tileUnderMouse = InputManager.GetTileHovered();
		if (tileUnderMouse != null && tileUnderMouse != m_hovered) {
			m_hovered = tileUnderMouse;
			SetPosition(tileUnderMouse.Coords);
		}
	}
}
