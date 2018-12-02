using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour {
	// DO NOT CHANGE state WITH "state = GameState.X"
	// INSTEAD, CHANGE state WITH "setState(GameState.X)"
	// OTHERWISE THINGS AREN'T UPDATED AND STUFF BREAKS
	public GameState state = GameState.INIT;

	private Character turn = null;
	private List<Action> turnActions = null;
	private Action activeAction = null;
	private List<ActionType> actionTypesPerTurn = null;

	public StatusEffectManager statusManager;
	public DelayedActionManager delayedActionManager;

	private Field yourField;
	private Field enemyField;

	[Header("Turn")]
    public List<Character> turnOrder;
    public bool randomizeTurnOrder;

	[Header("Constants")]
	public bool DEBUG_LOG;

	public static GameLoop getInstance() {
		return GameObject.Find("GameLoop").GetComponent<GameLoop>();
	}

	void Awake() {
		if(DEBUG_LOG) Debug.Log("GameLoop awake");

		Field[] grids = GameObject.FindObjectsOfType<Field>();

		if(grids[0].name == "Player_battleGround") {
			yourField = grids[0];
			enemyField = grids[1];
		} else {
			yourField = grids[1];
			enemyField = grids[0];
		}

		//turnOrder = new List<Character>();
		statusManager = new StatusEffectManager();
		delayedActionManager = new DelayedActionManager();
	}

	public Field getAllyField() {
		return yourField;
	}

	public Field getEnemyField() {
		return enemyField;
	}

	// could also notify ui
	public void nextTurn() {
		Character turnDone = turnOrder[0];
		turnOrder.Remove(turnDone);
		turnOrder.Add(turnDone);
		setState(GameState.START_TURN);
//        GetComponent<UI_Manager>().updateTurn();
    }

	// may be unnecessary
	public Character getCharacterTurn() {
		return (turnOrder.Count > 0) ? turnOrder[0] : null;
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
		yourField.initAddCharacter(c);
	}

	// to be implemented with real fields
	public void addEnemyCharacter(EnemyCharacter c) {
		turnOrder.Add(c);
		enemyField.initAddCharacter(c);
	}

	void Start() {
		
	}

	void Update() {
		if(delayedActionManager.inProgress()) {
			print("IP");
			return;
		}

        switch (state) {
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
                //GetComponent<UI_Manager>().resetMenus(true);
				break;
			case GameState.ALLY_WAIT_INPUT:
				// keeps checking for keyboard input; eventually to be overwritten with ui
				for(int i = 1; i < turnActions.Count + 1; i++) {
					if(Input.GetKeyDown(GameLoop.keyCodeFromInt(i)) && !turnActions[i - 1].isActive()) {
						activeAction = turnActions[i-1];
						if(turnActions[i-1].GetType() == typeof(Pass))
							Debug.Log(turn + " passed.");
						else if(turnActions[i-1].GetType() == typeof(Move))
							Debug.Log(turn + " beginning move.");
						else
							Debug.Log(turn + " used " + turnActions[i-1].GetType() + "!");
						activeAction.setActive();
						setState(GameState.ALLY_ACTION_ACTIVE);
					}
				}
                
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
//                    GetComponent<UI_Manager>().resetMenus(actionTypesPerTurn.IndexOf(ActionType.MOVE) != -1);

                    // if there are no more abilities in the turn, remove
                    // all actions with ActionType.ABILITY from turnActions
                    if (actionTypesPerTurn.IndexOf(ActionType.ABILITY) == -1) {
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
                    turnMoveStr += (i + 1) + ": " + turnActions[i].GetType() + ((turnActions[i].usesLeft() > 999999) ? "" : " (" + turnActions[i].usesLeft()+")") + ((i < turnActions.Count - 1) ? " / " : "");
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
		// things to be done when exiting a state
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
				do {
					turn = getCharacterTurn();
					Debug.Log(turn.name + "'s Turn!");
					statusManager.onTurn(turn);
					if(turn.statusEffects.Contains(StatusEffect.STUNNED)) {
						Debug.Log(turn.name + " is stunned!");
						Character turnDone = turnOrder[0];
						turnOrder.Remove(turnDone);
						turnOrder.Add(turnDone);
					}
				} while(turn.statusEffects.Contains(StatusEffect.STUNNED));
				delayedActionManager.onTurn(turn);

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
					turnMoveStr += (i + 1) + ": " + turnActions[i].GetType() + ((turnActions[i].usesLeft() > 999999) ? "" : " (" + turnActions[i].usesLeft() + ")") + ((i < turnActions.Count - 1) ? " / " : "");
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
				GameObject.Find("Libra").GetComponent<Character>().takeDamage(50);
				break;
		}
	}

	// is there a better way to do this than to hard-code?
	private static KeyCode keyCodeFromInt(int n) {
		switch(n) {
			case 1: return KeyCode.Alpha1;
			case 2: return KeyCode.Alpha2;
			case 3: return KeyCode.Alpha3;
			case 4: return KeyCode.Alpha4;
			case 5: return KeyCode.Alpha5;
			case 6: return KeyCode.Alpha6;
			case 7: return KeyCode.Alpha7;
			case 8: return KeyCode.Alpha8;
			case 9: return KeyCode.Alpha9;
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
            GetComponent<UI_Manager>().updateActionText(turnActions[i - 1].GetType().ToString());
            Debug.Log(turn + " passed.");
            activeAction.setActive();
            setState(GameState.ALLY_ACTION_ACTIVE);
        }
    }

    public void attack()
    {
        for (int i = 1; i < turnActions.Count + 1; i++)
        {
            if (turnActions[i - 1].GetType() != typeof(BasicAttack))
                continue;
            
            activeAction = turnActions[i - 1];
            Debug.Log(turn + " used BasicAttack!");
            GetComponent<UI_Manager>().updateActionText(turnActions[i - 1].GetType().ToString());
            activeAction.setActive();
            setState(GameState.ALLY_ACTION_ACTIVE);
        }
    }

    public void skill()
    {
        for (int i = 1; i < turnActions.Count + 1; i++)
        {
            if (turnActions[i - 1].GetType() == typeof(BasicAttack) ||
                turnActions[i - 1].GetType() == typeof(Move) ||
                turnActions[i - 1].GetType() == typeof(Pass))
                continue;

            activeAction = turnActions[i - 1];
            Debug.Log(turn + " used " + turnActions[i - 1].GetType() + "!");
            GetComponent<UI_Manager>().updateActionText(turnActions[i - 1].GetType().ToString());
            activeAction.setActive();
            setState(GameState.ALLY_ACTION_ACTIVE);
        }
    }

    public void move()
    {
        Debug.Log("Moving");
        for (int i = 1; i < turnActions.Count + 1; i++)
        {
            if (turnActions[i - 1].GetType() != typeof(Move))
                continue;

            activeAction = turnActions[i - 1];
            GetComponent<UI_Manager>().updateActionText(turnActions[i - 1].GetType().ToString());
            activeAction.setActive();
            setState(GameState.ALLY_ACTION_ACTIVE);
            Debug.Log(turn + " beginning move.");
        }
    }

    public void cancelAction()
    {
        if (activeAction)
        {
            Debug.Log("Canceling... " + activeAction.name);
            if (activeAction.GetType() != typeof(PromptingAction))
            {
                PromptingAction activePrompt = (PromptingAction) activeAction;
                activePrompt.cancelAction();
            }
            activeAction.setInactiveWithCompletion(false);
        }
        else
        {
            Debug.Log("No action to cancel...");
        }

    }

    /*
    public void moveInDirection(string direction)
    {
        for (int i = 1; i < turnActions.Count + 1; i++)
        {
            activeAction = turnActions[i - 1];
            if (turnActions[i - 1].GetType() != typeof(Move))
                continue;

            //Debug.Log("Moving " + direction);
            GetComponent<UI_Manager>().updateActionText(turnActions[i - 1].GetType().ToString());
            activeAction.setActive();
            Move moveAction = (Move)activeAction;
            moveAction.moveDirection(direction);
            setState(GameState.ALLY_ACTION_ACTIVE);
        }
    }*/
    #endregion
}