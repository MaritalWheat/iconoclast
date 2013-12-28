using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayField : MonoBehaviour {

	public static PlayField m_instance;
    public GameObject m_defaultTile;
	public GameObject m_doorTile;
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
	public List<Vector2> m_doorA;

	public GameObject m_highlightObject;
	private List<GameObject> m_highlightTiles;
	
	public static List<GameObject> HighlightObjects {
		get { return m_instance.m_highlightTiles; }
	}

	void Start () 
    {
		if (m_instance == null) {
			m_instance = this;
		}

		for (int i = 0; i < 4; i++) {
			m_doorA.Add(new Vector2(6+i,0));
		}

		m_highlightTiles = new List<GameObject>();
		for (int i = 0; i < 10; i++) {
			m_highlightTiles.Add((GameObject)GameObject.Instantiate(m_highlightObject));
			m_highlightTiles[i].SetActive(false);
		}


        Vector3 pos = m_spawner.transform.position;

		GameObject tiles = new GameObject ("Tiles");

		m_playField = new Dictionary<Vector2, GameObject>();
		m_objCoords = new Dictionary<GameObject, Vector2>();

		Vector2 newCoord;
        for (int i = 0; i < 16; i++) {
            pos.z += m_defaultTile.renderer.bounds.size.z;
            for (int j = 0; j < 16; j++) {
				newCoord = new Vector2(j, i);
                pos.x += m_defaultTile.renderer.bounds.size.x;
				GameObject tile;
				if (IsDoorTile(newCoord)) {
                	tile = (GameObject)GameObject.Instantiate(m_doorTile);
				} else {
					tile = (GameObject)GameObject.Instantiate(m_defaultTile);
				}
                tile.transform.position = pos;
				tile.transform.parent = tiles.transform;
				m_playField.Add(newCoord, tile);
				m_objCoords.Add (tile, newCoord);
				tile.GetComponent<Tile>().Coords = newCoord;
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

	public static Vector2 GetRandomDoorTile() {
		return m_instance.m_doorA[Random.Range(0,3)];
	}

	/// <summary>
	/// Determines if is door tile from the specified coords.
	/// </summary>
	/// <returns><c>true</c> if is door tile; otherwise, <c>false</c>.</returns>
	/// <param name="coords">Coords.</param>
	public static bool IsDoorTile(Vector2 coords) {
		return m_instance.m_doorA.Contains(coords);
	}
}

