using UnityEngine;
using System.Collections;

public class PlayField : MonoBehaviour {

    public GameObject m_defaultTile;
    public GameObject m_spawner;

	void Start () 
    {
        Vector3 pos = m_spawner.transform.position;
        for (int i = 0; i < 16; i++) {
            pos.z += m_defaultTile.renderer.bounds.size.z;
            for (int j = 0; j < 16; j++) {
                pos.x += m_defaultTile.renderer.bounds.size.x;
                
                GameObject tile = (GameObject)GameObject.Instantiate(m_defaultTile);
                tile.transform.position = pos;

            }
            pos.x -= m_defaultTile.renderer.bounds.size.x * 16;
        }
	}

    void Update()
    {
	    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, 10000)) {
            Debug.Log("Hit: " + hit.transform.gameObject.ToString());
        }
	}
}
