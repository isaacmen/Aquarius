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
//		inRange = GameLoop.getInstance().getAllyField().getInRange(, minRange(), maxRange());
	}

	override protected void innerLoop() {
		if(!prompted) {

		}
	}

	protected abstract int minRange();
	protected abstract int maxRange();
	protected abstract void innerInnerStart();
	protected abstract void innerInnerLoop();
	protected abstract void innerInnerEnd();

	override protected void innerEnd() { }
}
