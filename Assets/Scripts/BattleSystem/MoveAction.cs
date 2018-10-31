using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : Action {
	private const float SPEED = 1;
	private const float GRID = 1;

	override protected ActionType getType() { return ActionType.Move; }
	private Vector3 start;
	private MyState state;
	private MoveDir dir;

	override protected void innerStart() {
		state = MyState.PROMPTING;
		dir = MoveDir.UNDECIDED;
		Debug.Log("WASD to move, C to cancel");
	}

	override protected void innerLoop() {
		switch(state) {
			case MyState.PROMPTING:
				prompt();
				break;
			case MyState.MOVING:
				move();
				break;
		}
	}

	private void prompt() {
		if(Input.GetKey(KeyCode.W)) {
			state = MyState.MOVING;
			dir = MoveDir.UP;
			start = transform.position;
			//GameObject.Find("someBattleground").GetComponent<Character>()
		} else if(Input.GetKey(KeyCode.A)) {
			state = MyState.MOVING;
			dir = MoveDir.LEFT;
			start = transform.position;
		} else if(Input.GetKey(KeyCode.S)) {
			state = MyState.MOVING;
			dir = MoveDir.DOWN;
			start = transform.position;
		} else if(Input.GetKey(KeyCode.D)) {
			state = MyState.MOVING;
			dir = MoveDir.RIGHT;
			start = transform.position;
		} else if(Input.GetKey(KeyCode.C)) {
			setActive(false);
		}
	}

	private void move() {
		Vector3 pos = transform.position;
		switch(dir) {
			case MoveDir.UP:
				Vector3 ds = SPEED * Time.deltaTime * new Vector3(0, 1, 0);
				if(pos - start + ds < GRID) {
					transform.Translate(0, ds, 0);
				} else {
					transform.Translate(0, start.y + GRID - pos.y, 0);
					setActive(false);
				}
				break;
			case MoveDir.DOWN:
				//fix
				if(pos.y - start.y + ds < GRID) {
					transform.Translate(0, ds, 0);
				} else {
					transform.Translate(0, start.y + GRID - pos.y, 0);
					setActive(false);
				}
				break;
			case MoveDir.LEFT:
				break;
			case MoveDir.RIGHT:
				break;
			case MoveDir.UNDECIDED:
				Debug.Log("MoveAction got to MyState.MOVING and MoveDir.UNDECIDED - FIX");
				setActive(false);
				break;
		}
	}

	override protected void innerEnd() {}

	private enum MyState {
		PROMPTING, MOVING
	}

	private enum MoveDir {
		UP, DOWN, LEFT, RIGHT, UNDECIDED
	}
}

