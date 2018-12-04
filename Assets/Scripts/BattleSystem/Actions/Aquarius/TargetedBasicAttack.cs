using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetedBasicAttack : TargetedAction {
	private bool firstCheck;

	override public ActionType getActionType() {
		return ActionType.ABILITY;
	}

	override protected int minRange() { return GetComponentInParent<Character>().basicAttackMinRange; }
	override protected int maxRange() { return GetComponentInParent<Character>().basicAttackMaxRange; }
	override protected bool targetYourField() { return true; }

	override public int maxUses() {
		return 9999999;
	}

	override protected void innerStart() {
		firstCheck = true;
		GetComponentInChildren<Animator>().Play("Attacking");
	}

	override protected void innerLoop() {
		if(GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Idle")) {
			if(firstCheck) {
				firstCheck = false;
			} else {
				target.getCharacter().takeDamage(GetComponentInParent<Character>().basicAttackDamage);
				setInactiveWithCompletion(true);
			}
		}
	}

	override protected void innerEnd() { }

	override public int getValue() {
		List<Tile> tiles = Field.tilesInCharacterRange(this.GetComponentInParent<Character>(), minRange(), maxRange(), false);
		int value = 0;

		foreach(Tile t in tiles)
			if(t.getCharacter() != null) {
				int thisValue = GetComponentInParent<Character>().basicAttackDamage;
				if(thisValue > t.getCharacter().health)
					thisValue = Mathf.Max(2 * thisValue, thisValue + 15);
				if(thisValue > value)
					value = thisValue;
			}


		return value;
	}

	override public List<Tile> getOptimalTiles() {
		List<Tile> tiles = Field.tilesInCharacterRange(this.GetComponentInParent<Character>(), minRange(), maxRange(), false);
		List<Tile> optimalTiles = new List<Tile>();
		int highestValue = 0;

		foreach(Tile t in tiles)
			if(t.getCharacter() != null) {
				int thisValue = GetComponentInParent<Character>().basicAttackDamage;
				if(thisValue > t.getCharacter().health)
					thisValue = Mathf.Max(2 * thisValue, thisValue + 15);
				if(thisValue > highestValue) {
					highestValue = thisValue;
					optimalTiles = new List<Tile>() { t };
				} else if(thisValue == highestValue) {
					optimalTiles.Add(t);
				}
			}


		return optimalTiles;
	}
}
