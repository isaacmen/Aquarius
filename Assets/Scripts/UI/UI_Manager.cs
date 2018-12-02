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
    public GameObject actions;
    public GameObject cancelMenu;
    public GameObject skillInfo;

    [Header("Feedback")]
    public Text actionText;

    private void Start()
    {
        healthBars = healthBarParent.GetComponentsInChildren<HealthBar>();
    }

    private void Update()
    {
        updateHealthBars();
        if (Input.GetKeyDown(KeyCode.I))
            enableInfoMenu(!skillInfo.activeSelf);
    }

    public void updateTurn()
    {
        turnOrder.GetComponent<TurnOrderUI>().updateTurn();
        updateHealthBars();
        resetMenus();
        
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

    public void resetMenus()
    {
        availableActions.SetActive(true);
        resetAvailableActions(true, true);

        actions.SetActive(false);
        cancelMenu.SetActive(false);
    }

    public void resetAvailableActions(bool moveActive, bool actionsActive)
    {
        Debug.Log("RESETING: " + moveActive + " " + actionsActive);
        availableActions.SetActive(true);
        for (int i = 0; i < availableActions.transform.childCount; i++)
        {
            GameObject child = availableActions.transform.GetChild(i).gameObject;
            if (child.gameObject.name.Contains("Move"))
                child.gameObject.SetActive(moveActive);
            else if (child.gameObject.name.Contains("Action"))
                child.gameObject.SetActive(actionsActive);
            else
                child.gameObject.SetActive(true);
        }
    }

    public void enableInfoMenu(bool enable)
    {
        skillInfo.SetActive(enable);
    }

    public void noMenus()
    {
        availableActions.SetActive(false);
        actions.SetActive(false);
        cancelMenu.SetActive(false);
        enableInfoMenu(false);
    }
}
