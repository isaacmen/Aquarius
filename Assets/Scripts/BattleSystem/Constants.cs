using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants : MonoBehaviour {
	[Header("Libra")]
	public int layOnHands_healValue;
	public int layOnHands_minRange;
	public int layOnHands_maxRange;

	[Header("Leo")]
	public int unload_damage;
	public int unload_minRange;
	public int unload_maxRange;

	[Header("Scorpio")]
	public int fireball_damage;
	public int fireball_minRange;
	public int fireball_maxRange;

	public static Constants getInstance() {
		return GameObject.Find("Constants").GetComponent<Constants>();
	}
}
