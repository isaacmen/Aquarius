using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TargetedAction : Action {
	protected Tile target;

	public abstract int minRange();
	public abstract int maxRange();

	override public void setActive() {
		Debug.Log("Don't use setActive(), use setActiveTargeting(Tile)");
	}

	public void setActiveTargeting(Tile t) {
		if(!active) {
			target = t;
			active = true;
			innerStart();
		} else {
			Debug.Log(this.GetType() + " set active when already active.");
		}
	}

	override protected void innerLoop() {

	}

	override protected void innerEnd() { }

	public Tile getOptimalTile() {
		List<Tile> tiles = Field.tilesInCharacterRange(this.GetComponentInParent<Character>(), minRange(), maxRange(), false);
		Tile optimalTile = tiles[0];
		int highestValue = 0;

		foreach(Tile t in tiles)
			if(t.getCharacter() != null) {
				int thisValue = GetComponentInParent<Character>().basicAttackDamage;
				if(thisValue > t.getCharacter().health)
					thisValue = Mathf.Max(2 * thisValue, thisValue + 15);
				if(thisValue > highestValue) {
					highestValue = thisValue;
					optimalTile = t;
				}
			}


		return optimalTile;
	}
}