using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action : MonoBehaviour {
	protected abstract void innerStart();
	protected abstract void innerLoop();
	protected abstract void innerEnd();
	public abstract int maxUses();
	public abstract ActionType getActionType();

	private bool active;
	private bool complete;
	private int remainingUses;

	public bool isActive() {
		return active;
	}
	
	public void setActive() {
		if(!active) {
			active = true;
			innerStart();
		} else {
			Debug.Log(this.GetType() + " set active when already active.");
		}
	}

	// parameter indicates if the action was completed (as in fully executed)
	// put false if the user cancels the action or the action target is invalid
	public void setInactiveWithCompletion(bool c) {
		if(active) {
			active = false;
			complete = c;
			if(complete)
				remainingUses -= 1;
			innerEnd();
		} else {
			Debug.Log(this.GetType() + " set inactive when already inactive.");
		}
	}

	public bool getCompletion() {
		return complete;
	}

	public int usesLeft() {
		return remainingUses;
	}

	protected virtual void Start() {
		active = false;
		complete = false;
		remainingUses = maxUses();
	}

	protected virtual void Update() {
		if(active) innerLoop();
	}
}

public enum ActionType {
	PASS, MOVE, ABILITY
}