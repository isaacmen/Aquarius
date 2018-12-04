using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Scorpio *
public class PowerWordKill2 : Action {
	private Tile target;

	override public ActionType getActionType() {
		return ActionType.NA;
	}

	override public int maxUses() {
		return Constants.getInstance().powerWordKill_maxUses;
	}

	public void setTarget(Tile t) {
		target = t;
	}

	override protected void innerStart() {
		Debug.Log("Power Word: Kill!");
		GetComponentInChildren<Animator>().Play("Attacking");
	}

	override protected void innerLoop() {
		if(GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Idle")) {
			if(target.getCharacter() != null)
				target.getCharacter().takeDamage(Constants.getInstance().powerWordKill_damage);
			else
				Debug.Log("no target");
			setInactiveWithCompletion(true);
		}
	}

	override protected void innerEnd() { }

	override public int getValue() {
		return 0;
	}
}
