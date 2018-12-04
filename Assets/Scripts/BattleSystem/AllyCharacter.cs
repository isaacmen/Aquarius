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

		if(onField) {
			this.gameObject.SetActive(true);
			GameLoop.getInstance().addAllyCharacter(this);
		} else {
			this.gameObject.SetActive(false);
			GameLoop.getInstance().addAllyCharacterDisabled(this);
		}
	}

	public void setOnField(bool to) {
		if(to) {
			this.gameObject.SetActive(true);
			GameLoop.getInstance().addAllyCharacter(this);
			health = maxHealth;
		} else {
			this.gameObject.SetActive(false);
			GameLoop.getInstance().addAllyCharacterDisabled(this);
		}
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
		
		print(AbilityCatalog.powerWordKill);
		print(AbilityCatalog.lightning);
		print(AbilityCatalog.crescendo);
		print(AbilityCatalog.arpeggioPocoAPoco);
		print(AbilityCatalog.layOnHands);
		print(AbilityCatalog.shieldBash);

		actionList.Add(GameObject.Find(this.name).AddComponent<Move>());
		actionList.Add(GameObject.Find(this.name).AddComponent<BasicAttack>());

		switch(type) {
			case AllyCharacterType.Libra:
				actionList.Add(GameObject.Find(this.name).AddComponent<Guard>());

				if(AbilityCatalog.layOnHands || !AbilityCatalog.shieldBash)
					actionList.Add(GameObject.Find(this.name).AddComponent<LayOnHands>());
				if(AbilityCatalog.shieldBash || !AbilityCatalog.layOnHands)
					actionList.Add(GameObject.Find(this.name).AddComponent<ShieldBash>());

				break;
			case AllyCharacterType.Leo:
				actionList.Add(GameObject.Find(this.name).AddComponent<Lullaby>());

				if(AbilityCatalog.arpeggioPocoAPoco || !AbilityCatalog.crescendo)
					actionList.Add(GameObject.Find(this.name).AddComponent<Unload>());
				if(AbilityCatalog.crescendo || !AbilityCatalog.arpeggioPocoAPoco)
					actionList.Add(GameObject.Find(this.name).AddComponent<Crescendo>());

				break;
			case AllyCharacterType.Scorpio:
				actionList.Add(GameObject.Find(this.name).AddComponent<Fireball>());

				if(AbilityCatalog.powerWordKill || !AbilityCatalog.lightning) {
					actionList.Add(GameObject.Find(this.name).AddComponent<PowerWordKill>());
					GameObject.Find(this.name).AddComponent<PowerWordKill2>(); // the delayed 2ndary part
				}
				if(AbilityCatalog.lightning || !AbilityCatalog.powerWordKill)
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