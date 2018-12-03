using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crescendo : PromptingAction {
	override public ActionType getActionType() {
		return ActionType.ABILITY;
	}

	override public int maxUses() {
		return Constants.getInstance().crescendo_maxUses;
	}

	override protected int minRange() { return 1; }
	override protected int maxRange() { return 2; }

	override protected bool validTileToShow(Tile t) {
		Character c = GetComponentInParent<Character>();
		int[] homeYX = c.getField().getTileArrayCoordsYX(c.getField().getTileAt(c.transform.position));
		int[] yx = t.getTileArrayCoordsYX();

		if(c.getField().isRight) {
			homeYX[1] += 3;
			return (yx[0] == homeYX[0] && yx[1] == homeYX[1]-1) ||
				(yx[0] == homeYX[0]-1 && yx[1] == homeYX[1]-1) ||
				(yx[0] == homeYX[0]+1 && yx[1] == homeYX[1]-1) ||
				(yx[0] == homeYX[0] && yx[1] == homeYX[1]-2);
		} else {
			return (yx[0] == homeYX[0] && yx[1] == homeYX[1]-2) ||
				(yx[0] == homeYX[0]-1 && yx[1] == homeYX[1]-2) ||
				(yx[0] == homeYX[0]+1 && yx[1] == homeYX[1]-2) ||
				(yx[0] == homeYX[0] && yx[1] == homeYX[1]-1);
		}
	}

	protected override bool validTileToClick(Tile t) {
		Character c = GetComponentInParent<Character>();
		int[] homeYX = c.getField().getTileArrayCoordsYX(c.getField().getTileAt(c.transform.position));
		return c.getField() == GameLoop.getInstance().getAllyField()
			?
			GameLoop.getInstance().getEnemyField().isCharacterAtYX(homeYX[0], homeYX[1] + 2) ||
			GameLoop.getInstance().getEnemyField().isCharacterAtYX(homeYX[0] - 1, homeYX[1] + 2) ||
			GameLoop.getInstance().getEnemyField().isCharacterAtYX(homeYX[0] + 1, homeYX[1] + 2) ||
			GameLoop.getInstance().getEnemyField().isCharacterAtYX(homeYX[0], homeYX[1] + 1)
			:
			GameLoop.getInstance().getEnemyField().isCharacterAtYX(homeYX[0], homeYX[1] - 2) ||
			GameLoop.getInstance().getEnemyField().isCharacterAtYX(homeYX[0] - 1, homeYX[1] - 2) ||
			GameLoop.getInstance().getEnemyField().isCharacterAtYX(homeYX[0] + 1, homeYX[1] - 2) ||
			GameLoop.getInstance().getEnemyField().isCharacterAtYX(homeYX[0], homeYX[1] - 1)
			;
	}

	override protected bool targetYourField() { return false; }

	override protected void postPromptStart() {
		GetComponentInChildren<Animator>().Play("Attacking");
	}

	override protected void postPromptLoop() {
		if(GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Idle")) {
			Character c = GetComponentInParent<Character>();
			int[] homeYX = c.getField().getTileArrayCoordsYX(c.getField().getTileAt(c.transform.position));
			if(c.getField() == GameLoop.getInstance().getAllyField()) {
				homeYX[1] += 3;
				Tile t = GameLoop.getInstance().getEnemyField().getTileAtYX(homeYX[0], homeYX[1] - 1);
				if(t != null && t.getCharacter() != null)
					t.getCharacter().takeDamage(Constants.getInstance().crescendo_damage);
				t = GameLoop.getInstance().getEnemyField().getTileAtYX(homeYX[0] - 1, homeYX[1] - 1);
				if(t != null && t.getCharacter() != null)
					t.getCharacter().takeDamage(Constants.getInstance().crescendo_damage);
				t = GameLoop.getInstance().getEnemyField().getTileAtYX(homeYX[0] + 1, homeYX[1] - 1);
				if(t != null && t.getCharacter() != null)
					t.getCharacter().takeDamage(Constants.getInstance().crescendo_damage);
				t = GameLoop.getInstance().getEnemyField().getTileAtYX(homeYX[0], homeYX[1] - 2);
				if(t != null && t.getCharacter() != null)
					t.getCharacter().takeDamage(Constants.getInstance().crescendo_damage);
			} else {
				Tile t = GameLoop.getInstance().getAllyField().getTileAtYX(homeYX[0], homeYX[1] - 2);
				if(t != null && t.getCharacter() != null)
					t.getCharacter().takeDamage(Constants.getInstance().crescendo_damage);
				t = GameLoop.getInstance().getAllyField().getTileAtYX(homeYX[0] - 1, homeYX[1] - 2);
				if(t != null && t.getCharacter() != null)
					t.getCharacter().takeDamage(Constants.getInstance().crescendo_damage);
				t = GameLoop.getInstance().getAllyField().getTileAtYX(homeYX[0] + 1, homeYX[1] - 2);
				if(t != null && t.getCharacter() != null)
					t.getCharacter().takeDamage(Constants.getInstance().crescendo_damage);
				t = GameLoop.getInstance().getAllyField().getTileAtYX(homeYX[0], homeYX[1] - 1);
				if(t != null && t.getCharacter() != null)
					t.getCharacter().takeDamage(Constants.getInstance().crescendo_damage);
			}
			setInactiveWithCompletion(true);
		}
	}

	override protected void innerEnd() { }

    public override string getDescription()
    {
        return "AoE Cone\n" +
            "Damage: " + Constants.getInstance().crescendo_damage + "\n" +
            "Uses: " + maxUses() + "\n" +
            "Range: Area directly to left of caster";
    }
}
