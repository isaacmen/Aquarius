using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : PromptingAction {
	
	override public ActionType getActionType() {
		return ActionType.ABILITY;
	}

	override public int maxUses() {
		return 9999999;
	}

	override protected int minRange() { return GetComponentInParent<Character>().basicAttackMinRange; }
	override protected int maxRange() { return GetComponentInParent<Character>().basicAttackMaxRange; }

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
			target.getCharacter().takeDamage(GetComponentInParent<Character>().basicAttackDamage);
			setInactiveWithCompletion(true);
		}
	}

	override protected void innerEnd() { }

	override public int getValue() {
		List<Tile> tiles = Field.tilesInCharacterRange(this.GetComponentInParent<Character>(), minRange(), maxRange(), targetYourField());
		int value = 0;

		foreach(Tile t in tiles)
			if(t.getCharacter() != null) {
				int thisValue = GetComponentInParent<Character>().basicAttackDamage;
				if(thisValue > t.getCharacter().health)
					thisValue = Mathf.Max(2*thisValue, thisValue+15);
				if(thisValue > value)
					value = thisValue;
			}
				

		return value;
	}

	public override string getName()
    {
        return "Attack";
    }

    public override string getDescription()
    {
        Character character = GetComponentInParent<Character>();
        string description = "Single Target\n" +
            "Damage: " + character.basicAttackDamage + "\n" +
            "Uses: Infinite\n" +
            "Range: ";
        if (character.name.Contains("Scorpio"))
            description += "Infinite";
        else if (character.name.Contains("Libra"))
            description += "1";
        else if (character.name.Contains("Leo"))
            description += "2-3";
        return description;
    }
}
