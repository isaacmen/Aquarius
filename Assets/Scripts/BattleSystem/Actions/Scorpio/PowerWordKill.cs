using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Scorpio
public class PowerWordKill : PromptingAction {
	override public ActionType getActionType() {
		return ActionType.ABILITY;
	}

	override public int maxUses() {
		return Constants.getInstance().powerWordKill_maxUses;
	}

	override protected int minRange() { return Constants.getInstance().powerWordKill_minRange; }
	override protected int maxRange() { return Constants.getInstance().powerWordKill_maxRange; }

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
			GameLoop.getInstance().delayedActionManager.addAction(GetComponentInParent<Character>(), 1, GetComponentInParent<PowerWordKill2>());
			GetComponentInParent<PowerWordKill2>().setTarget(target);
			Debug.Log("Power Word: Kill charging");
			setInactiveWithCompletion(true);
		}
	}

	override protected void innerEnd() { }

    public override string getName()
    {
        return "Power Word Kill";
    }

    public override string getDescription()
    {
        return "Single Target\n" +
            "Damage: " + Constants.getInstance().powerWordKill_damage + " on next turn\n" +
            "Uses: " + usesLeft() + "\n" +
            "Range: " + minRange() + "-" + maxRange();
    }
}
