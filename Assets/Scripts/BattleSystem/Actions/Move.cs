using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : Action {

	public const float SPEED = 5;

	private const float GRID = 2;
	
	private MyState state;
	private Vector3 start;
	private Vector3 dir;

	override protected void innerStart() {
		state = MyState.PROMPTING;
		dir = new Vector3(0, 0);
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
			dir = new Vector3(0, 1);
			start = transform.position;
			if(GameLoop.DEBUG_LOG) Debug.Log(state + " " + dir + " " + start);
			//GameObject.Find("someBattleground").GetComponent<Character>()
		} else if(Input.GetKey(KeyCode.A)) {
			state = MyState.MOVING;
			dir = new Vector3(-1, 0);
			start = transform.position;
			if(GameLoop.DEBUG_LOG) Debug.Log(state + " " + dir + " " + start);
		} else if(Input.GetKey(KeyCode.S)) {
			state = MyState.MOVING;
			dir = new Vector3(0, -1);
			start = transform.position;
			if(GameLoop.DEBUG_LOG) Debug.Log(state + " " + dir + " " + start);
		} else if(Input.GetKey(KeyCode.D)) {
			state = MyState.MOVING;
			dir = new Vector3(1, 0);
			start = transform.position;
			if(GameLoop.DEBUG_LOG) Debug.Log(state + " " + dir + " " + start);
		} else if(Input.GetKey(KeyCode.C)) {
			setInactiveWithCompletion(false);
			if(GameLoop.DEBUG_LOG) Debug.Log("cancel move");
		}
	}

	private void move() {
		Vector3 pos = transform.position;
		Vector3 ds = SPEED * Time.deltaTime * dir;
		
		if(dir.magnitude > 0) {
			if((pos - start + ds).magnitude < GRID) {
				transform.Translate(ds);
			} else {
				transform.Translate(start + GRID*dir - pos);
				setInactiveWithCompletion(true);
			}
		} else {
			Debug.Log("Movement cancelled but still got to moving - FIX");
			setInactiveWithCompletion(false);
		}
	}

	override protected void innerEnd() {}

	private enum MyState {
		PROMPTING, MOVING
	}
}

