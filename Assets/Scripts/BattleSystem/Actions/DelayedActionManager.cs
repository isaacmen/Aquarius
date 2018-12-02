using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedActionManager {
	private Dictionary<Character, List<IntAction>> actionDict;
	private List<Action> activeActions;

	public DelayedActionManager() {
		actionDict = new Dictionary<Character, List<IntAction>>();
		activeActions = new List<Action>();
	}

	public void onTurn(Character chr) {
		if(GameLoop.getInstance().DEBUG_LOG) Debug.Log("check delayed action for " + chr.name);

		if(actionDict.ContainsKey(chr)) {
			List<IntAction> iaList = actionDict[chr];
			for(int i = 0; i < iaList.Count; i++) {
				IntAction ia = iaList[i];
				if(ia.numTurns == 1) {
					ia.action.setActive();
					activeActions.Add(ia.action);
					iaList.Remove(ia);
					i--;
					if(iaList.Count == 0)
						actionDict.Remove(chr);
				} else {
					ia.numTurns--;
				}
			}
		} else {
			if(GameLoop.getInstance().DEBUG_LOG) Debug.Log("delayed action does not contain");
		}
	}

	public void addAction(Character c, int numTurns, Action a) {
		if(actionDict.ContainsKey(c))
			actionDict[c].Add(new IntAction(numTurns, a));
		else
			actionDict.Add(c, new List<IntAction>() { new IntAction(numTurns, a) });
	}

	public bool inProgress() {
		foreach(Action a in activeActions)
			if(a.isActive())
				return true;
		return false;
	}

	private class IntAction {
		public int numTurns;
		public Action action;

		public IntAction(int n, Action a) {
			numTurns = n;
			action = a;
		}
	}
}
