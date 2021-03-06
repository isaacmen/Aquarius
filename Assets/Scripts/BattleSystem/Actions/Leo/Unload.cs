﻿using System.Collections;
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
		GetComponentInChildren<Animator>().Play("Attacking");
        GameObject.Find("SFX").GetComponent<SFXPlayer>().PlaySound("LeoUnload");
    }

	override protected void postPromptLoop() {
		if(GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Idle")) {
			target.getCharacter().takeDamage(Constants.getInstance().unload_damage);
			setInactiveWithCompletion(true);
		}
	}

	override protected void innerEnd() { }

	override public int getValue() {
		return 0;
	}

	public override string getDescription()
    {
        return "Single Target\n" +
            "Damage: " + Constants.getInstance().unload_damage + "\n" +
            "Uses: " + usesLeft() + "\n" +
            "Range: " + minRange() + "-" + maxRange();
    }
}
