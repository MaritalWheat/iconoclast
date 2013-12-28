using UnityEngine;
using System.Collections;

public class Moveable : MonoBehaviour {

	public Vector2 Coords {
		get;
		set;
	}

	public void SetPosition(Vector2 coords) 
	{
		Vector3 pos = PlayField.GetWorldPosition(coords);
		pos.y += 0.25f;
		transform.position = pos;
		Coords = coords;
	}

}
