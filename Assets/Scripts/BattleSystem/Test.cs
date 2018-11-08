using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {
	void Awake() {
		Debug.Log("awake" + this.name);
	}

	void Start() {
		Debug.Log("start" + this.name);
	}
	
	void Update() {
		Debug.Log(	fromBool(Input.GetKey(KeyCode.W)) +
					fromBool(Input.GetKey(KeyCode.A)) +
					fromBool(Input.GetKey(KeyCode.S)) +
					fromBool(Input.GetKey(KeyCode.D)));
	}

	string fromBool(bool b) {
		return (b) ? "T" : "F";
	}
}

/*
TO BE DELETED

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGameLoop : MonoBehaviour {
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
			if(TestGameLoop.DEBUG) yourField.printField();
			hasPrinted = true;
		}
		if(Input.GetKey(KeyCode.M)) {
			moveTest();
		} else if(Input.GetKey(KeyCode.N)) {
			attackTest();
		} else if(Input.GetKey(KeyCode.B)) {
			nextTurnTest();
		}
	}

    public void moveTest() {
        if(!GameObject.Find("Character1").GetComponent<Move>().isActive()) {
			Debug.Log("MOVE");
			GameObject.Find("Character1").GetComponent<Move>().setActive();
		} 
    }

	public void attackTest() {
		if(!GameObject.Find("Character1").GetComponent<BasicAttack>().isActive()) {
			Debug.Log("ATTACK");
			GameObject.Find("Character1").GetComponent<BasicAttack>().setActive();
		}
	}

	public void nextTurnTest() {
		nextTurn();
		Debug.Log("NEXT TURN");
	}
}
*/
