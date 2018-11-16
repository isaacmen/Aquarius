using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action : MonoBehaviour {
	protected abstract void innerStart();
	protected abstract void innerLoop();
	protected abstract void innerEnd();
	public abstract ActionType getActionType();

	private bool active;
	private bool complete;

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
			innerEnd();
		} else {
			Debug.Log(this.GetType() + " set inactive when already inactive.");
		}
	}

	public bool getCompletion() {
		return complete;
	}

	protected virtual void Start() {
		active = false;
		complete = false;
	}

	protected virtual void Update() {
		if(active) innerLoop();
	}
}

public enum ActionType {
	PASS, MOVE, ABILITY
}