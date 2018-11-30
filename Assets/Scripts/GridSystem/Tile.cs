using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
	private Field field;
	private PromptingAction action;

	[Header("Character")]
	public Character onTile;

	void Awake() {
		onTile = null;
		action = null;
	}

	void Update() {
		
	}

	public void setTargetableForPromptingAction(PromptingAction pa) {
		action = pa;
		GetComponent<SpriteRenderer>().sprite = field.tileClickableSprite;
	}

	public void setPromptDone() {
		action = null;
		GetComponent<SpriteRenderer>().sprite = field.tileRegularSprite;
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
