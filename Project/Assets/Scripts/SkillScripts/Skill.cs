using UnityEngine;
using System.Collections;

public class Skill : MonoBehaviour {

	public Texture2D m_skillIcon;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/// <summary>
	/// This funciton returns character so that the children of this class can call the base method and get the character in return. Also, thi way the base class do all the error handling.
	/// </summary>
	/// <returns>The skill.</returns>
	public virtual Character ApplySkill() {
		Character character = gameObject.GetComponent<Character> ();

		if (character == null) {
			Debug.LogError("Attempting to apply a skill to a gameobject with no character component");
			return null;
		} else {
			return character;
		}
	}
}
