using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Libra
public class LayOnHands : PromptingAction {
	override public ActionType getActionType() {
		return ActionType.ABILITY;
	}

	override public int maxUses() {
		return Constants.getInstance().layOnHands_maxUses;
	}

	override protected int minRange() { return Constants.getInstance().layOnHands_minRange; }
	override protected int maxRange() { return Constants.getInstance().layOnHands_maxRange; }

	override protected bool validTileToShow(Tile t) {
		return true;
	}

	protected override bool validTileToClick(Tile t) {
		return t.getCharacter() != null;
	}

	override protected bool targetYourField() { return true; }

	override protected void postPromptStart() {
		GetComponentInChildren<Animator>().Play("Attacking");
	}

	override protected void postPromptLoop() {
		if(GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Idle")) {
			target.getCharacter().takeDamage(-Constants.getInstance().layOnHands_healValue);
			setInactiveWithCompletion(true);
		}
	}

	override protected void innerEnd() { }

	override public int getValue() {
		return 0;
	}

	public override string getName()
    {
        return "Lay On Hands";
    }

    public override string getDescription()
    {
        return "Single Target\n" +
            "Heals: " + Constants.getInstance().layOnHands_healValue + "\n" +
            "Uses: " + maxUses() + "\n" +
            "Range: 1";
    }
}
