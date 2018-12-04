using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour {
	// DO NOT CHANGE state WITH "state = GameState.X"
	// INSTEAD, CHANGE state WITH "setState(GameState.X)"
	// OTHERWISE THINGS AREN'T UPDATED AND STUFF BREAKS
	public GameState state = GameState.INIT;
	public bool ongoing;

	private Character turn = null;
	private List<Action> turnActions = null;

	[Header("Why is this null")]
	public Action activeAction = null;
	private List<ActionType> actionTypesPerTurn = null;

	public StatusEffectManager statusManager;
	public DelayedActionManager delayedActionManager;

	private Field yourField;
	private Field enemyField;
	private List<EnemyCharacter> inactiveEnemies;
	private List<AllyCharacter> inactiveAllies;

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
		ongoing = true;

		Field[] grids = GameObject.FindObjectsOfType<Field>();

		if(grids[0].name == "Player_battleGround") {
			yourField = grids[0];
			enemyField = grids[1];
		} else {
			yourField = grids[1];
			enemyField = grids[0];
		}
		inactiveEnemies = new List<EnemyCharacter>();
		inactiveAllies = new List<AllyCharacter>();

		turnOrder = new List<Character>();
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
        GetComponent<UI_Manager>().updateTurn();
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
	
	public void addAllyCharacter(AllyCharacter c) {
		turnOrder.Add(c);

		yourField.addCharacter(c);
	}
	
	public void addEnemyCharacter(EnemyCharacter c) {
		if(enemyField.getNumCharacters() == 0)
			turnOrder.Add(c);
		else if(enemyField.getNumCharacters() == 1)
			turnOrder.Insert(2, c);
		else
			turnOrder.Insert(Mathf.Min(4, turnOrder.Count - 1), c);

		enemyField.addCharacter(c);
	}

	public void addEnemyCharacterDisabled(EnemyCharacter c) {
		inactiveEnemies.Add(c);
	}

	public void addAllyCharacterDisabled(AllyCharacter c) {
		inactiveAllies.Add(c);
	}

	public void updateGameStatus() {
		if(GameLoop.getInstance().getAllyField().getNumCharacters() == 0) {
			// stuff for losing
			Debug.Log("You lose.");
			ongoing = false;
		} else {
			bool foundAquarius = false;
            GameObject aqObj = GameObject.Find("Aquarius");
            if (aqObj)
            {
                Character aquarius = GameObject.Find("Aquarius").GetComponent<Character>();
                foreach (Tile t in GameLoop.getInstance().getEnemyField().gridPositions)
                    if (t.getCharacter() == aquarius)
                    {
                        foundAquarius = true;
                        break;
                    }
            }
            if (!foundAquarius)
            {
                // stuff for winning
                Debug.Log("You win.");
                ongoing = false;
            }
        }
	}

	public List<EnemyCharacter> getInactiveEnemies() {
		return inactiveEnemies;
	}

	public List<AllyCharacter> getInactiveAllies() {
		return inactiveAllies;
	}

	void Start() {
		
	}

	void Update() {
		if(ongoing) {
			if(delayedActionManager.inProgress()) {
				print("IP");
				return;
			}

			switch(state) {
				case GameState.INIT:
					// can randomize turn order
					if(randomizeTurnOrder) {
						List<Character> allies = new List<Character>();
						List<Character> enemies = new List<Character>();
						foreach(Character c in turnOrder)
							if(c.GetType() == typeof(AllyCharacter))
								allies.Add(c);
							else if(c.GetType() == typeof(EnemyCharacter))
								enemies.Add(c);

						List<Character> newAllies = new List<Character>();
						List<float> randList = new List<float>();
						for(int i = 0; i < allies.Count; i++)
							randList.Add(Random.Range(0.0f, 1.0f));

						for(int i = 0; i < allies.Count; i++) {
							int maxIdx = randList.IndexOf(Mathf.Max(randList.ToArray()));
							newAllies.Add(turnOrder[maxIdx]);
							randList[maxIdx] = 0;
						}

						turnOrder = new List<Character>();
						foreach(Character c in newAllies)
							turnOrder.Add(c);
						foreach(Character c in enemies)
							turnOrder.Add(c);
					}
					GetComponent<UI_Manager>().updateTurn();
					setState(GameState.START_TURN);
					break;
				case GameState.START_TURN:

					break;
				case GameState.ALLY_WAIT_INPUT:
					// keeps checking for keyboard input; eventually to be overwritten with ui
					for(int i = 1; i < turnActions.Count + 1; i++) {
						if(Input.GetKeyDown(GameLoop.keyCodeFromInt(i)) && !turnActions[i - 1].isActive()) {
							activeAction = turnActions[i - 1];
							if(turnActions[i - 1].GetType() == typeof(Pass))
								Debug.Log(turn + " passed.");
							else if(turnActions[i - 1].GetType() == typeof(Move))
								Debug.Log(turn + " beginning move.");
							else
								Debug.Log(turn + " used " + turnActions[i - 1].GetType() + "!");
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
						//GetComponent<UI_Manager>().resetMenus(actionTypesPerTurn.IndexOf(ActionType.MOVE) != -1);

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
						if(allyTurnOver()) {
							nextTurn();
						} else {
							setState(GameState.ALLY_WAIT_INPUT);
						}
						activeAction = null;
					}
					break;

				case GameState.ENEMY_MOVE:
					GetComponent<UI_Manager>().noMenus();
					if(!activeAction)
						findActiveAction();
					if(!activeAction.isActive()) {
						setState(GameState.ENEMY_ACT);
						print("set act");
					}
					break;
				case GameState.ENEMY_ACT:
//					Debug.Log("GameLoop Update() in ENEMY_STATE for " + turn);
//					Debug.Log("Chosen Action: " + activeAction + ".");
					GetComponent<UI_Manager>().noMenus();
					if(!activeAction)
						findActiveAction();
					if(!activeAction.isActive())
						nextTurn();
					break;
			}
		}
	}

    private void findActiveAction()
    {
        List<Action> potentialActions = ((EnemyCharacter)turn).getActions();
        foreach (Action potentialAction in potentialActions)
        {
            if (potentialAction.isActive())
                activeAction = potentialAction;
        }
    }

	private bool allyTurnOver() {
        return actionTypesPerTurn.Count == 0 ||
                (turnActions.Count == 0) ||
                (turnActions.Count == 2) ||
                activeAction.GetType() == typeof(Pass);
				//(turnActions.Count == 1 && turnActions[0].getActionType() == ActionType.PASS);
	}

    public void setState(string newState)
    {
		GameState strToState = GameState.START_TURN;
        if (newState.ToLower() == "start turn")
			strToState = GameState.START_TURN;

        switch (newState.ToLower())
        {
            case "start turn":
				strToState = GameState.START_TURN;
                break;
            case "ally wait input":
				strToState = GameState.ALLY_WAIT_INPUT;
                break;
            case "ally action active":
				strToState = GameState.ALLY_ACTION_ACTIVE;
                break;
            case "enemy state":
				strToState = GameState.ENEMY_MOVE;
                break;
        }

		setState(strToState);
    }

	public void setState(GameState newState) {
		// things to be done when exiting a state
		switch(state) {
			case GameState.START_TURN:			break;
			case GameState.ALLY_WAIT_INPUT:		break;
			case GameState.ALLY_ACTION_ACTIVE:	break;
			case GameState.ENEMY_MOVE:			break;
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

					setState(GameState.ALLY_WAIT_INPUT);
				} else {
					turnActions = ((EnemyCharacter)turn).getActions();
					actionTypesPerTurn = ((EnemyCharacter)turn).getActionTypesPerTurn();

					setState(GameState.ENEMY_MOVE);
				}
				GetComponent<UI_Manager>().resetMenus();

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

                bool moveActive = false;
                bool actionsActive = false;
                for (int i = 1; i < turnActions.Count + 1; i++)
                {
                    if (turnActions[i - 1].GetType() == typeof(Move))
                        moveActive = true;
                    else if (turnActions[i - 1].GetType() != typeof(Pass))
                        actionsActive = true;
                }
                GetComponent<UI_Manager>().resetAvailableActions(moveActive, actionsActive);

                break;
			case GameState.ALLY_ACTION_ACTIVE:
                //GetComponent<UI_Manager>().noMenus();
                break;
			case GameState.ENEMY_MOVE:
				if(((EnemyCharacter)turn).getEnemyCharacterType() == EnemyCharacterType.AQUARIUS) {
					TargetedMove move = turn.GetComponentInChildren<TargetedMove>();
					List<Tile> optimalTiles = move.getOptimalTiles();
					activeAction = move;
					move.setActiveTargeting(optimalTiles[Random.Range(0, optimalTiles.Count)]);
				} else {
					// minions have everything in act
					setState(GameState.ENEMY_ACT);
				}
				break;
			case GameState.ENEMY_ACT:
				Debug.Log("GameLoop entered ENEMY_STATE for " + turn);
				if(((EnemyCharacter)turn).getEnemyCharacterType() == EnemyCharacterType.AQUARIUS) {
					Action maxValued = ((EnemyCharacter)turn).getActions()[0];
					foreach(Action a in ((EnemyCharacter)turn).getActions()) {
						print(a + ": " + a.getValue());
						if(a.getValue() > maxValued.getValue())
							maxValued = a;
					}

//					for max value
					activeAction = maxValued;
//					for random	
					//activeAction = ((EnemyCharacter)turn).getActions()[Random.Range(0, ((EnemyCharacter)turn).getActions().Count)];
					
					print(activeAction.GetType());
					print(activeAction);
					activeAction.setActive();
				} else {
//					print("try basicattack");
					TargetedAction atk = turn.GetComponentInParent<TargetedBasicAttack>();
					activeAction = (atk.getValue() == 0)
										? turn.GetComponentInParent<TargetedMove>()
										: atk
										;

					((TargetedAction)activeAction).setActiveTargeting(((TargetedAction)activeAction).getOptimalTiles()[0]);
				}
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
		ENEMY_MOVE, ENEMY_ACT                   // enemy turn states
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
			print("activeAction " + activeAction);
            activeAction.setActive();
            setState(GameState.ALLY_ACTION_ACTIVE);
			break;
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
            //GetComponent<UI_Manager>().updateActionText(turnActions[i - 1].GetType().ToString());
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
            //GetComponent<UI_Manager>().updateActionText(turnActions[i - 1].GetType().ToString());
            activeAction.setActive();
            setState(GameState.ALLY_ACTION_ACTIVE);
        }
    }

    public void skill(int index)
    {
		print("skill idx");
        List<Action> potentialSkills = getActiveSkills();
        int grabIndex = index;
        if (index >= potentialSkills.Count)
            grabIndex = 0;

        activeAction = potentialSkills[grabIndex];
        Debug.Log(turn + " used " + potentialSkills[grabIndex].GetType() + "!");
        GetComponent<UI_Manager>().updateActionText(potentialSkills[grabIndex].GetType().ToString());
        activeAction.setActive();
        setState(GameState.ALLY_ACTION_ACTIVE);
    }

    public List<Action> getActiveSkills()
    {
        List<Action> skills = new List<Action>();
        for (int i = 1; i < turnActions.Count + 1; i++)
        {
            if (turnActions[i - 1].GetType() == typeof(BasicAttack) ||
                turnActions[i - 1].GetType() == typeof(Move) ||
                turnActions[i - 1].GetType() == typeof(Pass))
                continue;

            skills.Add(turnActions[i - 1]);
        }
        return skills;
    }

    public List<Action> getActiveActions()
    {
        List<Action> actions = new List<Action>();
        for (int i = 1; i < turnActions.Count + 1; i++)
        {
            if (turnActions[i - 1].GetType() == typeof(Move) ||
                turnActions[i - 1].GetType() == typeof(Pass))
                continue;

            actions.Add(turnActions[i - 1]);
        }
        return actions;
    }

    public void move()
    {
        Debug.Log("Moving");
        for (int i = 1; i < turnActions.Count + 1; i++)
        {
            if (turnActions[i - 1].GetType() != typeof(Move))
                continue;

            activeAction = turnActions[i - 1];
            //GetComponent<UI_Manager>().updateActionText(turnActions[i - 1].GetType().ToString());
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

    #endregion
}