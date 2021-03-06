﻿using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

	public static InputManager m_instance;
	public LayerMask m_tileLayer;
	public LayerMask m_characterLayer;
	public LayerMask m_enemyLayer;

	void Start() 
	{
		if (m_instance == null) {
			m_instance = this;
		}
	}

	public static Tile GetTileClicked() 
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		GameObject hitObj = null;
		if (Physics.Raycast(ray, out hit, 10000, m_instance.m_tileLayer)) {
			//Debug.Log("Hit: " + hit.transform.gameObject.ToString());
			if (Input.GetMouseButtonDown(0)) {
				hitObj = hit.transform.gameObject;
			}
		}

		if (hitObj == null) return null;
		return hitObj.GetComponent<Tile>();
	}

	public static Tile GetTileHovered() 
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		
		GameObject hitObj = null;
		if (Physics.Raycast(ray, out hit, 10000, m_instance.m_tileLayer)) {
			hitObj = hit.transform.gameObject;
		}
		
		if (hitObj == null) return null;
		return hitObj.GetComponent<Tile>();
	}

	public static Character GetCharacterClicked()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		
		GameObject hitObj = null;
		if (Physics.Raycast(ray, out hit, 10000, m_instance.m_characterLayer)) {
			if (Input.GetMouseButtonDown(0)) {	
				hitObj = hit.transform.gameObject;
			}
		}
			
		if (hitObj == null) return null;
		return hitObj.GetComponent<Character>();
	}

	public static Enemy GetEnemyClicked()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		
		GameObject hitObj = null;
		if (Physics.Raycast(ray, out hit, 10000, m_instance.m_enemyLayer)) {
			if (Input.GetMouseButtonDown(0)) {	
				hitObj = hit.transform.gameObject;
			}
		}
		
		if (hitObj == null) return null;
		return hitObj.GetComponent<Enemy>();
	}
}
