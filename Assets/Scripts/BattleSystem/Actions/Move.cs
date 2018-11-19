using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : Action {
	public const float SPEED = 5;

	private const float GRID = 2;
	
	private MyState state;
	private Vector3 start;
	private Vector3 dir;
	private Vector3 end;

	override public ActionType getActionType() {
		return ActionType.MOVE;
	}

	override protected void innerStart() {
        state = MyState.MOVING;//MyState.PROMPTING;
		dir = new Vector3(0, 0);
		Debug.Log("WASD to move, C to cancel");
	}

	override protected void innerLoop() {
        Debug.Log("Moving State: " + state);
		switch(state) {
			case MyState.PROMPTING:
				prompt();
				break;
			case MyState.MOVING:
				move();
				break;
		}
	}
	public bool AttemptMove(Vector3 start, Vector3 end){
		// print("end " + end);
		// foreach(Vector3 pos in GameObject.Find("Enemy_battleGround").GetComponent<Grid_Behavior>().gridPositions) {
		// 	print(pos);
		// }

		if(GameObject.Find("Enemy_battleGround").GetComponent<Grid_Behavior>().gridPositions.Contains(end)){
			return true;
		}
		return false;

	}

	private void prompt() {
		dir = new Vector3(0, 0);
		bool[] inputs = { Input.GetKey(KeyCode.W), Input.GetKey(KeyCode.A), Input.GetKey(KeyCode.S),
						  Input.GetKey(KeyCode.D), Input.GetKey(KeyCode.C)};
		if(inputs[0]) {
			dir = new Vector3(0, 1);
		} else if(inputs[1]) {
			dir = new Vector3(-1, 0);
		} else if(inputs[2]) {
			dir = new Vector3(0, -1);
		} else if(inputs[3]) {
			dir = new Vector3(1, 0);
		} else if(inputs[4]) {
			setInactiveWithCompletion(false);
			if(GameLoop.getInstance().DEBUG_LOG) Debug.Log("cancel move");
		}

		if((inputs[0] || inputs[1] || inputs[2] || inputs[3]) && !Input.GetKey(KeyCode.C)) {
			start = transform.position;
			end = start + GRID*dir;
			if(AttemptMove(start, end) == true) {
				state = MyState.MOVING;
			} else {
				Debug.Log("invalid direction");
				setInactiveWithCompletion(false);
			}
			if(GameLoop.getInstance().DEBUG_LOG) Debug.Log(state + " " + dir + " " + start);
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

    #region Direction-based Movement
    public void moveDirection(string direction)
    {
        direction = direction.ToLower();
        switch (direction)
        {
            case "up":
                moveUp();
                break;
            case "down":
                moveDown();
                break;
            case "right":
                moveRight();
                break;
            case "left":
                moveLeft();
                break;
            default:
                cancelMovement();
                break;
        }
    }

    private void moveUp()
    {
        dir = new Vector3(0, 1);
        start = transform.position;
        end = start + dir;
        Debug.Log(end);
        if (AttemptMove(start, end) == true)
        {
            state = MyState.MOVING;
        }
        if (GameLoop.getInstance().DEBUG_LOG) Debug.Log(state + " " + dir + " " + start);
        //GameObject.Find("someBattleground").GetComponent<Character>()
    }

    private void moveDown()
    {
        end = start + dir;
        dir = new Vector3(0, -1);
        start = transform.position;
        if (AttemptMove(start, end) == true)
        {
            state = MyState.MOVING;
        }
        if (GameLoop.getInstance().DEBUG_LOG) Debug.Log(state + " " + dir + " " + start);
    }

    private void moveLeft()
    {
        end = start + dir;
        dir = new Vector3(-1, 0);
        start = transform.position;
        if (AttemptMove(start, end) == true)
        {
            state = MyState.MOVING;
        }
        if (GameLoop.getInstance().DEBUG_LOG) Debug.Log(state + " " + dir + " " + start);
    }

    private void moveRight()
    {
        end = start + dir;
        dir = new Vector3(1, 0);
        start = transform.position;
        if (AttemptMove(start, end) == true)
        {
            state = MyState.MOVING;
        }
        if (GameLoop.getInstance().DEBUG_LOG) Debug.Log(state + " " + dir + " " + start);
    }

    private void cancelMovement()
    {
        setInactiveWithCompletion(false);
        if (GameLoop.getInstance().DEBUG_LOG) Debug.Log("cancel move");
    }

    private void moveDir(Vector3 dir)
    {
        start = transform.position;
        end = start + GRID * dir;
        if (AttemptMove(start, end))
        {
            state = MyState.MOVING;
        }
        else
        {
            Debug.Log("invalid direction");
            setInactiveWithCompletion(false);
        }
        if (GameLoop.getInstance().DEBUG_LOG) Debug.Log(state + " " + dir + " " + start);
    }
    #endregion
}

