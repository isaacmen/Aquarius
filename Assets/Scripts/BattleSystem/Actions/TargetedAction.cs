using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TargetedAction : Action {
	protected Tile target;

	protected abstract int minRange();
	protected abstract int maxRange();
	protected abstract bool targetYourField();

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

	public abstract List<Tile> getOptimalTiles();
}