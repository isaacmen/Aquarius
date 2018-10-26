using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action : MonoBehaviour {
	protected abstract ActionType getType();
	protected abstract void innerStart();
	protected abstract void innerLoop();
	protected abstract void innerEnd();

	private bool active;
	private bool lastActive;

	public bool isActive() {
		return active;
	}

	// returns if active was changed
	public bool setActive(bool to) {
		if(to == active) {
			return false;
		} else {
			active = to;
			if(active)
				innerStart();
			else
				innerEnd();
			return true;
		}
	}

	protected virtual void Start() {
		active = false;
		lastActive = false;
	}

	protected virtual void Update() {
		if(active != lastActive)
			lastActive = active;

		if(active)
			innerLoop();
	}
}
