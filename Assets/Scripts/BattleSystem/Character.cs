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
	public List<ActionType> actionList;

	void Awake() {
		print(this.name + " awake");
		instantiateOffType(type);
	}

	void Start() {
		print(this.name + " start");
		int l = "Character".Length;
		if(this.name.Length > l && this.name.Substring(0, l).Equals("Character"))
			GameLoop.addAllyCharacter(this);
		else
			GameLoop.addEnemyCharacter(this);
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
			case CharacterType.Libra:
				GameObject.Find(this.name).AddComponent<MoveAction>();
				GameObject.Find(this.name).AddComponent<BasicAttackAction>();
				actionList.AddRange(new List<ActionType>() {
					ActionType.Char1Ability1
				});
				break;
			case CharacterType.Leo:
				GameObject.Find(this.name).AddComponent<MoveAction>();
				GameObject.Find(this.name).AddComponent<BasicAttackAction>();
				actionList.AddRange(new List<ActionType>() {
					ActionType.Char2Ability1
				});
				break;
			case CharacterType.Scorpio:
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

	public override string ToString() {
		return this.name;
	}
}

public enum CharacterType {
	Unknown,
	Libra, Leo, Scorpio
}