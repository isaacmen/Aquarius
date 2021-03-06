﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TidalWaveRow : ShapeAction {
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
		bool[,] targets = new bool[3, 3] {
			{ false, false, false },
			{ false, false, false },
			{ false, false, false }
		};

		Character c = GetComponentInParent<Character>();
		int[] homeYX = c.getField().getTileArrayCoordsYX(c.getField().getTileAt(c.transform.position));

		for(int i = 0; i < 3; i++)
			targets[homeYX[0], i] = true;

		return targets;
	}

	override public int getDamage() {
		return Constants.getInstance().tidalWaveRow_damage;
	}
}