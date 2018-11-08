using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pass : Action {
	override public ActionType getActionType() {
		return ActionType.PASS;
	}

	override protected void innerStart() {
		setInactiveWithCompletion(true);
	}

	override protected void innerLoop() {}
	override protected void innerEnd() {}
}
