﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : PromptingAction {
	public const float SPEED = 5;

	private const float GRID = 2;

	private Vector3 start;
	private Vector3 dir;
	private Vector3 end;

	override public ActionType getActionType() {
		return ActionType.MOVE;
	}

	override public int maxUses() {
		return 9999999;
	}

	override protected int minRange() { return 1; }
	override protected int maxRange() { return 1; }

	override protected bool validTileToShow(Tile t) {
		return t.getCharacter() == null;
	}

	protected override bool validTileToClick(Tile t) {
		return true;
	}

	override protected bool targetYourField() { return true; }

	override protected void innerStart() {
		base.innerStart();

		Character c = GetComponentInParent<Character>();
		c.getField().getTileAt(c.transform.position).setSecondarySprite();
	}

	override protected void postPromptStart() {
        GetComponentInChildren<Animator>().Play("Jump");
        GameObject.Find("SFX").GetComponent<SFXPlayer>().PlaySound("jump");
        start = transform.position;
		end = target.transform.position - new Vector3(0, 0, 3);
		dir = (end - start) / (end-start).magnitude;
    }

	override protected void postPromptLoop() {
		Vector3 pos = transform.position;
		Vector3 ds = SPEED * Time.deltaTime * dir;
        
        if (pos != end)
            transform.position = Vector3.MoveTowards(pos, end, SPEED * Time.deltaTime);
        else
        {
            Debug.Log("Stopping moving");
            GetComponentInParent<Character>().moveGrid(start, end);
            setInactiveWithCompletion(true);
        }
    }

	override public int getValue() {
		return 0;
	}

	override protected void innerEnd() {}
}

