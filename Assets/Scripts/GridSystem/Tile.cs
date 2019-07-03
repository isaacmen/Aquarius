using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
	private Field field;

	[Header("Character")]
	public Character onTile;

    [Header("Status Effects")]
    public List<StatusEffect> statusEffects;

    void Awake() {
		onTile = null;
        statusEffects = new List<StatusEffect>();
    }

	void Update() {
		
	}

	public void setClickableSprite() {
		GetComponent<SpriteRenderer>().sprite = field.tileClickableSprite;
	}

	public void setRegularSprite() {
		GetComponent<SpriteRenderer>().sprite = field.tileRegularSprite;
	}

	public void setSecondarySprite() {
		GetComponent<SpriteRenderer>().sprite = field.tileSecondarySprite;
	}

	public void setTertiarySprite() {
		GetComponent<SpriteRenderer>().sprite = field.tileTertiarySprite;
	}

    public bool isTertiarySprite()
    {
        return GetComponent<SpriteRenderer>().sprite == field.tileTertiarySprite;
    }

	public int[] getTileArrayCoordsYX() {
		return field.getTileArrayCoordsYX(this);
	}

	public void setField(Field f) {
		field = f;
	}

	public Field getField() {
		return field;
	}

	public void removeCharacter() {
        foreach(StatusEffect status in statusEffects)
            onTile.removeStatus(status);

		placeCharacter(null);
	}

	public void placeCharacter(Character c) {
		onTile = c;
        foreach (StatusEffect status in statusEffects)
            if (onTile)
                onTile.addStatus(status);
	}
	
	public Character getCharacter() {
		return onTile;
	}

    public void addStatus(StatusEffect status)
    {
        if (statusEffects.Contains(status))
            return;
        statusEffects.Add(status);
        setVisualBasedOnEffect();
        if (onTile)
            onTile.addStatus(status);
    }

    public void removeStatus(StatusEffect status)
    {
        statusEffects.Remove(status);
        if (onTile)
            onTile.removeStatus(status);
        setVisualBasedOnEffect();
    }

    public void setVisualBasedOnEffect()
    {
        ///Changes the sprite and other effects of the tile based on
        ///what the current status effects are
        if (statusEffects.Contains(StatusEffect.INVULNERABLE))
        {
            setSecondarySprite();
        }
        else if (statusEffects.Contains(StatusEffect.TARGET))
        {
            setTertiarySprite();
        }
        else
        {
            setRegularSprite();
        }

    }
}
