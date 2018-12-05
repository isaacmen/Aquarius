using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer : MonoBehaviour {

	public AudioClip jump;
	public AudioClip AquaAttack;
	public AudioClip AquaTidalCharge;
	public AudioClip AquaTidalHit;
	public AudioClip AquaSummon;
	public AudioClip LeoAttack;
	public AudioClip LeoUnload;
	public AudioClip LeoLullaby;
	public AudioClip LeoCrescendo;
	public AudioClip ScorpioAttack;
	public AudioClip ScorpioFireball;
	public AudioClip ScorpioLightning;
	public AudioClip ScorpioPWKCharge;
	public AudioClip ScorpioPWKHit;
	public AudioClip LibraAttack;
	public AudioClip LibraGuard;
	public AudioClip LibraLOH;
	public AudioClip LibraShieldbash;



	private AudioSource src;

	// Use this for initialization
	void Start () {
		src = this.GetComponent<AudioSource> ();
	}
	
	public void Jump (string skill) {
		if (skill == "jump") {
			src.clip = jump;
		} else if (skill == "AquaAttack") {
			src.clip = AquaAttack;
		} else if (skill == "AquaTidalCharge") {
			src.clip = AquaTidalCharge;
		} else if (skill == "AquaTidalHit") {
			src.clip = AquaTidalHit;
		} else if (skill == "AquaSummon") {
			src.clip = AquaSummon;
		} else if (skill == "LeoAttack") {
			src.clip = LeoAttack;
		} else if (skill == "LeoUnload") {
			src.clip = LeoUnload;
		} else if (skill == "LeoLullaby") {
			src.clip = LeoLullaby;
		} else if (skill == "LeoCrescendo") {
			src.clip = LeoCrescendo;
		} else if (skill == "ScorpioAttack") {
			src.clip = ScorpioAttack;
		} else if (skill == "ScorpioFireball") {
			src.clip = ScorpioFireball;
		} else if (skill == "ScorpioLightning") {
			src.clip = ScorpioLightning;
		} else if (skill == "ScorpioPWKCharge") {
			src.clip = ScorpioPWKCharge;
		} else if (skill == "ScorpioPWKHit") {
			src.clip = ScorpioPWKHit;
		} else if (skill == "LibraAttack") {
			src.clip = LibraAttack;
		} else if (skill == "LibraGuard") {
			src.clip = LibraGuard;
		} else if (skill == "LibraLOH") {
			src.clip = LibraLOH;
		} else if (skill == "LibraShieldbash") {
			src.clip = LibraShieldbash;
		} 

		src.Play ();
	}

}
