using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    [Header("On Screen Positioning")]
    public Camera cam;
    public GameObject character;
    public Vector3 offset;

    private Vector3 position;

    //[Header("Slider Stuff")]
    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        setHealthSlider();
    }

    private void Update()
    {
        if (cam)
            updatePosition();
        updateHealthBar();
    }

    private void updatePosition()
    {
        position = character.transform.position;
        Vector3 charaScreenPosition = cam.WorldToScreenPoint(position);
        transform.position = charaScreenPosition + offset;
    }

    public void setHealthSlider()
    {
        slider.maxValue = character.GetComponent<Character>().maxHealth;
        slider.value = slider.maxValue;
    }

    public void updateHealthBar()
    {
        slider.maxValue = character.GetComponent<Character>().maxHealth;
        slider.value = character.GetComponent<Character>().health;
    }
}
