using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicChanger : MonoBehaviour {

	public AudioClip DeliriousMarch;
	public AudioClip StayThisWay;

	private AudioSource src;

	// Use this for initialization
	void Start () {
		src = this.GetComponent<AudioSource> ();
	}
	
	public void change1() {
		if (src.clip != DeliriousMarch) {
			src.Stop ();
			src.clip = DeliriousMarch;
			src.Play ();
		}
	}

	public void change2() {
		if (src.clip != StayThisWay) {
			src.Stop ();
			src.clip = StayThisWay;
			src.Play ();
		}
	}
}
