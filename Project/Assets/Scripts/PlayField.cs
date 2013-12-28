using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayField : MonoBehaviour {

	public static PlayField m_instance;
    public GameObject m_defaultTile;
	public GameObject m_selector;
	public GameObject m_selectedTile;
    public GameObject m_spawner;

	private GameObject m_hoverMarker;
	private GameObject m_hovered;
	private GameObject m_selected;
	private GameObject m_selectedCharacter;


	private Color m_color = Color.white;
	public LayerMask m_characterMask;
	public Dictionary<Vector2, GameObject> m_playField;
	public Dictionary<GameObject, Vector2> m_objCoords;

	void Start () 
    {
		if (m_instance == null) {
			m_instance = this;
		}

        Vector3 pos = m_spawner.transform.position;

		GameObject tiles = new GameObject ("Tiles");

		m_playField = new Dictionary<Vector2, GameObject>();
		m_objCoords = new Dictionary<GameObject, Vector2>();

        for (int i = 0; i < 16; i++) {
            pos.z += m_defaultTile.renderer.bounds.size.z;
            for (int j = 0; j < 16; j++) {
                pos.x += m_defaultTile.renderer.bounds.size.x;
                
                GameObject tile = (GameObject)GameObject.Instantiate(m_defaultTile);
                tile.transform.position = pos;
				tile.transform.parent = tiles.transform;
				m_playField.Add(new Vector2(i, j), tile);
				m_objCoords.Add (tile, new Vector2(i, j));
				tile.GetComponent<Tile>().Coords = new Vector2(i, j);
            }
            pos.x -= m_defaultTile.renderer.bounds.size.x * 16;
        }

		m_hoverMarker = (GameObject)GameObject.Instantiate(m_selector);
		m_selectedTile = (GameObject)GameObject.Instantiate(m_selectedTile);
	}
	
	public static Vector3 GetWorldPosition(Vector2 coords) 
	{
		if (m_instance.m_playField.ContainsKey(coords)) {
			return m_instance.m_playField[coords].transform.position;
		} else { 
			return Vector3.zero;
		}
	}

	public static Vector2 GetObjectCoordinates(GameObject gameObject) 
	{
		if (m_instance.m_objCoords.ContainsKey(gameObject)) {
			return m_instance.m_objCoords[gameObject];
		} else {
			return Vector2.zero;
		}
	}

	public static Tile GetTile(Vector2 coords) 
	{
		if (m_instance.m_playField.ContainsKey(coords)) {
			return m_instance.m_playField[coords].GetComponent<Tile>();
		} else {
			return null;
		}
	}
}

