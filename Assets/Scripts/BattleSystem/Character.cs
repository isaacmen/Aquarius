using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {
	[Header("Attributes")]
	public int maxHealth;
	public int health;
	public int basicAttackDamage;
	
	void Start() {
		
	}
	
	void Update() {
		
	}

	public void takeDamage(int d) {
		health -= d;
	}
}
