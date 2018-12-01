using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
	private Field field;

	[Header("Character")]
	public Character onTile;

	void Awake() {
		onTile = null;
	}

	void Update() {
		
	}

	public void setClickableSprite() {
		GetComponent<SpriteRenderer>().sprite = field.tileClickableSprite;
	}

	public void setRegularSprite() {
		GetComponent<SpriteRenderer>().sprite = field.tileRegularSprite;
	}

	public void setSecondarySprite() {
		GetComponent<SpriteRenderer>().sprite = field.tileSecondarySprite;
	}

	public int[] getTileArrayCoordsYX() {
		return field.getTileArrayCoordsYX(this);
	}

	public void setField(Field f) {
		field = f;
	}

	public Field getField() {
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
