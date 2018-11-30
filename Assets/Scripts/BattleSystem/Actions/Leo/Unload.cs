using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Leo
public class Unload : PromptingAction {

	override public ActionType getActionType() {
		return ActionType.ABILITY;
	}

	override public int maxUses() {
		return Constants.getInstance().unload_maxUses;
	}

	override protected int minRange() { return Constants.getInstance().unload_minRange; }
	override protected int maxRange() { return Constants.getInstance().unload_maxRange; }

	override protected bool validTileToShow(Tile t) {
		return true;
	}

	protected override bool validTileToClick(Tile t) {
		return t.getCharacter() != null;
	}

	override protected bool targetYourField() { return false; }

	override protected void postPromptStart() {
		GetComponentInParent<Animator>().Play("Attacking");
	}

	override protected void postPromptLoop() {
		if(GetComponentInParent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Idle")) {
			getTarget().getCharacter().takeDamage(Constants.getInstance().unload_damage);
			setInactiveWithCompletion(true);
		}
	}

	override protected void innerEnd() { }
}
