using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Character : Moveable {

	public GameObject m_tile;
	List<Tile> neighbors;

	private bool m_move = false;
	private bool m_attack = false;
	private bool m_two = false;
	private int m_actionsPoints;
	private const int MAX_APS = 2;

	public bool InPlay {
		get;
		set;
	}

    public bool Moving
    {
        get { 
            return m_move;
        }

        set
        {
            if (value)
            {
                HighlightAdjacentTiles();
            }
            else
            {
                ClearHighlightedTiles();
            }
            m_move = value;
        }
    }

	public bool Selected {
		set;
		get;
	}

	public bool Depleted {
		set;
		get;
	}

	void Start() 
	{
		neighbors = new List<Tile>();
		m_actionsPoints = MAX_APS;
	}

	void Update() 
	{
		//HighlightAdjacentTiles();

		if (m_move) {
			Move ();
		}

		if (m_attack) {
			Attack();
		}

	}

	void OnGUI() {
		if (Selected) {
			GUI.Box(new Rect(0, 0, 200, 200), "");
			GUILayout.BeginArea(new Rect(0, 0, 200, 200));
			Moving = GUI.Toggle(new Rect(0, 0, 200, 50), Moving,  "Move");
			m_attack = GUI.Toggle(new Rect(0, 50, 200, 50), m_attack,  "Attack");
			if (Moving) {
				if (m_attack) {
					Debug.LogError("Can't attack and move");
					m_attack = false;
				}
			}
			GUI.Box(new Rect(0, 75, 200, 50), "Actions Points: " + m_actionsPoints);
			GUILayout.EndArea();
		}
	}

    void ClearHighlightedTiles()
    {
        for (int i = 0; i < neighbors.Count; i++)
        {
            if (neighbors[i] != null)
            {
                PlayField.HighlightObjects[i].SetActive(false);
            }
        }
    }

	void HighlightAdjacentTiles() 
	{
        ClearHighlightedTiles();
		//if (!m_move  && !m_attack) return;
		if (Depleted) return;

		neighbors = new List<Tile>();
		neighbors.Add(PlayField.GetTile(new Vector2(Coords.x - 1, Coords.y)));
		neighbors.Add(PlayField.GetTile(new Vector2(Coords.x + 1, Coords.y)));
		neighbors.Add(PlayField.GetTile(new Vector2(Coords.x, Coords.y - 1)));
		neighbors.Add(PlayField.GetTile(new Vector2(Coords.x, Coords.y + 1)));
		neighbors.Add(PlayField.GetTile(new Vector2(Coords.x - 1, Coords.y - 1)));
		neighbors.Add(PlayField.GetTile(new Vector2(Coords.x - 1, Coords.y + 1)));
		neighbors.Add(PlayField.GetTile(new Vector2(Coords.x + 1, Coords.y - 1)));
		neighbors.Add(PlayField.GetTile(new Vector2(Coords.x + 1, Coords.y + 1)));

		for (int i = 0; i < neighbors.Count; i++) {
			if (neighbors[i] != null) {
				PlayField.HighlightObjects[i].SetActive(true);
				PlayField.HighlightObjects[i].GetComponent<HighlightedTile>().SetPosition(neighbors[i].Coords);
			}
		}
	}

	void Move() 
	{
		Tile moveTo = InputManager.GetTileClicked();
		if (moveTo != null && neighbors.Contains(moveTo)) {
			if (UseActionPoints(1)) {
				SetPosition(moveTo.Coords);
				m_move = false;
			}
		}
	}

	void Attack()
	{
		Enemy toAttack = InputManager.GetEnemyClicked();
		if (toAttack != null) {
			if (UseActionPoints(1)) {
				toAttack.SetPosition(new Vector2(0,0));
			}
		}
	}



	public void ResetActionPoints() 
	{
		m_actionsPoints = MAX_APS;
		Depleted = false;
	}

	public bool UseActionPoints(int points)
	{
		if (m_actionsPoints - points < 0) return false; //points not available;
		m_actionsPoints -= points;
		if (m_actionsPoints == 0) Depleted = true;
		return true;
	}
}
