using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour {
	private Field field;

	[Header("Status Effects")]
	public List<StatusEffect> statusEffects;

	[Header("Attributes")]
	public bool onField;
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

	protected virtual void Awake() {
		if(GameLoop.getInstance().DEBUG_LOG) print(this.name + " awake");
		statusEffects = new List<StatusEffect>();
	}

	protected virtual void Start() {
		if(GameLoop.getInstance().DEBUG_LOG) print(this.name + " start");
	}
	
	protected void Update() {
		
	}

	

	public void addStatus(StatusEffect status) {
		statusEffects.Add(status);
	}

	public void removeStatus(StatusEffect status) {
		statusEffects.Remove(status);
	}

	public void takeDamage(int d) {
		if(d < 0 || !statusEffects.Contains(StatusEffect.INVULNERABLE))
			health = Mathf.Min(health - d, maxHealth);
		if(health <= 0) {
			Debug.Log(this.name + " died");
            // REMOVE HEALTH BAR
            HealthBar health = UI_Manager.getInstance().getHealthBar(this);
            if (health)
                Destroy(health.gameObject);
			this.gameObject.SetActive(false);
			field.removeCharacter(this);
			GameLoop.getInstance().turnOrder.Remove(this);
		}
	}
}