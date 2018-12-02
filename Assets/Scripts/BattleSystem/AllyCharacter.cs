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
				actionList.Add(GameObject.Find(this.name).AddComponent<Lullaby>());
				actionList.Add(GameObject.Find(this.name).AddComponent<Crescendo>());

				break;
			case AllyCharacterType.Scorpio:
				actionList.Add(GameObject.Find(this.name).AddComponent<Move>());
				actionList.Add(GameObject.Find(this.name).AddComponent<BasicAttack>());

				actionList.Add(GameObject.Find(this.name).AddComponent<Fireball>());
				actionList.Add(GameObject.Find(this.name).AddComponent<PowerWordKill>());
				GameObject.Find(this.name).AddComponent<PowerWordKill2>(); // the delayed 2ndary part
				actionList.Add(GameObject.Find(this.name).AddComponent<LightningBolt>());

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