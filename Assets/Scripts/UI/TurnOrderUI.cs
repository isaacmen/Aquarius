using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnOrderUI : MonoBehaviour {
    [Header("Turn Order Bar")]
    //public Image[] imageSpaces; // the spaces for the actual shit
    //public Vector3 indicatorOffset; // the offset from the center where the indicator will be
    public GameObject turnOrderParent; //parent of the images of the turn order
    public GameObject indicator; // the little thing that shows which turn it is on the bar

    public List<Image> turnOrderImages;
    public List<Transform> positions;
    //private Vector3[] scales;

    [Header("Current Turn Panel")]
    public Text currentTurnText;
    public Image characterPic;
    public HealthBar currentCharacterHealth;

    //[Header("General Game Stuff")]
    private GameLoop gameLoop;
    private int currentTurn;

    private void Start()
    {
        gameLoop = GameLoop.getInstance();
        currentTurn = 0;

        Transform[] kidsTransforms = turnOrderParent.GetComponentsInChildren<Transform>();
        turnOrderImages = new List<Image>();
        positions = new List<Transform>();

        foreach (Transform kid in kidsTransforms)
        {
            if (kid.name.Equals("Image"))
            {
                turnOrderImages.Add(kid.gameObject.GetComponent<Image>());
            }
            else if (kid.name.Contains("Pic"))
            {
                positions.Add(kid);
            }
        }

        updateTurn();
    }

    private void Update()
    {
        ////TODO: fix for actual turn change
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    updateTurn();
        //}
    }

    public void updateTurn()
    {
        updateTurnBar();
        updateTurnIndicator();
        updateCurrentInfo();
        currentTurn++;
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

    private void updateTurnBar()
    {
        ///Will Hopefully be Obsolete
        List<Character> turnOrder = gameLoop.turnOrder;
        //for (int i = 0; i < imageSpaces.Length; i++)
        //{
        //    int nextPositionIndex = (i + currentTurn) % imageSpaces.Length;
        //    //images[i].rectTransform.position = positions[nextPositionIndex];
        //    //images[i].rectTransform.localScale = scales[nextPositionIndex];
        //}
        //currentTurn++;
    }

    private void updateTurnIndicator()
    {

        Vector3 newPosition = indicator.transform.position;
        Debug.Log("Current Turn: " + currentTurn);
        Debug.Log("Positions Count: " + positions.Count);
        Debug.Log("Next Space: " + positions[currentTurn % positions.Count]);
        Transform nextPic = positions[currentTurn % positions.Count];
        newPosition.x = nextPic.position.x;//gameLoop.getCharacterTurn().transform.position.x;

        //Vector3.MoveTowards(indicator.transform.position, 
        //    newPosition, 10);
        indicator.transform.position = newPosition;
    }

    private void updateCurrentInfo()
    {
        updateCurrentText();
        updateCurrentPic();
        updateCurrentCharacterHealth();
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

    private void updateCurrentCharacterHealth()
    {
        currentCharacterHealth.character = gameLoop.getCharacterTurn().gameObject;
        currentCharacterHealth.updateHealthBar();
    }
}
