using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour {
	// DO NOT CHANGE state WITH "state = GameState.X"
	// INSTEAD, CHANGE state WITH "setState(GameState.X)"
	// OTHERWISE THINGS AREN'T UPDATED AND STUFF BREAKS
	private GameState state = GameState.INIT;

	private Character turn = null;
	private List<Action> turnActions = null;
	private Action activeAction = null;
	private bool hasMoved = false;
	private bool hasActed = false;

	private static Field yourField;
	private static Field enemyField;

	[Header("Turn")]
	public List<Character> turnOrder;
	public bool randomizeTurnOrder;

	[Header("Constants")]
	public static bool DEBUG_LOG = false;

	void Awake() {
		if(DEBUG_LOG) Debug.Log("GameLoop awake");

		// to be implemented to be set to real fields from GameObject.Find(nameOfField)
		yourField = new Field();
		enemyField = new Field();

		// can randomize turn order
			// still need to implement automatic finding of characters
			// probably will add to GameLoop.addAllyCharacter(Character c)
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
	}

	// could also notify gui
	public void nextTurn() {
		Character turn = turnOrder[0];
		turnOrder.Remove(turn);
		turnOrder.Add(turn);
	}

	// may be unnecessary
	public Character getCharacterTurn() {
		return turnOrder[0];
	}

	private void printTurnOrder() {
		string toPrint = "";
		foreach(Character c in turnOrder) {
			toPrint += c.ToString() + " ";
		}
		Debug.Log(toPrint);
	}

	// to be implemented with real fields
	public static void addAllyCharacter(Character c) {
		yourField.addCharacter(c);
	}

	// to be implemented with real fields
	public static void addEnemyCharacter(Character c) {
		enemyField.addCharacter(c);
	}
	
	void Start() {
		setState(GameState.START_TURN);
	}
	
	void Update() {
		switch(state) {
			case GameState.START_TURN:
				break;
			case GameState.WAIT_INPUT:
				// keeps checking for keyboard input; eventually to be overwritten with gui
				for(int i = 1; i < turnActions.Count+1; i++) {
					if(Input.GetKeyDown(keyCodeFromInt(i)) && !turnActions[i-1].isActive()) {
						activeAction = turnActions[i-1];
						activeAction.setActive();
						if(DEBUG_LOG) Debug.Log("activating " + turnActions[i-1].GetType());
						setState(GameState.ACTION_ACTIVE);
					}
						
				}
				break;
			case GameState.ACTION_ACTIVE:
				// keeps checking for action to not be active
				if(!activeAction.isActive()) {
					if(DEBUG_LOG) Debug.Log("GameLoop detects action inactive");

					// updates hasMoved and hasActed if the action was fully completed and executed
					if(activeAction.getCompletion())
						if(activeAction.GetType() == typeof(Move)) {
							hasMoved = true;
						} else if(activeAction.GetType() == typeof(Pass)) {
							hasMoved = true;
							hasActed = true;
						} else {
							hasActed = true;
						}

					// updates turnActions based off hasMoved and hasActed
					if(hasMoved) {
						turnActions.Remove(turn.GetComponent<Move>());
					}
					if(hasActed) {
						for(int i = 0; i < turnActions.Count; i++) {
							if(turnActions[i].GetType() != typeof(Move) && turnActions[i].GetType() != typeof(Pass)) {
								turnActions.Remove(turnActions[i]);
								i--;
							}
						}
					}

					// checks if turn is over
					if(turnOver()) {
						nextTurn();
						setState(GameState.START_TURN);
					} else {
						setState(GameState.WAIT_INPUT);
					}
					activeAction = null;
				}
				break;
		}
	}

	private bool turnOver() {
		return	(hasMoved && hasActed) ||
				(turnActions.Count == 0) ||
				(turnActions.Count == 1 && turnActions[0].GetType() == typeof(Pass));
	}

	public void setState(GameState newState) {
		// things to be done when exiting a state (nothing so far or maybe won't be needed)
		switch(state) {
			case GameState.START_TURN:		break;
			case GameState.WAIT_INPUT:		break;
			case GameState.ACTION_ACTIVE:	break;
		}

		// update state
		state = newState;

		// things to be done when entering a state
		switch(state) {
			case GameState.START_TURN:
				// resets variables for each turn
				turn = getCharacterTurn();
				turnActions = turn.getActions();

				hasMoved = false;
				hasActed = false;

				Debug.Log(turn + "'s Turn");

				setState(GameState.WAIT_INPUT);

				break;
			case GameState.WAIT_INPUT:
				// prints moves and waits for the user to select one - to be partially overridden by implemention with gui
				string turnMoveStr = "What should " + turn + " do? (press # to act)\n";
				for(int i = 0; i < turnActions.Count; i++) {
					turnMoveStr += (i + 1) + ": " + turnActions[i].GetType() + ((i < turnActions.Count-1) ? " / " : "");
				}
				Debug.Log(turnMoveStr);
				if(DEBUG_LOG) Debug.Log("hasMoved " + hasMoved + ", hasActed " + hasActed);
				break;
			case GameState.ACTION_ACTIVE:
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
			default:
				Debug.Log("number " + n + " not implemented yet");
				return KeyCode.Alpha0;
		}
	}

	public enum GameState {
		INIT, START_TURN, WAIT_INPUT, ACTION_ACTIVE
	}
}
