using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {
	void Awake() {
		Debug.Log("awake" + this.name);
	}

	void Start() {
		Debug.Log("start" + this.name);
	}
	
	void Update() {
		Debug.Log(	fromBool(Input.GetKey(KeyCode.W)) +
					fromBool(Input.GetKey(KeyCode.A)) +
					fromBool(Input.GetKey(KeyCode.S)) +
					fromBool(Input.GetKey(KeyCode.D)));
	}

	string fromBool(bool b) {
		return (b) ? "T" : "F";
	}
}
