using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {
	private Grid_Behavior field;

	[Header("Attributes")]
	public int maxHealth;
	public int health;
	public int basicAttackDamage;
	
	public void setField(Grid_Behavior f) {
		field = f;
	}

	public Grid_Behavior getField() {
		return field;
	}

	public void moveGrid(Vector3 start, Vector3 end) {
		field.getTileAt(start).removeCharacter();
		field.getTileAt(end).placeCharacter(this);
	}

	void Start() {

	}
	
	void Update() {
		
	}

	public void takeDamage(int d) {
		health -= d;
	}
}
