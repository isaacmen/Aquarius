using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pass : Action {
	override protected void innerStart() {
		setInactiveWithCompletion(true);
	}

	override protected void innerLoop() {}
	override protected void innerEnd() {}
}
