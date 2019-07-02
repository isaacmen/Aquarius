using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : Character {	
	[Header("Attributes")]
	public EnemyCharacterType type;
    public GameObject healthbar;

    private List<Action> actionList;
    private List<ActionType> actionTypesPerTurn;

	override protected void Awake() {
        base.Awake();
        instantiateActionsFor(type);
	}

	override protected void Start() {
        base.Start();

        if (onField) {
			this.gameObject.SetActive(true);
			GameLoop.getInstance().addEnemyCharacter(this);
		} else {
			this.gameObject.SetActive(false);
			GameLoop.getInstance().addEnemyCharacterDisabled(this);
		}
	}

    private void OnEnable()
    {
        healthbar.SetActive(true);
    }

    public void setOnField(bool to) {
		if(to) {
			this.gameObject.SetActive(true);
			GameLoop.getInstance().addEnemyCharacter(this);
			health = maxHealth;
		} else {
			this.gameObject.SetActive(false);
			GameLoop.getInstance().addEnemyCharacterDisabled(this);
		}
	}

	public List<Action> getActions() {
		return new List<Action>(actionList);
	}

	public List<ActionType> getActionTypesPerTurn() {
		return new List<ActionType>(actionTypesPerTurn);
	}

	private void instantiateActionsFor(EnemyCharacterType type) {
        actionList = new List<Action>();
		//below is unnecessary as statuseffects handle that
//      actionList.Add(GameObject.Find(this.name).AddComponent<Pass>()); //for when they are forced to wait for their turn;
        actionList.Add(GameObject.Find(this.name).AddComponent<TargetedMove>());
        actionList.Add(GameObject.Find(this.name).AddComponent<TargetedBasicAttack>());

        actionTypesPerTurn = new List<ActionType>() { ActionType.ABILITY, ActionType.MOVE};

        if(type == EnemyCharacterType.AQUARIUS) {
			actionList.Add(GameObject.Find(this.name).AddComponent<SummonWaterElementals>());
			actionList.Add(GameObject.Find(this.name).AddComponent<TidalWaveX>());
			actionList.Add(GameObject.Find(this.name).AddComponent<TidalWaveO>());
			actionList.Add(GameObject.Find(this.name).AddComponent<TidalWaveRow>());
			actionList.Add(GameObject.Find(this.name).AddComponent<TidalWaveColumn>());
            GameObject.Find(this.name).AddComponent<ShapeAction2>(); // the delayed 2ndary part
        }

    }

	public EnemyCharacterType getEnemyCharacterType() {
		return type;
	}
}

public enum EnemyCharacterType {
	AQUARIUS,
	RANGED_MINION, MELEE_MINION
}