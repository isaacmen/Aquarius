using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnOrderUI : MonoBehaviour {
    [Header("Turn Order Bar")]
    //public GameObject turnOrderParent; //parent of the images of the turn order
    public List<GameObject> potentialPics;
    public GameObject indicator; // the little thing that shows which turn it is on the bar

    //private List<Image> turnOrderImages;
    private List<Transform> positions;
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
        Debug.Log("Order: " + order.Count);
        Debug.Log("turnOrder: " + gameLoop.turnOrder.Count);
        if (order.Count == positions.Count)
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
            if (i >= order.Count)
            {
                kid.SetActive(false);
            }
            else
            {
                kid.SetActive(true);
                positions.Add(kid.transform);
                Debug.Log("Kid name: " + kid.name);
                Transform[] kidsKids = kid.GetComponentsInChildren<Transform>();
                
                foreach (Transform kidsKid in kidsKids)
                {
                    if (kidsKid.name.Contains("Mask"))
                        Debug.Log("Found mask");
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
