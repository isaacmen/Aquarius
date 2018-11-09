using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : Character {
	
	void Start() {
		GameLoop.getInstance().addEnemyCharacter(this);
	}
	
	void Update() {
		
	}
}
