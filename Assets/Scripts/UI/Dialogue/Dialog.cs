using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Dialog : MonoBehaviour {

    public TextMeshProUGUI textDisplay;
    [TextArea(3, 15)]
    public string[] sentences;
    private int index;
    private bool option1 = false;
    private bool option2 = false;
    private bool skip = false;
    public float typingSpeed;
    public int conversationNumberChoice1;
    public int conversationNumberChoice2;
    private int conversationNumberChoice = 100;
    //int textIndex = 0;

    public GameObject continueButton;
    public GameObject choiceButton1;
    public GameObject choiceButton2;
    public GameObject dialogueBackplate;
    public GameObject choiceBackplate;
    public GameObject Speaker1;
    public GameObject Speaker2;
    public GameObject Speaker3;
    public int choiceNumber;

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

        if (textDisplay.text == sentences[index] && index != choiceNumber){
            continueButton.SetActive(true);
        }
        if (option1 == true){
            conversationNumberChoice = conversationNumberChoice1;
        } else if (option2 == true) {
            conversationNumberChoice = conversationNumberChoice2;
        }
    }


    IEnumerator Type(){
        foreach(char letter in sentences[index].ToCharArray()){
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void choice1(){
        option1 = true;
        choiceBackplate.SetActive(false);
    }
    public void choice2(){
        option2 = true;
        choiceBackplate.SetActive(false);
    }

    public void NextSentence(){

        continueButton.SetActive(false);
        choiceButton1.SetActive(false);
        choiceButton2.SetActive(false);
        //index < sentences.Length - 1
        if (index < conversationNumberChoice){
            if (option2 == true){
                index += 2;
            } else if (option1 == true && skip == true){
                index += 2;
            } else if (option1 == true){
                skip = true;
                index++;
            } else {
                index++;
            }
            textDisplay.text = "";
            StartCoroutine(Type());
        } else {
            textDisplay.text = "";
            continueButton.SetActive(false);
            dialogueBackplate.SetActive(false);
        }
        if (index == choiceNumber){
            choiceButton1.SetActive(true);
            choiceButton2.SetActive(true);
            choiceBackplate.SetActive(true);
        }
        Debug.Log(index);
    }

}
