using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShapeAction : Action {
	public abstract void playAnimation();
	public abstract bool animationComplete();
	public abstract bool[,] getTargets();
	public abstract int getDamage();

	private bool alreadySet;

	override public ActionType getActionType() {
		return ActionType.ABILITY;
	}

	override protected void innerStart() {
		playAnimation();
	}

	override protected void innerLoop() {
        if (GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            GameLoop.getInstance().delayedActionManager.addAction(GetComponentInParent<Character>(), 1, GetComponentInParent<ShapeAction2>());
            GetComponentInParent<ShapeAction2>().setTargets(getTargets());
            GetComponentInParent<ShapeAction2>().setDamage(getDamage());
            Debug.Log("Tidal Wave: Charging");
            setInactiveWithCompletion(true);
        }

        markTiles();
    }

	override protected void innerEnd() { }

    override public int getValue()
    {
        Field targetField = (GetComponentInParent<Character>().getField() == GameLoop.getInstance().getEnemyField())
                                ? GameLoop.getInstance().getAllyField()
                                : GameLoop.getInstance().getEnemyField()
                                ;

        int value = 0;

        for (int y = 0; y < 3; y++)
            for (int x = 0; x < 3; x++)
                if (getTargets()[y, x] && targetField.isCharacterAtYX(y, x))
                    value += (getDamage() > targetField.getTileAtYX(y, x).getCharacter().health)
                                ? Mathf.Max(getDamage() * 2, getDamage() + 15)
                                : getDamage()
                                ;

        return value;
    }

    private void markTiles()
    {
        Field targetField = (GetComponentInParent<Character>().getField() == GameLoop.getInstance().getEnemyField())
                                        ? GameLoop.getInstance().getAllyField()
                                        : GameLoop.getInstance().getEnemyField()
                                        ;

        bool[,] targets = getTargets();

        for (int y = 0; y < 3; y++)
            for (int x = 0; x < 3; x++)
                if (targets[x, y])
                {
                    targetField.getTileAtYX(x, y).addStatus(StatusEffect.TARGET);
                    targetField.getTileAtYX(x, y).setTertiarySprite();
                }
                    
    }

}
