﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour {
	// DO NOT CHANGE state WITH "state = GameState.X"
	// INSTEAD, CHANGE state WITH "setState(GameState.X)"
	// OTHERWISE THINGS AREN'T UPDATED AND STUFF BREAKS
	private GameState state = GameState.INIT;

	private List<Character> turnOrder;
	private Character turn = null;
	private List<Action> turnActions = null;
	private Action activeAction = null;
	private List<ActionType> actionTypesPerTurn = null;

	private Field yourField;
	private Field enemyField;

	[Header("Turn")]	
	public bool randomizeTurnOrder;

	[Header("Constants")]
	public static bool DEBUG_LOG;

	public static GameLoop getInstance() {
		return GameObject.Find("GameLoop").GetComponent<GameLoop>();
	}

	void Awake() {
		if(DEBUG_LOG) Debug.Log("GameLoop awake");

		// to be implemented to be set to real fields from GameObject.Find(nameOfField)
		yourField = new Field();
		enemyField = new Field();

		turnOrder = new List<Character>();
	}

	// could also notify ui
	public void nextTurn() {
		Character turnDone = turnOrder[0];
		turnOrder.Remove(turnDone);
		turnOrder.Add(turnDone);
		setState(GameState.START_TURN);
        GetComponent<UI_Manager>().updateTurn();
    }

	// may be unnecessary
	public Character getCharacterTurn() {
		return turnOrder[0];
	}

	private void printTurnOrder() {
		string toPrint = "";
		foreach(AllyCharacter c in turnOrder) {
			toPrint += c.ToString() + " ";
		}
		Debug.Log(toPrint);
	}

	// to be implemented with real fields
	public void addAllyCharacter(AllyCharacter c) {
		turnOrder.Add(c);
		yourField.addCharacter(c);
	}

	// to be implemented with real fields
	public void addEnemyCharacter(EnemyCharacter c) {
		turnOrder.Add(c);
		enemyField.addCharacter(c);
	}

	void Start() {

	}

	void Update() {
		switch(state) {
			case GameState.INIT:
				// can randomize turn order
				if(randomizeTurnOrder) {
					List<Character> newTurnOrder = new List<Character>();
					List<float> randList = new List<float>();
					for(int i = 0; i < turnOrder.Count; i++)
						randList.Add(Random.Range(0.0f, 1.0f));

					for(int i = 0; i < turnOrder.Count; i++) {
						int maxIdx = randList.IndexOf(Mathf.Max(randList.ToArray()));
						newTurnOrder.Add(turnOrder[maxIdx]);
						randList[maxIdx] = 0;
					}

					turnOrder = newTurnOrder;
				}
				setState(GameState.START_TURN);
				break;
			case GameState.START_TURN:
				break;
			case GameState.ALLY_WAIT_INPUT:
                // keeps checking for keyboard input; eventually to be overwritten with ui
                /*
                for (int i = 1; i < turnActions.Count + 1; i++) {
					if(Input.GetKeyDown(keyCodeFromInt(i)) && !turnActions[i - 1].isActive()) {
						activeAction = turnActions[i-1];
						if(turnActions[i-1].GetType() == typeof(Pass))
							Debug.Log(turn + " passed.");
						else if(turnActions[i-1].GetType() == typeof(Move))
							Debug.Log(turn + " moved.");
						else
							Debug.Log(turn + " used " + turnActions[i-1].GetType() + "!");
						activeAction.setActive();
						setState(GameState.ALLY_ACTION_ACTIVE);
					}
				}
                */
				break;
			case GameState.ALLY_ACTION_ACTIVE:
				// keeps checking for action to not be active
				if(!activeAction.isActive()) {
					if(DEBUG_LOG) Debug.Log("GameLoop detects action inactive");

					// updates actionTypesPerTurn if the previously
					// active action was fully completed and executed
					if(activeAction.getCompletion())
						if(activeAction.getActionType() == ActionType.MOVE) {
							actionTypesPerTurn.Remove(ActionType.MOVE);
						} else if(activeAction.getActionType() == ActionType.PASS) {
							actionTypesPerTurn.Clear();
						} else {
							actionTypesPerTurn.Remove(ActionType.ABILITY);
						}

					// if there are no more moves in the turn, remove
					// all actions with ActionType.MOVE from turnActions
					if(actionTypesPerTurn.IndexOf(ActionType.MOVE) == -1) {
						for(int i = 0; i < turnActions.Count; i++) {
							if(turnActions[i].getActionType() == ActionType.MOVE) {
								turnActions.Remove(turnActions[i]);
								i--;
							}
						}
					}

					// if there are no more abilities in the turn, remove
					// all actions with ActionType.ABILITY from turnActions
					if(actionTypesPerTurn.IndexOf(ActionType.ABILITY) == -1) {
						for(int i = 0; i < turnActions.Count; i++) {
							if(turnActions[i].getActionType() == ActionType.ABILITY) {
								turnActions.Remove(turnActions[i]);
								i--;
							}
						}
					}

					// checks if turn is over to move to the appropriate next state
					if(turnOver()) {
						nextTurn();
					} else {
						setState(GameState.ALLY_WAIT_INPUT);
					}
					activeAction = null;
				}
				break;
			case GameState.ENEMY_STATE:
				Debug.Log("GameLoop Update() in ENEMY_STATE for " + turn);
				nextTurn();
				break;
		}
	}

	private bool turnOver() {
		return  actionTypesPerTurn.Count == 0 ||
				(turnActions.Count == 0) ||
				(turnActions.Count == 1 && turnActions[0].getActionType() == ActionType.PASS);
	}

    public void setState(string newState)
    {
        if (newState.ToLower() == "start turn")
            state = GameState.START_TURN;

        switch (newState.ToLower())
        {
            case "start turn":
                state = GameState.START_TURN;
                break;
            case "ally wait input":
                state = GameState.ALLY_WAIT_INPUT;
                break;
            case "ally action active":
                state = GameState.ALLY_ACTION_ACTIVE;
                break;
            case "enemy state":
                state = GameState.ENEMY_STATE;
                break;
        }

        switch (state)
        {
            case GameState.START_TURN:
                // resets variables for each turn
                turn = getCharacterTurn();
                Debug.Log(turn.name + "'s Turn!");

                if (turn.GetType() == typeof(AllyCharacter))
                {
                    turnActions = ((AllyCharacter)turn).getActions();
                    actionTypesPerTurn = ((AllyCharacter)turn).getActionTypesPerTurn();

                    // enemy stuff null

                    setState(GameState.ALLY_WAIT_INPUT);
                }
                else
                {
                    // init enemy stuff

                    turnActions = null;
                    actionTypesPerTurn = null;

                    setState(GameState.ENEMY_STATE);
                }

                break;
            case GameState.ALLY_WAIT_INPUT:
                // prints possible actions then waits for the user to select one
                // to be partially overridden by implemention with ui
                string turnMoveStr = "What should " + turn + " do? (press # to act)\n";
                for (int i = 0; i < turnActions.Count; i++)
                {
                    turnMoveStr += (i + 1) + ": " + turnActions[i].GetType() + ((i < turnActions.Count - 1) ? " / " : "");
                }
                Debug.Log(turnMoveStr);

                if (DEBUG_LOG)
                {
                    string s = "";
                    foreach (ActionType at in actionTypesPerTurn)
                    {
                        s += at + " ";
                    }
                    Debug.Log("ActionType's left: " + s);
                }

                break;
            case GameState.ALLY_ACTION_ACTIVE:
                break;
            case GameState.ENEMY_STATE:
                Debug.Log("GameLoop entered ENEMY_STATE for " + turn);
                break;
        }
    }

	public void setState(GameState newState) {
		// things to be done when exiting a state (nothing so far; maybe won't be needed)
		switch(state) {
			case GameState.START_TURN:			break;
			case GameState.ALLY_WAIT_INPUT:		break;
			case GameState.ALLY_ACTION_ACTIVE:	break;
			case GameState.ENEMY_STATE:			break;
		}

		// update state
		state = newState;

		// things to be done when entering a state
		switch(state) {
			case GameState.START_TURN:
				// resets variables for each turn
				turn = getCharacterTurn();
				Debug.Log(turn.name + "'s Turn!");

				if(turn.GetType() == typeof(AllyCharacter)) {
					turnActions = ((AllyCharacter)turn).getActions();
					actionTypesPerTurn = ((AllyCharacter)turn).getActionTypesPerTurn();

					// enemy stuff null

					setState(GameState.ALLY_WAIT_INPUT);
				} else {
					// init enemy stuff

					turnActions = null;
					actionTypesPerTurn = null;
					
					setState(GameState.ENEMY_STATE);
				}

				break;
			case GameState.ALLY_WAIT_INPUT:
				// prints possible actions then waits for the user to select one
				// to be partially overridden by implemention with ui
				string turnMoveStr = "What should " + turn + " do? (press # to act)\n";
				for(int i = 0; i < turnActions.Count; i++) {
					turnMoveStr += (i + 1) + ": " + turnActions[i].GetType() + ((i < turnActions.Count - 1) ? " / " : "");
				}
				Debug.Log(turnMoveStr);

				if(DEBUG_LOG) {
					string s = "";
					foreach(ActionType at in actionTypesPerTurn) {
						s += at + " ";
					}
					Debug.Log("ActionType's left: " + s);
				}

				break;
			case GameState.ALLY_ACTION_ACTIVE:
				break;
			case GameState.ENEMY_STATE:
				Debug.Log("GameLoop entered ENEMY_STATE for " + turn);
				break;
		}
	}

	// is there a better way to do this than to hard-code?
	private KeyCode keyCodeFromInt(int n) {
		switch(n) {
			case 1: return KeyCode.Alpha1;
			case 2: return KeyCode.Alpha2;
			case 3: return KeyCode.Alpha3;
			case 4: return KeyCode.Alpha4;
			case 5: return KeyCode.Alpha5;
			default:
				Debug.Log("number " + n + " not implemented yet");
				return KeyCode.Alpha0;
		}
	}

	public enum GameState {
		INIT, START_TURN,                       // general states
		ALLY_WAIT_INPUT, ALLY_ACTION_ACTIVE,    // ally turn states
		ENEMY_STATE                             // enemy turn states
	}

    #region Public versions of input-based actions (for UI accessibility)
    public void pass()
    {
        for (int i = 1; i < turnActions.Count + 1; i++)
        {
            activeAction = turnActions[i - 1];
            if (turnActions[i - 1].GetType() != typeof(Pass))
                continue;
            Debug.Log(turn + " passed.");
            activeAction.setActive();
            setState(GameState.ALLY_ACTION_ACTIVE);
        }
    }

    public void attack()
    {
        for (int i = 1; i < turnActions.Count + 1; i++)
        {
            activeAction = turnActions[i - 1];
            if (turnActions[i - 1].GetType() != typeof(BasicAttack))
                continue;
            Debug.Log(turn + " used BasicAttack!");
            activeAction.setActive();
            setState(GameState.ALLY_ACTION_ACTIVE);
        }
    }

    public void skill()
    {
        for (int i = 1; i < turnActions.Count + 1; i++)
        {
            activeAction = turnActions[i - 1];
            if (turnActions[i - 1].GetType() == typeof(BasicAttack) ||
                turnActions[i - 1].GetType() == typeof(Move) ||
                turnActions[i - 1].GetType() == typeof(Pass))
                continue;

            Debug.Log(turn + " used " + turnActions[i - 1].GetType() + "!");
            activeAction.setActive();
            setState(GameState.ALLY_ACTION_ACTIVE);
        }
    }

    public void moveInDirection(string direction)
    {
        for (int i = 1; i < turnActions.Count + 1; i++)
        {
            activeAction = turnActions[i - 1];
            if (turnActions[i - 1].GetType() != typeof(Move))
                continue;

            Debug.Log("Moving " + direction);
            activeAction.setActive();
            Move moveAction = (Move)activeAction;
            moveAction.moveDirection(direction);
            setState(GameState.ALLY_ACTION_ACTIVE);
        }
    }
    #endregion
}