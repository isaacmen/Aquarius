﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionTemplate : Action {
	override public ActionType getActionType() {
		throw new System.NotImplementedException();
	}

	override public int maxUses() {
		throw new System.NotImplementedException();
	}

	override protected void innerStart() {

	}

	override protected void innerLoop() {

	}

	override protected void innerEnd() {}

	override public int getValue() {
		return 0;
	}

	private enum MyState {

	}
}
