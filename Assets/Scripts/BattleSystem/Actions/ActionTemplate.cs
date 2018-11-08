using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionTemplate : Action {
	override public ActionType getActionType() {
		throw new System.NotImplementedException();
	}

	override protected void innerStart() {

	}

	override protected void innerLoop() {

	}

	override protected void innerEnd() {}

	private enum MyState {

	}
}
