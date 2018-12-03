using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Dialog : MonoBehaviour {

    public TextMeshProUGUI textDisplay;
    [TextArea(3, 15)]
    public string[] sentences;
    private int index;
    public float typingSpeed;
    //int textIndex = 0;

    public GameObject continueButton;
    public GameObject choiceButton1;
    public GameObject choiceButton2;
    public GameObject dialogueBackplate;
    public GameObject choiceBackplate;
    public GameObject Speaker1;
    public GameObject Speaker2;
    public GameObject Speaker3;
    public int choiceNumber1;
    public int choiceNumber2;
    public int choiceNumber3;

    void Start()
    {
        StartCoroutine(Type());
    }


    private void Update()
    {
        if (sentences[index].Contains(Speaker1.name)){
            Speaker1.SetActive(true);
            Speaker2.SetActive(false);
            Speaker3.SetActive(false);
        } else if (sentences[index].Contains(Speaker2.name)){
            Speaker2.SetActive(true);
            Speaker1.SetActive(false);
            Speaker3.SetActive(false);
        } else if (sentences[index].Contains(Speaker3.name)){
            Speaker3.SetActive(true);
            Speaker2.SetActive(false);
            Speaker1.SetActive(false);
        }
        else{
            //Do nothing
        }

        if (textDisplay.text == sentences[index] && index != choiceNumber1 && index != choiceNumber2 && index != choiceNumber3){
            continueButton.SetActive(true);
        } else if (textDisplay.text == sentences[index] && (index == choiceNumber1 || index == choiceNumber2 || index == choiceNumber3)){
            choiceButton1.SetActive(true);
            choiceButton2.SetActive(true);
            choiceBackplate.SetActive(true);
        }
    }


    IEnumerator Type(){
        foreach(char letter in sentences[index].ToCharArray()){
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void Choice1() {
        if (index == choiceNumber1) {
            AbilityCatalog.powerWordKill = true;
            choiceBackplate.SetActive(false);
        } else if (index == choiceNumber2){
            AbilityCatalog.crescendo = true;
            choiceBackplate.SetActive(false);
        } else if (index == choiceNumber3){
            AbilityCatalog.layOnHands = true;
            choiceBackplate.SetActive(false);
        }
    }
    public void Choice2(){
        if (index == choiceNumber1){
            AbilityCatalog.lightning = true;
            AbilityCatalog.endingPoints++;
            choiceBackplate.SetActive(false);
        }
        else if (index == choiceNumber2){
            AbilityCatalog.arpeggioPocoAPoco = true;
            AbilityCatalog.endingPoints++;
            choiceBackplate.SetActive(false);
        }
        else if (index == choiceNumber3){
            AbilityCatalog.shieldBash = true;
            AbilityCatalog.endingPoints++;
            choiceBackplate.SetActive(false);
        }
    }

    public void NextSentence(){

        continueButton.SetActive(false);
        choiceButton1.SetActive(false);
        choiceButton2.SetActive(false);
        choiceBackplate.SetActive(false);
        //index < sentences.Length - 1
        if (index < sentences.Length){
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        } else {
            textDisplay.text = "";
            continueButton.SetActive(false);
            dialogueBackplate.SetActive(false);
        }
        if (index == choiceNumber1){
            //choiceButton1.SetActive(true);
            choiceButton1.GetComponent<UnityEngine.UI.Text>().text = "(Point out the obvious.)";
            //choiceButton2.SetActive(true);
            choiceButton2.GetComponent<UnityEngine.UI.Text>().text = "(Explain your thoughts.)";
            //choiceBackplate.SetActive(true);
        } else if (index == choiceNumber2){
            //choiceButton1.SetActive(true);
            choiceButton1.GetComponent<UnityEngine.UI.Text>().text = "(Wing it.)";
            //choiceButton2.SetActive(true);
            choiceButton2.GetComponent<UnityEngine.UI.Text>().text = "(Get emotional.)";
            //choiceBackplate.SetActive(true);
        } else if (index == choiceNumber3){
            //choiceButton1.SetActive(true);
            choiceButton1.GetComponent<UnityEngine.UI.Text>().text = "(Appeal to them with logic.)";
            //choiceButton2.SetActive(true);
            choiceButton2.GetComponent<UnityEngine.UI.Text>().text = "(Tell them how you really feel.)";
            //choiceBackplate.SetActive(true);
        }
        Debug.Log(index);
    }

}
