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

	void Start () 
    {
		if (m_instance == null) {
			m_instance = this;
		}

        Vector3 pos = m_spawner.transform.position;

		m_playField = new Dictionary<Vector2, GameObject>();
        for (int i = 0; i < 16; i++) {
            pos.z += m_defaultTile.renderer.bounds.size.z;
            for (int j = 0; j < 16; j++) {
                pos.x += m_defaultTile.renderer.bounds.size.x;
                
                GameObject tile = (GameObject)GameObject.Instantiate(m_defaultTile);
                tile.transform.position = pos;
				m_playField.Add(new Vector2(i, j), tile);
            }
            pos.x -= m_defaultTile.renderer.bounds.size.x * 16;
        }

		m_hoverMarker = (GameObject)GameObject.Instantiate(m_selector);
		m_selectedTile = (GameObject)GameObject.Instantiate(m_selectedTile);

		foreach (Vector2 key in m_playField.Keys) {
			Debug.Log("Key: " + key + ", Value: " + m_instance.m_playField[key]);

		}
	}

    void Update()
    {
	    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, 10000)) {
            //Debug.Log("Hit: " + hit.transform.gameObject.ToString());
			GameObject hitObj = hit.transform.gameObject;
			if (Input.GetMouseButtonDown(0)) {
				m_selected = hitObj;
				Vector3 tilePos = m_selected.transform.position;
				tilePos.y += 0.1f;
				m_selectedTile.transform.position = tilePos;
			}
			if (hitObj != m_hovered) {
				m_hovered = hitObj;
				Vector3 tilePos = m_hovered.transform.position;
				tilePos.y += 0.1f;
				m_hoverMarker.transform.position = tilePos;
			}
        }

		if (Input.GetMouseButtonDown(0)) {
			if (Physics.Raycast(ray, out hit, 10000, m_characterMask)) {
				Debug.Log("Hit: " + hit.transform.gameObject.ToString());
				if (hit.transform.gameObject.GetComponent<Character>() != null) {
					if (m_selectedCharacter != null) {
						m_selectedCharacter.GetComponent<Character>().Selected = false; //unselect previously selected
					}
					m_selectedCharacter = hit.transform.gameObject;
				}
			} else {
				Debug.Log("Nothing hit on click");
			}
		} 

		if (m_selectedCharacter != null) {
			m_selectedCharacter.GetComponent<Character>().Selected = true;
		}
	}

	public static Vector3 GetWorldPosition(Vector2 coords) 
	{
		if (m_instance.m_playField.ContainsKey(coords)) {
			return m_instance.m_playField[coords].transform.position;
		} else { 
			return Vector3.zero;
		}
	}
}
