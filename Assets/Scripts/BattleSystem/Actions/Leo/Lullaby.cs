using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lullaby : PromptingAction {
	override public ActionType getActionType() {
		return ActionType.ABILITY;
	}

	override public int maxUses() {
		return Constants.getInstance().lullaby_maxUses;
	}

	override protected int minRange() { return Constants.getInstance().lullaby_minRange; }
	override protected int maxRange() { return Constants.getInstance().lullaby_maxRange; }

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
			GameLoop.getInstance().statusManager.addStatusEffect(this.GetComponentInParent<Character>(), new List<Character>() { target.getCharacter() }, StatusEffect.STUNNED);
			setInactiveWithCompletion(true);
		}
	}

	override protected void innerEnd() { }

	override public int getValue() {
		return 0;
	}

	public override string getDescription()
    {
        return "Single Target, Stuns\n" +
            "Damage: 0\n" +
            "Uses: " + maxUses() + "\n" +
            "Range: " + minRange() + "-" + maxRange();
    }
}
