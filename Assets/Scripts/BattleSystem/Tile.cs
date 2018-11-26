using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
	[Header("Character")]
	public Character onTile;

	void Awake() {
		onTile = null;
	}

	public void placeCharacter(Character c) {
		onTile = c;
	}
/*
	//returns whether a character is on the tile
	public bool isCharacterOnTile(Vector3 pos){
		if (grid.getPlayerPositions().Contains(position) == true){
			Debug.Log("Someone is on me" + position);
			return true;
		}
		Debug.Log("No one is on me" + position);
		return false;

	}
	
	// creates a list of neighboring tiles
	public List<Vector3> surroundingTiles(){
		List<Vector3> Tiles = new List<Vector3>();
		Tiles.Add(positon + new Vector3(0,2,0));
		Tiles.Add(positon + new Vector3(0,-2,0));
		Tiles.Add(positon + new Vector3(2,0,0));
		Tiles.Add(positon + new Vector3(-2,0,0));
		return Tiles;
	}*/
}
