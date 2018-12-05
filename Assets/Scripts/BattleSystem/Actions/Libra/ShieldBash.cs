using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Libra
public class ShieldBash : PromptingAction {
	override public ActionType getActionType() {
		return ActionType.ABILITY;
	}

	override public int maxUses() {
		return Constants.getInstance().shieldBash_maxUses;
	}

	override protected int minRange() { return Constants.getInstance().shieldBash_minRange; }
	override protected int maxRange() { return Constants.getInstance().shieldBash_maxRange; }

	override protected bool validTileToShow(Tile t) {
		return true;
	}

	protected override bool validTileToClick(Tile t) {
		return t.getCharacter() != null;
	}

	override protected bool targetYourField() { return false; }

	override protected void postPromptStart() {
		GetComponentInChildren<Animator>().Play("Attacking");
        GameObject.Find("SFX").GetComponent<SFXPlayer>().PlaySound("LibraShieldbash");
    }

	override protected void postPromptLoop() {
		if(GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Idle")) {
			target.getCharacter().takeDamage(Constants.getInstance().shieldBash_damage);
			GameLoop.getInstance().statusManager.addStatusEffect(this.GetComponentInParent<Character>(), new List<Character>() { target.getCharacter() }, StatusEffect.STUNNED);
			setInactiveWithCompletion(true);
		}
	}

	override protected void innerEnd() { }

	override public int getValue() {
		return 0;
	}

	public override string getName()
    {
        return "Shield Bash";
    }

    public override string getDescription()
    {
        return "Single Target, Stuns\n" +
            "Damage: " + Constants.getInstance().shieldBash_damage + "\n" +
            "Uses: " + usesLeft() + "\n" +
            "Range: " + minRange() + "-" + maxRange();
    }
}
