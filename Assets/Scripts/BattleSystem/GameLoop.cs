using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour {
	private static Field yourField;
	private static Field enemyField;
	private bool hasPrinted = false;

	[Header("Turn Order")]
	public List<Character> turnOrder;
	public bool randomizeTurnOrder;

	void Awake() {
		Debug.Log("GameLoop awake");
		yourField = new Field();
		enemyField = new Field();

		if(randomizeTurnOrder) {
			List<Character> newTurnOrder = new List<Character>();
			List<float> randList = new List<float>();
			for(int i = 0; i < turnOrder.Count; i++) {
				randList.Add(Random.Range(0.0f, 1.0f));
				Debug.Log(randList[i]);
			}

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
		print(c.name);
		yourField.addCharacter(c);
//		GameObject.Find(c.name).AddComponent<Character>();
	}

	public static void addEnemyCharacter(Character c) {
		enemyField.addCharacter(c);
	}

	// Use this for initialization
	void Start() {
		
	}
	
	// Update is called once per frame
	void Update() {
		if(!hasPrinted) {
			yourField.print();
			hasPrinted = true;
		}
		if(Input.GetKey(KeyCode.M)) {
			if(GameObject.Find("Character1").GetComponent<MoveAction>().setActive(true))
				Debug.Log("MOVE");
		} else if(Input.GetKey(KeyCode.N)) {
			if(GameObject.Find("Character1").GetComponent<BasicAttackAction>().setActive(true))
				Debug.Log("ATTACK");
		} else if(Input.GetKey(KeyCode.B)) {
			nextTurn();
			Debug.Log("TURN");
		}
	}

    public void moveCharacter()
    {
        if (GameObject.Find("Character1").GetComponent<MoveAction>().setActive(true))
            Debug.Log("MOVE");
    }
}
