using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {
	[Header("Attributes")]
	public CharacterType type;
	public int maxHealth;
	public int health;
	public int basicAttackDamage;

//	[Header("Actions")]
	private List<Action> actionList;
	private List<ActionType> actionTypesPerTurn;

	void Awake() {
		if(GameLoop.DEBUG_LOG) print(this.name + " awake");
		instantiateActionsFor(type);
	}

	void Start() {
		if(GameLoop.DEBUG_LOG) print(this.name + " start");
		int l = "Character".Length;
		if(this.name.Length > l && this.name.Substring(0, l).Equals("Character"))
			GameLoop.addAllyCharacter(this);
		else
			GameLoop.addEnemyCharacter(this);
	}

	public List<Action> getActions() {
		return new List<Action>(actionList);
	}

	public List<ActionType> getActionTypesPerTurn() {
		return new List<ActionType>(actionTypesPerTurn);
	}

	public void takeDamage(int d) {
		health -= d;
	}
	
	void Update() {
		
	}

	private void instantiateActionsFor(CharacterType type) {
		actionList = new List<Action>();
		actionList.Add(GameObject.Find(this.name).AddComponent<Pass>());

		actionTypesPerTurn = new List<ActionType>() { ActionType.ABILITY, ActionType.MOVE };

		switch(type) {
			case CharacterType.Libra:
				actionList.Add(GameObject.Find(this.name).AddComponent<Move>());
				actionList.Add(GameObject.Find(this.name).AddComponent<BasicAttack>());

				actionList.Add(GameObject.Find(this.name).AddComponent<LayOnHands>());

				break;
			case CharacterType.Leo:
				actionList.Add(GameObject.Find(this.name).AddComponent<Move>());
				actionList.Add(GameObject.Find(this.name).AddComponent<BasicAttack>());

				actionList.Add(GameObject.Find(this.name).AddComponent<Unload>());

				break;
			case CharacterType.Scorpio:
				actionList.Add(GameObject.Find(this.name).AddComponent<Move>());
				actionList.Add(GameObject.Find(this.name).AddComponent<BasicAttack>());

				actionList.Add(GameObject.Find(this.name).AddComponent<Fireball>());

				break;
			case CharacterType.Enemy:
				actionList.Add(GameObject.Find(this.name).AddComponent<Move>());

				//

				break;
		}
	}

	public override string ToString() {
		return this.name;
	}
}

public enum CharacterType {
	Enemy,
	Libra, Leo, Scorpio
}