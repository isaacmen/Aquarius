using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile {
	private int width; // width is from the top going down, starting from 0
	private int depth; // depth is from the left going right, starting from 0
	private Character onTile;

	public Tile(int w, int d) {
		width = w;
		depth = d;
	}

	void Awake() {
		//whatever instantiations
	}

	public Character getCharacter() {
		return onTile;
	}

	public Character removeCharacter() {
		Character prevOnTile = onTile;
		onTile = null;
		return prevOnTile;
	}

	// Returns whether the character was successfully placed
	public bool placeCharacter(Character c) {
		if(onTile == null) {
			onTile = c;
			return true;
		} else {
			return false;
		}
	}
}
