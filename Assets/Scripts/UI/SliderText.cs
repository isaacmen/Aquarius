using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderText : MonoBehaviour {
    public Slider slider;

    private void FixedUpdate()
    {
        Text txt = GetComponent<Text>();
        txt.text = slider.value.ToString() + "/" + slider.maxValue.ToString();
    }
}
