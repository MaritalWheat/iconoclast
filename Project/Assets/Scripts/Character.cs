using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

	public GameObject m_tile;

	private Vector2 m_pos;
	private bool m_move = false;
	private bool m_two = false;

	public bool Selected {
		set;
		get;
	}


	void Start() {
		//GameObject tile = (GameObject)GameObject.Instantiate(m_tile);
	}

	void Update() {
		/*Vector3 pos = PlayField.GetWorldPosition(new Vector2(0.0f, 0.0f));
		pos.y += 0.25f;
		transform.position = pos;*/

		if (m_move) {
			GameObject moveTo = InputManager.GetTileClicked();
			if (moveTo != null) {
				Vector3 pos = moveTo.transform.position;
				pos.y += 0.25f;
				transform.position = pos;
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
