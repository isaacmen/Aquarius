using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeAction2 : Action
{
    private bool[,] targets;
    private int damage;
    private bool attacked;

    public void setTargets(bool[,] t)
    {
        targets = t;
    }

    override public int maxUses()
    {
        return 9999999;
    }

    public void setDamage(int d)
    {
        damage = d;
        print("Damage set to :" + damage);
    }

    override public ActionType getActionType()
    {
        return ActionType.ABILITY;
    }

    override protected void innerStart()
    {
        attacked = false;
        Field targetField = (GetComponentInParent<Character>().getField() == GameLoop.getInstance().getEnemyField())
                                    ? GameLoop.getInstance().getAllyField()
                                    : GameLoop.getInstance().getEnemyField()
                                    ;
        for (int y = 0; y < 3; y++)
            for (int x = 0; x < 3; x++)
                if (targets[y, x])
                    targetField.getTileAtYX(y, x).setTertiarySprite();
        GetComponentInChildren<Animator>().Play("Attacking");
    }

    override protected void innerLoop()
    {
        if (GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            if (attacked)
            {
                if (GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                {
                    attacked = true;
                    Debug.Log("Attacking");
                    GetComponentInChildren<Animator>().Play("Attacking");
                }
            }
            else
            {
                attacked = false;
                print("animation complete");
                GameObject.Find("SFX").GetComponent<SFXPlayer>().PlaySound("AquaTidalHit");
                Field targetField = (GetComponentInParent<Character>().getField() == GameLoop.getInstance().getEnemyField())
                                        ? GameLoop.getInstance().getAllyField()
                                        : GameLoop.getInstance().getEnemyField()
                                        ;

                for (int y = 0; y < 3; y++)
                    for (int x = 0; x < 3; x++)
                        if (targets[y, x] && targetField.isCharacterAtYX(y, x))
                        {
                            targetField.getTileAtYX(y, x).getCharacter().takeDamage(damage);
                        }

                resetAllSprites();
                GameLoop.getInstance().nextTurn(); //skip this turn (taking a rest period)
                setInactiveWithCompletion(true);
            }

        }
    }

    override public int getValue()
    {
        Field targetField = (GetComponentInParent<Character>().getField() == GameLoop.getInstance().getEnemyField())
                                ? GameLoop.getInstance().getAllyField()
                                : GameLoop.getInstance().getEnemyField()
                                ;

        int value = 0;

        for (int y = 0; y < 3; y++)
            for (int x = 0; x < 3; x++)
                if (targets[y, x] && targetField.isCharacterAtYX(y, x))
                    value += (damage > targetField.getTileAtYX(y, x).getCharacter().health)
                                ? Mathf.Max(damage * 2, damage + 15)
                                : damage
                                ;

        return value;
    }

    override protected void innerEnd() { }

}
