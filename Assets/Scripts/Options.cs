using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Options : MonoBehaviour {

    public AudioMixer mixer;
    private float textSpeed = 1f;

    private void Start()
    {
        if (PlayerPrefs.HasKey("textSpeed"))
            textSpeed = PlayerPrefs.GetFloat("textSpeed");
    }

    public void setBGM()
    {
        Slider bgmSlider = GameObject.Find("bgmSlider").GetComponent<Slider>();
        float bgmVol = bgmSlider.value;
        mixer.SetFloat("BGM", bgmVol);
        PlayerPrefs.SetFloat("BGM", bgmVol);
    }

    public void setSFX()
    {
        Slider sfxSlider = GameObject.Find("sfxSlider").GetComponent<Slider>();
        float sfxVol = sfxSlider.value;
        mixer.SetFloat("SFX", sfxVol);
        PlayerPrefs.SetFloat("SFX", sfxVol);
    }

    public void setTextSpeed()
    {
        Dropdown speedTextDrop = GameObject.Find("textSpeed").GetComponent<Dropdown>();
        int textSpeedIndex = speedTextDrop.value;
        Debug.Log(textSpeedIndex);
        switch (textSpeedIndex)
        {
            case 0:
                textSpeed = 1;
                break;
            case 1:
                textSpeed = 0.1f;
                break;
            case 2:
                textSpeed = 0.01f;
                break;
            case 3:
                textSpeed = 0f;
                break;
            default:
                textSpeed = 0.1f;
                break;
        }

        PlayerPrefs.SetFloat("textSpeed", textSpeed);
        PlayerPrefs.SetInt("textSpeedIndex", textSpeedIndex);
    }

    public float getTextSpeed()
    {
        return textSpeed;
    }
}
