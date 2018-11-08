using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action : MonoBehaviour {
	protected abstract void innerStart();
	protected abstract void innerLoop();
	protected abstract void innerEnd();

	private bool active;
	private bool lastActive;
	private bool complete;

	public bool isActive() {
		return active;
	}
	
	public void setActive() {
		if(!active) {
			active = true;
			innerStart();
		} else {
			Debug.Log(this.GetType() + " set active when already inactive.");
		}
	}

	// parameter indicates if the action was completed
	public void setInactiveWithCompletion(bool c) {
		if(active) {
			active = false;
			complete = c;
			innerEnd();
		} else {
			Debug.Log(this.GetType() + " set inactive when already active.");
		}
	}

	public bool getCompletion() {
		return complete;
	}

	protected virtual void Start() {
		active = false;
		lastActive = false;
		complete = false;
	}

	protected virtual void Update() {
		if(active != lastActive)
			lastActive = active;

		if(active)
			innerLoop();
	}
}
