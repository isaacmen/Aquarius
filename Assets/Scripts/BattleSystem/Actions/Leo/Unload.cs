using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Leo
public class Unload : Action {
	override public ActionType getActionType() {
		return ActionType.ABILITY;
	}

	public override int maxUses() {
		return Constants.getInstance().unload_maxUses;
	}

	override protected void innerStart() {
		setInactiveWithCompletion(true);
	}

	override protected void innerLoop() {

	}

	override protected void innerEnd() {}

	private enum MyState {

	}
}
