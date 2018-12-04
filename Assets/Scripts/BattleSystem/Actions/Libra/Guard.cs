using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Libra
public class Guard : PromptingAction {
	private List<Tile> affectedTiles;

	override public ActionType getActionType() {
		return ActionType.ABILITY;
	}

	override public int maxUses() {
		return Constants.getInstance().guard_maxUses;
	}

	override protected int minRange() { return Constants.getInstance().guard_minRange; }
	override protected int maxRange() { return Constants.getInstance().guard_maxRange; }

	override protected bool validTileToShow(Tile t) {
		return true;
	}

	protected override bool validTileToClick(Tile t) {
		return true;
	}

	override protected bool targetYourField() { return true; }

	override protected void innerStart() {
		base.innerStart();

		affectedTiles = new List<Tile>();
		int[] homeYX = inRange[0].getTileArrayCoordsYX();
		if(inRange[0].getField().isRight) {
			for(int x = homeYX[1]+1; x < 3; x++) {
				affectedTiles.Add(inRange[0].getField().getTileAtYX(homeYX[0], x));
				affectedTiles[affectedTiles.Count - 1].setSecondarySprite();
			}
		} else {
			for(int x = homeYX[1] - 1; x >= 0; x--) {
				affectedTiles.Add(inRange[0].getField().getTileAtYX(homeYX[0], x));
				affectedTiles[affectedTiles.Count - 1].setSecondarySprite();
			}
		}
	}
	
	override protected void postPromptStart() {
		GetComponentInChildren<Animator>().Play("Attacking");
	}

	override protected void postPromptLoop() {
		if(GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Idle")) {
			List<Character> toInvuln = new List<Character>();
			if(target.getCharacter() != null)
				toInvuln.Add(target.getCharacter());
			foreach(Tile t in affectedTiles)
            {
                t.addStatus(StatusEffect.INVULNERABLE);
                //if (t.getCharacter() != null)
                //    toInvuln.Add(t.getCharacter());
            }


			GameLoop.getInstance().statusManager.addStatusEffect(GetComponentInParent<Character>(), toInvuln, StatusEffect.INVULNERABLE);

			setInactiveWithCompletion(true);
		}
	}

	override protected void innerEnd() {}

	override public int getValue() {
		return 0;
	}

	public override string getDescription()
    {
        return "Single Target, Defense\n" +
            "Damage: 0\n" +
            "Uses: " + usesLeft() + "\n" +
            "Range: All spaces to the right, including current";
    }
}
