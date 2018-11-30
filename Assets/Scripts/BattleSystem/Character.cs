using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour {
	private Field field;

	[Header("Attributes")]
	public int maxHealth;
	public int health;
	public int basicAttackDamage;
	public int basicAttackMinRange;
	public int basicAttackMaxRange;

	[Header("Visual Stuff")]
    public Sprite portrait;
	
	public void setField(Field f) {
		field = f;
	}

	public Field getField() {
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
		health = Mathf.Max(Mathf.Min(health - d, maxHealth), 0);
	}
}
