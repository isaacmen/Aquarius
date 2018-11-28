using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PromptingAction : Action {
	private bool prompted;
	private bool started;
	private List<Tile> inRange;
	private Tile tile;

	override protected void innerStart() {
		prompted = false;
		started = false;
		tile = null;
		Debug.Log("pick a tile");
		print(this);
		inRange = Field.tilesInCharacterRange(this.GetComponentInParent<Character>(), minRange(), maxRange(), targetYourField());
	}

	override protected void innerLoop() {
		if(!prompted) {
			//
		}
	}

	protected abstract int minRange();
	protected abstract int maxRange();
	protected abstract bool targetYourField();
	protected abstract void postPromptStart();
	protected abstract void postPromptLoop();
	protected abstract void postPromptEnd();

	override protected void innerEnd() {
		postPromptEnd();
	}
}
