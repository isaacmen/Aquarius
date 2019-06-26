using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedDropdown : MonoBehaviour {

    private void Awake()
    {
        GetComponent<Dropdown>().value = PlayerPrefs.GetInt("textSpeedIndex");
    }
}
