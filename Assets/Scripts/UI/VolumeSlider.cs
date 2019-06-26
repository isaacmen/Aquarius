using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeSlider : MonoBehaviour {

    public AudioMixer mixer;
    public bool bgm; //not sfx

    private void Awake()
    {
        string var = "SFX";
        if (bgm)
            var = "BGM";
        GetComponent<Slider>().value = PlayerPrefs.GetFloat(var);
    }
}
