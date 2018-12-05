using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Scorpio
public class LightningBolt : PromptingAction {
	private List<Tile> affectedTiles;

	override public ActionType getActionType() {
		return ActionType.ABILITY;
	}

	override public int maxUses() {
		return Constants.getInstance().lightningBolt_maxUses;
	}

	override protected int minRange() {
		Character c = GetComponentInParent<Character>();
		return GetComponentInParent<Character>().getField().isRight
			? c.getField().getTileArrayCoordsYX(c.getField().getTileAt(c.transform.position))[1]+1
			: 3- c.getField().getTileArrayCoordsYX(c.getField().getTileAt(c.transform.position))[1];
	}
	override protected int maxRange() { return minRange(); }

	override protected bool validTileToShow(Tile t) {
		return true;
	}

	protected override bool validTileToClick(Tile t) {
		return true;
	}

	override protected bool targetYourField() { return false; }

	override protected void innerStart() {
		base.innerStart();

		affectedTiles = new List<Tile>();
		Character c = GetComponentInParent<Character>();
		int homeY = c.getField().getTileArrayCoordsYX(c.getField().getTileAt(c.transform.position))[0];
		Field field = (c.getField() == GameLoop.getInstance().getAllyField()) ? GameLoop.getInstance().getEnemyField() : GameLoop.getInstance().getAllyField();
		if(field.isRight) {
			for(int x = 0; x < 3; x++) {
				affectedTiles.Add(field.getTileAtYX(homeY, x));
				if(x != 0)
					affectedTiles[affectedTiles.Count - 1].setSecondarySprite();
			}
		} else {
			for(int x = 2; x >= 0; x--) {
				affectedTiles.Add(field.getTileAtYX(homeY, x));
				if(x != 2)
					affectedTiles[affectedTiles.Count - 1].setSecondarySprite();
			}
		}
	}

	override protected void postPromptStart() {
		GetComponentInChildren<Animator>().Play("Attacking");
        GameObject.Find("SFX").GetComponent<SFXPlayer>().PlaySound("ScorpioLightning");
    }

	override protected void postPromptLoop() {
		if(GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Idle")) {
			foreach(Tile t in affectedTiles)
				if(t.getCharacter() != null)
					t.getCharacter().takeDamage(Constants.getInstance().lightningBolt_damage);

			setInactiveWithCompletion(true);
		}
	}

	override protected void innerEnd() { }

	override public int getValue() {
		return 0;
	}

	public override string getName()
    {
        return "Lightning Bolt";
    }

    public override string getDescription()
    {
        return "AoE Line\n" +
            "Damage: " + Constants.getInstance().lightningBolt_damage + "\n" +
            "Uses: " + usesLeft() + "\n" +
            "Range: Whole row (no friendly fire)";
    }
}
