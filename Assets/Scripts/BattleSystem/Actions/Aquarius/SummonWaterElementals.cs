using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Aquarius
public class SummonWaterElementals : Action {
	private bool firstCheck;

	override public ActionType getActionType() {
		return ActionType.ABILITY;
	}

	override public int maxUses() {
		return 9999999;
	}

	override protected void innerStart() {
		firstCheck = true;
		if(GameLoop.getInstance().getEnemyField().getNumCharacters() <= 1) {
			GetComponentInChildren<Animator>().Play("Hat");
		} else {
			print("already too many");
			setInactiveWithCompletion(false);
		}
	}

	override protected void innerLoop() {
		if(GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Idle")) {
			if(firstCheck) {
				firstCheck = false;
			} else {
				List<EnemyCharacter> elementals = new List<EnemyCharacter>();
				Character c = GetComponentInParent<Character>();
				int[] homeYX = c.getField().getTileArrayCoordsYX(c.getField().getTileAt(c.transform.position));

				foreach(EnemyCharacter chr in GameLoop.getInstance().getInactiveEnemies())
					if(chr.name.Contains("MeleeElemental")) {
						int[] newYX = new int[] { homeYX[0], homeYX[1] + 1 };
						while(newYX[0] > 2 || newYX[0] < 0 || newYX[1] > 2 || newYX[1] < 0)
							newYX = new int[] { homeYX[0] + 2 * Random.Range(0, 2) - 1, homeYX[1] };
						Vector3 pos = c.getField().getTileAtYX(newYX).transform.position;
						chr.transform.position = new Vector3(pos.x, pos.y, -3);
						chr.setOnField(true);
					} else if(chr.name.Contains("RangedElemental")) {
						int[] newYX = new int[] { homeYX[0], homeYX[1] - 1 };
						while(newYX[0] > 2 || newYX[0] < 0 || newYX[1] > 2 || newYX[1] < 0)
							newYX = new int[] { homeYX[0] + 2 * Random.Range(0, 2) - 1, homeYX[1] };
						Vector3 pos = c.getField().getTileAtYX(newYX).transform.position;
						chr.transform.position = new Vector3(pos.x, pos.y, -3);
						chr.setOnField(true);
					}

				setInactiveWithCompletion(true);
			}
		}
	}

	override protected void innerEnd() { }

	override public int getValue() {
		return (GameLoop.getInstance().getEnemyField().getNumCharacters() <= 1) ? 9999999 : 0;
	}

	public override string getDescription() {
		return "Summons 1 Melee and 1 Ranged Water Elemental\n" +
			"Uses: Infinite\n" +
			"Range: Left and Right of Aquarius";
	}
}
