using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

        if (actions.activeSelf)
            updateActionButtons();
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
        skillInfo.SetActive(false);
    }

    public void resetAvailableActions(bool moveActive, bool actionsActive)
    {
        //Debug.Log("RESETING: " + moveActive + " " + actionsActive);
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

    private void updateActionButtons()
    {
        Transform[] actionKids = actions.GetComponentsInChildren<Transform>();
        foreach (Transform kid in actionKids)
        {
            if (kid.name.Contains("Skill"))
            {
                string newText = "Skill";
                List<Action> activeSkills = GetComponent<GameLoop>().getActiveSkills();
                if (kid.name.Contains("1"))
                {
                    newText = activeSkills[1].getName();
                }
                else
                {
                    newText = activeSkills[0].getName();
                }
                kid.GetComponentInChildren<Text>().text = newText;
            }
        }
    }

    public void updateInfoBox(int actionIndex)
    {
        List<Action> actions = GetComponent<GameLoop>().getActiveActions();
        //Debug.Log(actions[actionIndex].GetType());
        Action hoveredAction = actions[actionIndex];

        //Debug.Log(skillInfo.GetComponentInChildren<TextMeshProUGUI>());
        foreach (TextMeshProUGUI textBB in skillInfo.GetComponentsInChildren<TextMeshProUGUI>())
        {
            //Debug.Log(textBB.name);
            if (textBB.name.Contains("Skill"))
                textBB.text = hoveredAction.getName();
            else if (textBB.name.Contains("Description"))
                textBB.text = hoveredAction.getDescription();
        }

        skillInfo.SetActive(true);
    }

    public void hideInfoBox()
    {
        skillInfo.SetActive(false);
    }
}
