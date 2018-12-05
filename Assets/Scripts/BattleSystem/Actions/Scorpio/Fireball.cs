using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Scorpio
public class Fireball : PromptingAction {
	override public ActionType getActionType() {
		return ActionType.ABILITY;
	}

	override public int maxUses() {
		return Constants.getInstance().fireball_maxUses;
	}

	override protected int minRange() { return Constants.getInstance().fireball_minRange; }
	override protected int maxRange() { return Constants.getInstance().fireball_maxRange; }

	override protected bool validTileToShow(Tile t) {
		return true;
	}

	protected override bool validTileToClick(Tile t) {
		return true;
	}

	override protected bool targetYourField() { return false; }

	override protected void postPromptStart() {
		GetComponentInChildren<Animator>().Play("Attacking");
        GameObject.Find("SFX").GetComponent<SFXPlayer>().PlaySound("ScorpioFireball");
    }

	override protected void postPromptLoop() {
		if(GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Idle")) {
			if(target.getCharacter() != null)
				target.getCharacter().takeDamage(Constants.getInstance().fireball_damage);

			int[] targetYX = target.getTileArrayCoordsYX();
			int[] yx = new int[] { 0, 1 };
			for(int i = 0; i < 4; i++) {
				Tile t = target.getField().getTileAtYX(targetYX[0] + yx[0], targetYX[1] + yx[1]);
				if(t != null && t.getCharacter() != null)
					t.getCharacter().takeDamage(Constants.getInstance().fireball_damage);
				nextDYX(yx);
			}
			setInactiveWithCompletion(true);
		}
	}

	private void nextDYX(int[] yx) {
		if(yx[0] == 0) {
			if(yx[1] == 1) {
				yx[1] = -1;
			} else {
				yx[0] = 1;
				yx[1] = 0;
			}
		} else {
			if(yx[0] == 1) {
				yx[0] = -1;
			} else {
				yx[1] = 1;
				yx[0] = 0;
			}
		}
	}

	override public int getValue() {
		return 0;
	}

	override protected void innerEnd() { }

    public override string getDescription()
    {
        return "AoE Cross\n" +
            "Damage: " + Constants.getInstance().fireball_damage + "\n" +
            "Uses: " + usesLeft() + "\n" +
            "Range: " + minRange() + "-" + maxRange() + " for center";
    }
}
