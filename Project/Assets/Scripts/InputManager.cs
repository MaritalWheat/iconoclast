using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

	public static InputManager m_instance;

	void Start() 
	{
		if (m_instance == null) {
			m_instance = this;
		}
	}
	
	void Update()
	{
	}

	public static GameObject GetTileClicked() 
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		GameObject hitObj = null;
		if (Physics.Raycast(ray, out hit, 10000)) {
			//Debug.Log("Hit: " + hit.transform.gameObject.ToString());
			if (Input.GetMouseButtonDown(0)) {
				hitObj = hit.transform.gameObject;
			}
		}

		return hitObj;
	}

}
