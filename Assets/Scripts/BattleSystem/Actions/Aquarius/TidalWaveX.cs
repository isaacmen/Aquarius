﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Aquarius
public class TidalWaveX : ShapeAction {
	override public int maxUses() {
		return 9999999;
	}

	override public void playAnimation() {
		GetComponentInChildren<Animator>().Play("Attacking");
        GameObject.Find("SFX").GetComponent<SFXPlayer>().PlaySound("AquaTidalHit");
    }

	override public bool animationComplete() {
		return GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Idle");
	}

	override public bool[,] getTargets() {
		return new bool[3, 3] {
			{ true, false, true },
			{ false, true, false },
			{ true, false, true }
		};
	}

	override public int getDamage() {
		return Constants.getInstance().tidalWaveX_damage;
	}
}
