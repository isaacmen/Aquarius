using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : Character {
	[Header("Attributes")]
	public EnemyCharacterType type;

	private void Awake() {
		if(GameLoop.getInstance().DEBUG_LOG) print(this.name + " awake");
	}

	void Start() {
		if(GameLoop.getInstance().DEBUG_LOG) print(this.name + " start");
		GameLoop.getInstance().addEnemyCharacter(this);
	}
	
	void Update() {
		
	}
}

public enum EnemyCharacterType {
	AQUARIUS,
	RANGED_MINION, MELEE_MINION
}