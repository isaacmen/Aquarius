using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants : MonoBehaviour {
	[Header("Libra")]
	public int layOnHands_healValue;
	public int layOnHands_minRange;
	public int layOnHands_maxRange;
	public int layOnHands_maxUses;

	[Header("Leo")]
	public int unload_damage;
	public int unload_minRange;
	public int unload_maxRange;
	public int unload_maxUses;

	[Header("Scorpio")]
	public int fireball_damage;
	public int fireball_minRange;
	public int fireball_maxRange;
	public int fireball_maxUses;

	public static Constants getInstance() {
		return GameObject.Find("Constants").GetComponent<Constants>();
	}
}
