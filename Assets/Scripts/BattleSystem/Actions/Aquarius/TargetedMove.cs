using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetedMove : TargetedAction {
	public const float SPEED = 5;

	private const float GRID = 2;

	private Vector3 start;
	private Vector3 dir;
	private Vector3 end;

	override public ActionType getActionType() {
		return ActionType.MOVE;
	}

	override public int minRange() { return 1; }
	override public int maxRange() { return 1; }

	override public int maxUses() {
		return 9999999;
	}

	override protected void innerStart() {
		start = transform.position;
		end = target.transform.position - new Vector3(0, 0, 3);
		dir = (end - start) / (end - start).magnitude;
	}

	override protected void innerLoop() {
		Vector3 pos = transform.position;
		Vector3 ds = SPEED * Time.deltaTime * dir;

		if(pos != end)
			transform.position = Vector3.MoveTowards(pos, end, SPEED * Time.deltaTime);
		else {
			Debug.Log("Stopping moving");
			GetComponentInParent<Character>().moveGrid(start, end);
			setInactiveWithCompletion(true);
		}
	}

	override protected void innerEnd() { }

	override public int getValue() {
		return 1;
	}
}
