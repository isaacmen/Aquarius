using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectManager {
	private Dictionary<Character, List<ListCharacterStatusEffect>> statusDict;

	public StatusEffectManager() {
		statusDict = new Dictionary<Character, List<ListCharacterStatusEffect>>();
	}

	public void onTurn(Character chr) {
		if(GameLoop.getInstance().DEBUG_LOG) Debug.Log("check status for " + chr.name);
		if(statusDict.ContainsKey(chr)) {
			List<ListCharacterStatusEffect> lcseArr = statusDict[chr];
			foreach(ListCharacterStatusEffect lcse in lcseArr)
				foreach(Character c in lcse.affected)
					c.removeStatus(lcse.effect);
		} else {
			if(GameLoop.getInstance().DEBUG_LOG) Debug.Log("does not contain");
		}
	}

	public void addStatusEffect(Character endEffect, List<Character> affected, StatusEffect effect) {
		if(statusDict.ContainsKey(endEffect))
			statusDict[endEffect].Add(new ListCharacterStatusEffect(affected, effect));
		else
			statusDict.Add(endEffect, new List<ListCharacterStatusEffect>() { new ListCharacterStatusEffect(affected, effect) });

		foreach(Character c in affected)
			c.addStatus(effect);
	}

	private class ListCharacterStatusEffect {
		public List<Character> affected;
		public StatusEffect effect;

		public ListCharacterStatusEffect(List<Character> a, StatusEffect e) {
			affected = a;
			effect = e;
		}
	}
}



public enum StatusEffect {
	STUNNED, INVULNERABLE
}