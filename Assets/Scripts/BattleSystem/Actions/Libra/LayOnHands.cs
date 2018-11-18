using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Libra
public class LayOnHands : Action {
	override public ActionType getActionType() {
		return ActionType.ABILITY;
	}

	override protected void innerStart() {
		setInactiveWithCompletion(true);
	}

	override protected void innerLoop() {

	}

	override protected void innerEnd() { }

	private enum MyState {

	}
}
