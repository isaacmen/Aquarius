using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Scorpio
public class Fireball : Action {
	private MyState state;
//	private Tile target;

	private const int CENTER_DAMAGE = 16;
	private const int EDGE_DAMAGE = 8;

	override public ActionType getActionType() {
		return ActionType.ABILITY;
	}

	public override int maxUses() {
		return Constants.getInstance().fireball_maxUses;
	}

	override protected void innerStart() {
		state = MyState.PROMPTING;
		// print to pick a tile
	}

	override protected void innerLoop() {
		switch(state) {
			case MyState.PROMPTING:
				// if(tile picked) {
				//					state = MyState.ACTING;
				//					target = getTileFromInput(inputCode);
				//				} else {
				//					setInactiveWithCompletion(false);
				//				}
				state = MyState.ACTING;
				break;
			case MyState.ACTING:

				setInactiveWithCompletion(true);
				break;
		}

	}

	override protected void innerEnd() {}

	private enum MyState {
		PROMPTING, ACTING
	}
}
