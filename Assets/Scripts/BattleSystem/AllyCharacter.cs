using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyCharacter : Character {
	[Header("Attributes")]
	public AllyCharacterType type;
	
	private List<Action> actionList;
	private List<ActionType> actionTypesPerTurn;

	override protected void Awake() {
		base.Awake();

		instantiateActionsFor(type);
	}

	override protected void Start() {
		base.Start();

		GameLoop.getInstance().addAllyCharacter(this);
	}

	public List<Action> getActions() {
		List<Action> actionsWithUses = new List<Action>();
		foreach(Action a in actionList)
			if(a.usesLeft() > 0)
				actionsWithUses.Add(a);
		return actionsWithUses;
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

				actionList.Add(GameObject.Find(this.name).AddComponent<Guard>());
				actionList.Add(GameObject.Find(this.name).AddComponent<LayOnHands>());
				actionList.Add(GameObject.Find(this.name).AddComponent<ShieldBash>());

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