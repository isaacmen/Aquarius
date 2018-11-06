using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour {
	private State state = State.START_TURN;
	public Character turn = null;
	public List<Action> turnActions = null;
	public bool hasMoved = false;
	public bool hasActed = false;

	private static Field yourField;
	private static Field enemyField;
	private bool hasPrinted = false;

	[Header("Turn Order")]
	public List<Character> turnOrder;
	public bool randomizeTurnOrder;

	[Header("Constants")]
	public static bool DEBUG = false;

	void Awake() {
		if(TestGameLoop.DEBUG) Debug.Log("GameLoop awake");
		yourField = new Field();
		enemyField = new Field();

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

	public void nextTurn() {
		Character turn = turnOrder[0];
		turnOrder.Remove(turn);
		turnOrder.Add(turn);
	}

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

	public static void addAllyCharacter(Character c) {
		//yourField.addCharacter(c);
//		GameObject.Find(c.name).AddComponent<Character>();
	}

	public static void addEnemyCharacter(Character c) {
		//enemyField.addCharacter(c);
	}
	
	void Start() {
		
	}
	
	void Update() {
		switch(state) {
			case State.START_TURN:
				turn = getCharacterTurn();
				turnActions = turn.actionList;

				Debug.Log(turn + "'s Turn");
				for(int i = 0; i < turnActions.Count; i++) {
					Debug.Log((i+1) + ": " + turnActions[i].GetType());
				}
				
				hasMoved = false;
				hasActed = false;
				
				state = State.WAIT_INPUT;

				break;
			case State.WAIT_INPUT:

				break;
		}
	}

    public void turnMove() {
        if(turn.GetComponent<Move>().setActive(true))
            Debug.Log("TURN MOVE");
    }

	public void turnAttack() {
		if(GameObject.Find("Libra").GetComponent<BasicAttack>().setActive(true))
			Debug.Log("TURN ATTACK");
	}

	public void nextTurnTest() {
		nextTurn();
		Debug.Log("NEXT TURN");
	}

	private enum State {
		START_TURN, WAIT_INPUT, 
	}
}
