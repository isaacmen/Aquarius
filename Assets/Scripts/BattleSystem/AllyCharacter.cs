using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyCharacter : Character {
	[Header("Attributes")]
	public AllyCharacterType type;

//	[Header("Actions")]
	private List<Action> actionList;
	private List<ActionType> actionTypesPerTurn;

	void Awake() {
		if(GameLoop.DEBUG_LOG) print(this.name + " awake");
		instantiateActionsFor(type);
	}

	void Start() {
		if(GameLoop.DEBUG_LOG) print(this.name + " start");
		GameLoop.getInstance().addAllyCharacter(this);
	}

	public List<Action> getActions() {
		return new List<Action>(actionList);
	}

	public List<ActionType> getActionTypesPerTurn() {
		return new List<ActionType>(actionTypesPerTurn);
	}
	
	void Update() {
		
	}

	private void instantiateActionsFor(AllyCharacterType type) {
		actionList = new List<Action>();
		actionList.Add(GameObject.Find(this.name).AddComponent<Pass>());

		actionTypesPerTurn = new List<ActionType>() { ActionType.ABILITY, ActionType.MOVE };

		switch(type) {
			case AllyCharacterType.Libra:
				actionList.Add(GameObject.Find(this.name).AddComponent<Move>());
				actionList.Add(GameObject.Find(this.name).AddComponent<BasicAttack>());

				actionList.Add(GameObject.Find(this.name).AddComponent<LayOnHands>());

				break;
			case AllyCharacterType.Leo:
				actionList.Add(GameObject.Find(this.name).AddComponent<Move>());
				actionList.Add(GameObject.Find(this.name).AddComponent<BasicAttack>());

				actionList.Add(GameObject.Find(this.name).AddComponent<Unload>());

				break;
			case AllyCharacterType.Scorpio:
				actionList.Add(GameObject.Find(this.name).AddComponent<Move>());
				actionList.Add(GameObject.Find(this.name).AddComponent<BasicAttack>());

				actionList.Add(GameObject.Find(this.name).AddComponent<Fireball>());

				break;
		}
	}

	public override string ToString() {
		return this.name;
	}
}

public enum AllyCharacterType {
	Libra, Leo, Scorpio
}