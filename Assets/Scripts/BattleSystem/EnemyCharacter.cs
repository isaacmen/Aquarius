using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : Character {
	[Header("Attributes")]
	public EnemyCharacterType type;

    private List<Action> actionList;
    private List<ActionType> actionTypesPerTurn;

	override protected void Awake() {
        base.Awake();

        instantiateActionsFor(type);
	}

	override protected void Start() {
        base.Start();

		GameLoop.getInstance().addEnemyCharacter(this);
	}
	
	void Update() {
		
	}

    private void instantiateActionsFor(EnemyCharacterType type)
    {
        actionList = new List<Action>();
        actionList.Add(GameObject.Find(this.name).AddComponent<Pass>()); //for when they are forced to wait for their turn;
        actionList.Add(GameObject.Find(this.name).AddComponent<Move>());
        actionList.Add(GameObject.Find(this.name).AddComponent<BasicAttack>());

        actionTypesPerTurn = new List<ActionType>() { ActionType.ABILITY, ActionType.MOVE};

        if (type == EnemyCharacterType.AQUARIUS)
        {
            /*
             * Insert Aquarius major skills here.
             * These will be what is called from the AI.
             */ 
        }
    }
}

public enum EnemyCharacterType {
	AQUARIUS,
	RANGED_MINION, MELEE_MINION
}