using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MoveAction))]
public class Character : MonoBehaviour {
	[Header("Attributes")]
	public CharacterType type;
	public int maxHealth;
	public int health;
	public int basicAttackDamage;

	[Header("Actions")]
	public List<ActionType> actionList;

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
			if(!int.TryParse(this.name.Substring(l), out charN))
				Debug.Log("bad name for " + this.name);
		} else {
			GameLoop.addEnemyCharacter(this);
			int enemyN = 1;
			if(!int.TryParse(this.name.Substring("Enemy".Length), out enemyN))
				Debug.Log("bad name for " + this.name);
		}
	}

	public void takeDamage(int d) {
		health -= d;
	}
	
	void Update() {
		
	}

	private void instantiateOffType(CharacterType type) {
		actionList = new List<ActionType>() {
			ActionType.Move, ActionType.Pass, ActionType.BasicAttack
		};
		switch(type) {
			case CharacterType.Character1:
				maxHealth = 200;
				health = maxHealth;
				basicAttackDamage = 6;
				GameObject.Find(this.name).AddComponent<MoveAction>();
				GameObject.Find(this.name).AddComponent<BasicAttackAction>();
				actionList.AddRange(new List<ActionType>() {
					ActionType.Char1Ability1
				});
				break;
			case CharacterType.Character2:
				maxHealth = 150;
				health = maxHealth;
				basicAttackDamage = 8;
				GameObject.Find(this.name).AddComponent<MoveAction>();
				GameObject.Find(this.name).AddComponent<BasicAttackAction>();
				actionList.AddRange(new List<ActionType>() {
					ActionType.Char2Ability1
				});
				break;
			case CharacterType.Character3:
				maxHealth = 100;
				health = maxHealth;
				basicAttackDamage = 10;
				GameObject.Find(this.name).AddComponent<MoveAction>();
				GameObject.Find(this.name).AddComponent<BasicAttackAction>();
				actionList.AddRange(new List<ActionType>() {
					ActionType.Char1Ability1
				});
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