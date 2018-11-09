using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : Character {
	[Header("Attributes")]
	public EnemyCharacterType type;

	void Start() {
		GameLoop.getInstance().addEnemyCharacter(this);
	}
	
	void Update() {
		
	}
}

public enum EnemyCharacterType {
	BOSS, MINION
}