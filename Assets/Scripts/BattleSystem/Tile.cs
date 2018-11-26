using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile {
	Grid_Behavior grid = new Grid_Behavior();
	private List<Vector3> Tiles = new List<Vector3>();
	private Vector3 positon;
	private bool onTile;
	public Tile(Vector3 tilePos){
		this.positon = tilePos;
		this.Tiles = surroundingTiles(positon);
		this.onTile = isCharatcerOnTile(this.positon);


	}

	//returns whether a character is on the tile
	public bool isCharatcerOnTile(Vector3 pos){
		if (grid.getPlayerPositions().Contains(positon) == true){
			Debug.Log("Someone is on me" + positon);
			return true;
		}
		Debug.Log("No one is on me" + positon);
		return false;

	}
	// creates a list of neighboring tiles
	public List<Vector3> surroundingTiles(Vector3 positon){
		List<Vector3> Tiles = new List<Vector3>();
		Tiles.Add(positon + new Vector3(0,2,0));
		Tiles.Add(positon + new Vector3(0,-2,0));
		Tiles.Add(positon + new Vector3(2,0,0));
		Tiles.Add(positon + new Vector3(-2,0,0));
		return Tiles;
	}

}
