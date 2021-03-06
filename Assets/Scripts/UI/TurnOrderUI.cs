﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnOrderUI : MonoBehaviour {
    [Header("Turn Order Bar")]
    //public GameObject turnOrderParent; //parent of the images of the turn order
    public List<GameObject> potentialPics;
    public GameObject indicator; // the little thing that shows which turn it is on the bar

    //private List<Image> turnOrderImages;
    public List<Transform> positions;
    //private Vector3[] scales;

    [Header("Current Turn Panel")]
    public Text currentTurnText;
    public Image characterPic;
    public HealthBar currentCharacterHealth;

    //[Header("General Game Stuff")]
    private GameLoop gameLoop;
    private List<Character> order;
    private int currentTurn;

    private void Awake()
    {
        gameLoop = GameLoop.getInstance();
        order = gameLoop.turnOrder;
        
        currentTurn = 0;

        //setUpTurnBar();

        //Transform[] kidsTransforms = turnOrderParent.GetComponentsInChildren<Transform>();
        //turnOrderImages = new List<Image>();
        positions = new List<Transform>();

        setUpTurnBar();

        updateTurn();
    }

    private void Update()
    {
        updateTurnBar();
    }

    public void updateTurn()
    {
        updateTurnBar();
        updateTurnIndicator();
        updateCurrentInfo();
        currentTurn++;
    }

    private void updateTurnBar()
    {
        if (gameLoop.turnOrder.Count == 0 && order.Count == positions.Count)
            return;

        positions.Clear();
        setUpTurnBar();
    }

    private void setUpTurnBar()
    {
        order = gameLoop.turnOrder;
        currentTurn = 0;

        for (int i=0; i<potentialPics.Count; i++)
        {
            GameObject kid = potentialPics[i];
            //Debug.Log("Kid name: " + kid.name);
            //Debug.Log("At index: " + i + " of " + order.Count);
            if (i >= order.Count)
            {
                kid.SetActive(false);
            }
            else
            {
                kid.SetActive(true);
                positions.Add(kid.transform);
                Transform[] kidsKids = kid.GetComponentsInChildren<Transform>();
                
                foreach (Transform kidsKid in kidsKids)
                {
                    //if (kidsKid.name.Contains("Mask"))
                    //    Debug.Log("Found mask");
                    if (kidsKid.name.Contains("Image"))
                    {
                        kidsKid.gameObject.GetComponent<Image>().sprite = order[i].portrait;
                    }
                }

            }

        }
    }

    private void updateTurnIndicator()
    {

        Vector3 newPosition = indicator.transform.position;
        if (positions.Count != 0)
        {
            Transform nextPic = positions[currentTurn % positions.Count];
            newPosition.x = nextPic.position.x;//gameLoop.getCharacterTurn().transform.position.x;
        }

        //Vector3.MoveTowards(indicator.transform.position, 
        //    newPosition, 10);
        indicator.transform.position = newPosition;
    }

    private void updateCurrentInfo()
    {
        if (!gameLoop.getCharacterTurn())
            return;
        updateCurrentText();
        updateCurrentPic();
        updateCurrentCharacterHealth();
    }

    private void updateCurrentText()
    {
        currentTurnText.text = gameLoop.getCharacterTurn().name; // + "'s turn";
    }

    private void updateCurrentPic()
    {
        // Updating the portrait of the current player
        Character currentCharacter = gameLoop.getCharacterTurn();
        characterPic.sprite = currentCharacter.portrait;
    }

    private void updateCurrentCharacterHealth()
    {
        currentCharacterHealth.character = gameLoop.getCharacterTurn().gameObject;
        currentCharacterHealth.updateHealthBar();

        currentCharacterHealth.gameObject.SetActive(gameLoop.getCharacterTurn().GetType() == typeof(AllyCharacter));
    }
}
