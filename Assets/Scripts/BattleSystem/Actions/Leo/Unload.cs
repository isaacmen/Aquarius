using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unload : Action {
	override protected void innerStart() {
		setInactiveWithCompletion(true);
	}

	override protected void innerLoop() {

	}

	override protected void innerEnd() {}

	private enum MyState {

	}
}
