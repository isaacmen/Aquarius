using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnOrderUI : MonoBehaviour {
    [Header("Turn Order Bar")]
    public Image[] imageSpaces; // the spaces for the actual shit
    public Vector3 indicatorOffset; // the offset from the center where the indicator will be

    private Vector3[] positions;
    //private Vector3[] scales;

    [Header("Current Turn Panel")]
    public Text currentTurnText;
    public Image characterPic;

    //[Header("General Game Stuff")]
    private GameLoop gameLoop;
    private int currentTurn;

    private void Start()
    {
        gameLoop = GameLoop.getInstance();
        currentTurn = 1;
        positions = new Vector3[imageSpaces.Length];
        //scales = new Vector3[images.Length];
        for (int i = 0; i < imageSpaces.Length; i++)
        {
            positions[i] = imageSpaces[i].rectTransform.position;
            //scales[i] = images[i].rectTransform.localScale;
        }

        updateCurrentInfo();
    }

    private void Update()
    {
        //TODO: fix for actual turn change
        if (Input.GetKeyDown(KeyCode.Space))
        {
            updateTurn();
        }
    }

    public void updateTurn()
    {
        updateTurnBar();
        updateTurnIndicator();
        updateCurrentInfo();
    }

    public void addNewTurn()
    {
        /* Ideas for messing w/ turn order:
         *   - have each character script have a "public image portrait;"
         *   - (assuming only 6 characters allowed at a time) have positions/scales
         *     of all character portraits ready
         *     only show the first few and have them go onto those
         *   - basically, we'll rewrite, but save this for later 
         *     when we can better test
         */
    }

    private void updateTurnIndicator()
    {

    }

    private void updateTurnBar()
    {
        ///Will Hopefully be Obsolete
        List<Character> turnOrder = gameLoop.turnOrder;
        for (int i = 0; i < imageSpaces.Length; i++)
        {
            int nextPositionIndex = (i + currentTurn) % imageSpaces.Length;
            //images[i].rectTransform.position = positions[nextPositionIndex];
            //images[i].rectTransform.localScale = scales[nextPositionIndex];
        }
        currentTurn++;
    }

    private void updateCurrentInfo()
    {
        updateCurrentText();
        updateCurrentPic();
    }

    private void updateCurrentText()
    {
        currentTurnText.text = gameLoop.getCharacterTurn() + "'s turn";
    }

    private void updateCurrentPic()
    {
        // Updating the portrait of the current player
        Character currentCharacter = gameLoop.getCharacterTurn();
        if (currentCharacter.GetType() == typeof(AllyCharacter))
            characterPic.sprite = currentCharacter.portrait;
    }
}
