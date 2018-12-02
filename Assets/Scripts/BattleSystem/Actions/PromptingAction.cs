using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PromptingAction : Action {
	protected bool innerStarted;
	protected List<Tile> inRange;
	protected Tile target;

	override protected void innerStart() {
		innerStarted = false;
		target = null;
		Debug.Log("pick a tile");
		inRange = Field.tilesInCharacterRange(this.GetComponentInParent<Character>(), minRange(), maxRange(), targetYourField());
		foreach(Tile t in inRange) {
			if(validTileToShow(t))
				t.setClickableSprite();
		}
	}

	override protected void innerLoop() {
		if(!innerStarted) {
			if(Input.GetMouseButtonDown(0)) {
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				if(Physics.Raycast(ray, out hit, 100)) {
					GameObject obj = hit.collider.gameObject;
					if(obj.name.Contains("Tile") && inRange.Contains(obj.GetComponent<Tile>())) {
						target = obj.GetComponent<Tile>();
						if(validTileToClick(target)) {
							foreach(Tile t in inRange[0].getField().getTiles())
								t.setRegularSprite();

							postPromptStart();
							innerStarted = true;
                            UI_Manager ui = FindObjectOfType<UI_Manager>();
                            ui.noMenus();
						} else {
							print("invalid target");
						}
					}
				}
			}
			if(Input.GetKey(KeyCode.C)) {
				foreach(Tile t in inRange[0].getField().getTiles())
					t.setRegularSprite();
				setInactiveWithCompletion(false);
			}
		} else {
			postPromptLoop();
		}
	}

	protected abstract int minRange();
	protected abstract int maxRange();
	protected abstract bool validTileToShow(Tile t);
	protected abstract bool validTileToClick(Tile t);
	protected abstract bool targetYourField();

	protected abstract void postPromptStart();
	protected abstract void postPromptLoop();

    public void cancelAction()
    {
        foreach (Tile t in inRange)
            t.setRegularSprite();
        Debug.Log("Cancelling Action");
        setInactiveWithCompletion(false);
    }
}
