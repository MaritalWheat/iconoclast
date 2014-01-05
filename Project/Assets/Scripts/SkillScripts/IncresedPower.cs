using UnityEngine;
using System.Collections;

public class IncresedPower : Skill {

	public const int INCREASE_VALUE = 5;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public Character ApplySkill() {
		Character character = base.ApplySkill ();
		if (character != null) {
			character.SetPower(character.GetPower() + INCREASE_VALUE);
		} 
		return character;
	}
}
