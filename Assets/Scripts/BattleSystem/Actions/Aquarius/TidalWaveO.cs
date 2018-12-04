using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TidalWaveO : ShapeAction {
	override public int maxUses() {
		return 9999999;
	}

	override public void playAnimation() {
		GetComponentInChildren<Animator>().Play("Attacking");
	}

	override public bool animationComplete() {
		return GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Idle");
	}

	override public bool[,] getTargets() {
		return new bool[3, 3] {
			{ false, true, false },
			{ true, false, true },
			{ false, true, false }
		};
	}

	override public int getDamage() {
		return Constants.getInstance().tidalWaveO_damage;
	}
}