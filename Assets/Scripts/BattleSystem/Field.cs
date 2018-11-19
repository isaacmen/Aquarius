using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field {
	private const int WIDTH = 3;
	private const int DEPTH = 3;
	private const int NUM_CHARS = 3;

	private Tile[,] positions;
	private List<Character> characters;

	public Field() {
		if(GameLoop.getInstance().DEBUG_LOG) Debug.Log("field created");
		positions = new Tile[WIDTH, DEPTH];
		for(int d = 0; d < DEPTH; d++)
			for(int w = 0; w < WIDTH; w++)
				positions[w, d] = new Tile(w, d);
		characters = new List<Character>();
	}

	public bool addCharacter(Character c) {
		if(characters.Contains(c)) {
			return false;
		} else {
			characters.Add(c);
			for(int i = 0; i < DEPTH; i++)
				if(positions[WIDTH / 2, i].placeCharacter(c))
					break;
			
			return true;
		}
	}

	public List<Tile> getInRange(Character c, int minRange, int maxRange) {
		return null;
	}

	public void printField() {
		string toPrint = "";
		for(int d = 0; d < DEPTH; d++) {
			for(int w = 0; w < WIDTH; w++) {
				if(positions[w, d].getCharacter() == null) {
					toPrint += "X ";
				} else {
					toPrint += positions[w, d].getCharacter().name + " ";
				}
				
			}
			if(d < DEPTH-1)
				toPrint += " / ";
		}

		Debug.Log(toPrint);
	}
}
