using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetedBasicAttack : TargetedAction {
	private bool firstCheck;

	override public ActionType getActionType() {
		return ActionType.ABILITY;
	}

	override public int minRange() { return GetComponentInParent<Character>().basicAttackMinRange; }
	override public int maxRange() { return GetComponentInParent<Character>().basicAttackMaxRange; }

	override public int maxUses() {
		return 9999999;
	}

	override protected void innerStart() {
		firstCheck = true;
		GetComponentInChildren<Animator>().Play("Attacking");
	}

	override protected void innerLoop() {
		if(GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Idle")) {
			if(firstCheck) {
				firstCheck = false;
			} else {
				target.getCharacter().takeDamage(GetComponentInParent<Character>().basicAttackDamage);
				setInactiveWithCompletion(true);
			}
		}
	}

	override protected void innerEnd() { }

	override public int getValue() {
		List<Tile> tiles = Field.tilesInCharacterRange(this.GetComponentInParent<Character>(), minRange(), maxRange(), false);
		int value = 0;

		foreach(Tile t in tiles)
			if(t.getCharacter() != null) {
				int thisValue = GetComponentInParent<Character>().basicAttackDamage;
				if(thisValue > t.getCharacter().health)
					thisValue = Mathf.Max(2 * thisValue, thisValue + 15);
				if(thisValue > value)
					value = thisValue;
			}


		return value;
	}
}
