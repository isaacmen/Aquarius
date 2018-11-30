using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour {

    [Header("Character Health Bars")]
    public GameObject healthBarParent; //Game object that holds all the health bars, for ease
    private HealthBar[] healthBars; //The actual list of health bars

    [Header("Turn Order")]
    public GameObject turnOrder;

    [Header("Menus")]
    public GameObject availableActions;
    //public GameObject actions;
    //public GameObject moves;

    [Header("Feedback")]
    public Text actionText;

    private void Start()
    {
        healthBars = healthBarParent.GetComponentsInChildren<HealthBar>();
    }

    private void Update()
    {
        updateHealthBars();
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

    public void updateHealthBars()
    {
        foreach (HealthBar healthBar in healthBars)
        {
            healthBar.updateHealthBar();
        }
    }

    public void updateActionText(string text)
    {
        actionText.text = text;
    }
}
