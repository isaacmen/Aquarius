using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShapeAction : Action {
	public abstract void playAnimation();
	public abstract bool animationComplete();
	public abstract bool[,] getTargets();
	public abstract int getDamage();

	private bool firstCheck;

	override public ActionType getActionType() {
		return ActionType.ABILITY;
	}

	override protected void innerStart() {
		firstCheck = true;
		Field targetField = (GetComponentInParent<Character>().getField() == GameLoop.getInstance().getEnemyField())
									? GameLoop.getInstance().getAllyField()
									: GameLoop.getInstance().getEnemyField()
									;
		bool[,] targets = getTargets();
		for(int y = 0; y < 3; y++)
			for(int x = 0; x < 3; x++)
				if(targets[y, x])
					targetField.getTileAtYX(y, x).setTertiarySprite();
		playAnimation();
	}

	override protected void innerLoop() {
		if(animationComplete()) {
			if(firstCheck) {
				firstCheck = false;
			} else {
				print("animation complete");
				bool[,] targets = getTargets();
				Field targetField = (GetComponentInParent<Character>().getField() == GameLoop.getInstance().getEnemyField())
										? GameLoop.getInstance().getAllyField()
										: GameLoop.getInstance().getEnemyField()
										;

				for(int y = 0; y < 3; y++)
					for(int x = 0; x < 3; x++)
						if(targets[y, x] && targetField.isCharacterAtYX(y, x))
							targetField.getTileAtYX(y, x).getCharacter().takeDamage(getDamage());

				setInactiveWithCompletion(true);
			}
		}
	}

	override public int getValue() {
		bool[,] targets = getTargets();
		Field targetField = (GetComponentInParent<Character>().getField() == GameLoop.getInstance().getEnemyField())
								? GameLoop.getInstance().getAllyField()
								: GameLoop.getInstance().getEnemyField()
								;
		int damage = getDamage();

		int value = 0;

		for(int y = 0; y < 3; y++)
			for(int x = 0; x < 3; x++)
				if(targets[y, x] && targetField.isCharacterAtYX(y, x))
					value += (damage > targetField.getTileAtYX(y, x).getCharacter().health)
								? Mathf.Max(damage * 2, damage + 15)
								: damage
								;

		return value;
	}

	override protected void innerEnd() { }
}
