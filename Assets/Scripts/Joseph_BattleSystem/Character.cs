using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {
	[Header("Attributes")]
	public CharacterType type;
	public int maxHealth;
	public int health;
	public int basicAttackDamage;

	[Header("Actions")]
	public Dictionary<ActionType, bool> actionDict;

	void Awake() {
		print(this.name + " awake");
		type = typeFromString(this.name);
		instantiateOffType(type);
	}

	void Start() {
		print(this.name + " start");
		int l = "Character".Length;
		if(this.name.Length > l && this.name.Substring(0, l).Equals("Character")) {
			GameLoop.addAllyCharacter(this);
			int charN = -1;
			if(int.TryParse(this.name.Substring(l), out charN))
				transform.position = new Vector3(charN, 0, -10);
			else
				Debug.Log("bad name for " + this.name);
		} else {
			GameLoop.addEnemyCharacter(this);
			int enemyN = 1;
			if(int.TryParse(this.name.Substring("Enemy".Length), out enemyN))
				transform.position = new Vector3(-enemyN, 0, -10);
			else
				Debug.Log("bad name for " + this.name);
		}
	}

	public void takeDamage(int d) {
		health -= d;
	}
	
	void Update() {
		
	}

	private void instantiateOffType(CharacterType type) {
		actionDict = new Dictionary<ActionType, bool>();
		actionDict.Add(ActionType.Move, false);
		actionDict.Add(ActionType.Pass, false);
		actionDict.Add(ActionType.BasicAttack, false);
		switch(type) {
			case CharacterType.Character1:
				maxHealth = 200;
				health = maxHealth;
				basicAttackDamage = 6;
				GameObject.Find(this.name).AddComponent<MoveAction>();
				GameObject.Find(this.name).AddComponent<BasicAttackAction>();
				actionDict.Add(ActionType.Char1Ability1, false);
				break;
			case CharacterType.Character2:
				maxHealth = 150;
				health = maxHealth;
				basicAttackDamage = 8;
				actionDict.Add(ActionType.Char2Ability1, false);
				break;
			case CharacterType.Character3:
				maxHealth = 100;
				health = maxHealth;
				basicAttackDamage = 10;
				actionDict.Add(ActionType.Char3Ability1, false);
				break;
			default:
				Debug.Log("Instantiation From Invalid CharacterType " + type);
				break;
		}
	}

	private CharacterType typeFromString(string s) {
		foreach(CharacterType type in (CharacterType[])System.Enum.GetValues(typeof(CharacterType)))
			if(type.ToString().Equals(this.name))
				return type;
		Debug.Log("Invalid CharacterType From Name " + s);
		return CharacterType.Unknown;
	}
}

public enum CharacterType {
	Unknown,
	Character1, Character2, Character3
}