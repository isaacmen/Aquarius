using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : PromptingAction {
	
	override public ActionType getActionType() {
		return ActionType.ABILITY;
	}

	override public int maxUses() {
		return 9999999;
	}

	override protected int minRange() { return GetComponentInParent<Character>().basicAttackMinRange; }
	override protected int maxRange() { return GetComponentInParent<Character>().basicAttackMaxRange; }

	override protected bool validTileToShow(Tile t) {
		return true;
	}

	protected override bool validTileToClick(Tile t) {
		return t.getCharacter() != null;
	}

	override protected bool targetYourField() { return false; }

	override protected void postPromptStart() {
		GetComponentInChildren<Animator>().Play("Attacking");
	}

	override protected void postPromptLoop() {
		if(GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Idle")) {
			getTarget().getCharacter().takeDamage(GetComponentInParent<Character>().basicAttackDamage);
			setInactiveWithCompletion(true);
		}
	}

	override protected void innerEnd() { }
}
