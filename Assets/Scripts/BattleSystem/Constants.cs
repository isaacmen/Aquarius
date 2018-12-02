using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants : MonoBehaviour {
	[Header("Libra")]
	public int layOnHands_healValue;
	public int layOnHands_minRange;
	public int layOnHands_maxRange;
	public int layOnHands_maxUses;

	public int guard_minRange;
	public int guard_maxRange;
	public int guard_maxUses;

	public int shieldBash_minRange;
	public int shieldBash_maxRange;
	public int shieldBash_damage;
	public int shieldBash_maxUses;

	[Header("Leo")]
	public int unload_damage;
	public int unload_minRange;
	public int unload_maxRange;
	public int unload_maxUses;

	public int lullaby_minRange;
	public int lullaby_maxRange;
	public int lullaby_maxUses;

	public int crescendo_damage;
	public int crescendo_maxUses;

	[Header("Scorpio")]
	public int fireball_damage;
	public int fireball_minRange;
	public int fireball_maxRange;
	public int fireball_maxUses;

	public int powerWordKill_damage;
	public int powerWordKill_minRange;
	public int powerWordKill_maxRange;
	public int powerWordKill_maxUses;

	public int lightningBolt_damage;
	public int lightningBolt_maxUses;

	public static Constants getInstance() {
		return GameObject.Find("Constants").GetComponent<Constants>();
	}
}
