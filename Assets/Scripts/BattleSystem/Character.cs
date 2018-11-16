using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {
	[Header("Attributes")]
	public int maxHealth;
	public int health;
	public int basicAttackDamage;
	public int gridPosition;
	
	void Start() {
		gameObject.transform.position = GameObject.Find("Enemy_battleGround").GetComponent<Grid_Behavior>().gridPositions[gridPosition];

	}
	
	void Update() {
		
	}

	public void takeDamage(int d) {
		health -= d;
	}
}
