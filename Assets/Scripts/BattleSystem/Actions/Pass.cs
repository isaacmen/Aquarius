using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pass : Action {
	override public ActionType getActionType() {
		return ActionType.PASS;
	}

	public override int maxUses() {
		return 9999999;
	}

	override protected void innerStart() {
		setInactiveWithCompletion(true);
	}

	override protected void innerLoop() {}
	override protected void innerEnd() {}
}
