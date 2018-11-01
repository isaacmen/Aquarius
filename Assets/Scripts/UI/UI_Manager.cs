using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour {
    public Slider character1Health;
    public Slider enemy1Health;

    public GameObject character1;
    public GameObject enemy1;

    private void Start()
    {
        setHealthSliders(character1Health, character1);
        setHealthSliders(enemy1Health, enemy1);
    }

    public void updateHealthBars()
    {
        character1Health.value = character1.GetComponent<Character>().health;
        enemy1Health.value = enemy1.GetComponent<Character>().health;
    }

    private void setHealthSliders(Slider healthSlider, GameObject character)
    {
        healthSlider.maxValue = character.GetComponent<Character>().maxHealth;
        healthSlider.value = healthSlider.maxValue;
    }

}
