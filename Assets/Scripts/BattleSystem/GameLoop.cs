using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour {
	private static Field yourField;
	private static Field enemyField;
	private bool hasPrinted = false;

	void Awake() {
		Debug.Log("GameLoop awake");
		yourField = new Field();
		enemyField = new Field();
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
	void Start () {
		
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
		}
	}
}
