using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : Action {
	private MyState state;
	private Character target;
//	private Vector3 home;

	private const int SPEED = 1;

	override public ActionType getActionType() {
		return ActionType.ABILITY;
	}

	override protected void innerStart() {
		state = MyState.PROMPT;
		target = null;
	}

	override protected void innerLoop() {
		switch(state) {
			case MyState.PROMPT:
				prompt();
				break;
			case MyState.MOVE_TO:
				moveTo();
				break;
			case MyState.DAMAGE:
				damage();
				break;
			case MyState.MOVE_BACK:
				moveBack();
				break;
		}
	}

	private void prompt() {
		target = GameObject.Find("Enemy1").GetComponent<Character>();
//		home = transform.position;

		state = MyState.MOVE_TO;
	}

	private void moveTo() {
//		Vector3 ds = -SPEED*Time.deltaTime/home.magnitude * home;

		state = MyState.DAMAGE;
	}

	private void damage() {
		if(target != null) {
			target.takeDamage(GameObject.Find(this.name).GetComponent<Character>().basicAttackDamage);
		} else {
			Debug.Log("NO/INVALID TARGET");
		}
		state = MyState.MOVE_BACK;
	}

	private void moveBack() {
		setInactiveWithCompletion(true);
	}

	override protected void innerEnd() {}

	private enum MyState {
		PROMPT, MOVE_TO, DAMAGE, MOVE_BACK
	}
}
