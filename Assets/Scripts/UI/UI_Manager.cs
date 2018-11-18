using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour {

    [Header("Character Health Bars")]
    public Slider character1Health;
    public Slider enemy1Health;

    [Header("Characters")]
    public GameObject character1;
    public GameObject enemy1;

    [Header("Turn Order")]
    public GameObject turnOrder;

    [Header("Menus")]
    public GameObject availableActions;
    public GameObject actions;
    public GameObject moves;

    private void Start()
    {
        setHealthSliders(character1Health, character1);
        setHealthSliders(enemy1Health, enemy1);
    }

    private void Update()
    {
        updateHealthBars();
    }

    public void updateHealthBars()
    {
        Debug.Log("Updating Health Bars");
        character1Health.value = character1.GetComponent<Character>().health;
        enemy1Health.value = enemy1.GetComponent<Character>().health;
    }

    private void setHealthSliders(Slider healthSlider, GameObject character)
    {
        healthSlider.maxValue = character.GetComponent<Character>().maxHealth;
        healthSlider.value = healthSlider.maxValue;
    }

    public void updateTurn()
    {
        turnOrder.GetComponent<TurnOrderUI>().updateTurn();
        updateHealthBars();
        for (int i=0; i < availableActions.transform.childCount; i++)
        {
            GameObject child = availableActions.transform.GetChild(1).gameObject;
            child.SetActive(true);
        }
    }

}
