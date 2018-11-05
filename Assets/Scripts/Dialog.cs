using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Dialog : MonoBehaviour {

    public TextMeshProUGUI textDisplay;
    [TextArea(5, 15)]
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
    public int choiceNumber;

    void Start()
    {
        StartCoroutine(Type());
    }


    private void Update()
    {
        if(textDisplay.text == sentences[index] && index != choiceNumber){
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
    }
    public void choice2(){
        option2 = true;
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
        }
        if (index == choiceNumber){
            choiceButton1.SetActive(true);
            choiceButton2.SetActive(true);
        }
        Debug.Log(index);
    }

}
