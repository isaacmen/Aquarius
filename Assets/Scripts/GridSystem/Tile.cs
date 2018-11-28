using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
	private Grid_Behavior field;

	[Header("Character")]
	public Character onTile;

	void Awake() {
		onTile = null;
	}

	public void setField(Grid_Behavior f) {
		field = f;
	}

	public Grid_Behavior getField() {
		return field;
	}

	public void removeCharacter() {
		placeCharacter(null);
	}

	public void placeCharacter(Character c) {
		onTile = c;
	}
	
	public Character getCharacter() {
		return onTile;
	}
}
