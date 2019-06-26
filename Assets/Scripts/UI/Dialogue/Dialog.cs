using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Dialog : MonoBehaviour {

    public TextMeshProUGUI textDisplay;
    [TextArea(3, 15)]
    public string[] sentences;
    private int index;
    public float typingSpeed;
    //int textIndex = 0;
    public bool choices;
    public string nextScene;
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
    private bool divergeText;
    public int divergeTextLength1 = 3;
    public int divergeTextLength2 = 3;
    public int divergeTextLength3 = 8;
    public int divergeTextLength4 = 5;
    public int divergeTextLength5 = 4;
    public int divergeTextLength6 = 8;
    private int surrogate;
    private bool c1;
    private bool c2;
    private bool c3;
    private bool c4;
    private bool c5;
    private bool c6;
    private bool sequenceComplete;

	private MusicChanger musicChanger;


    void Start()
    {
        typingSpeed = GameObject.Find("Options").GetComponent<Options>().getTextSpeed();
		musicChanger = FindObjectOfType<MusicChanger>();
        StartCoroutine(Type());
    }



    private void Update()
    {
        typingSpeed = GameObject.Find("Options").GetComponent<Options>().getTextSpeed();
        //Debug.Log(index);
        //Update the portrait depending on who is speaking.
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
        } else {

        }

        //Activate buttons
        if (choices == true){
            if (textDisplay.text == sentences[index] && index != choiceNumber1 && index != choiceNumber2 && index != choiceNumber3){
                continueButton.SetActive(true);
            } else if (divergeText == false && textDisplay.text == sentences[index] && (index == choiceNumber1 || index == choiceNumber2 || index == choiceNumber3)){
                if (sequenceComplete == false){
                    choiceButton1.SetActive(true);
                    choiceButton2.SetActive(true);
                    choiceBackplate.SetActive(true);
                } else {
                    continueButton.SetActive(true);
                    sequenceComplete = true;
                }

            } else if (divergeText == true && textDisplay.text == sentences[index]){
                continueButton.SetActive(true);
            }
        } else {
            if (textDisplay.text == sentences[index])
            {
                continueButton.SetActive(true);
            }
        }

    }


    //Scroll text
    IEnumerator Type(){
        foreach(char letter in sentences[index].ToCharArray()){
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }


    //Choice 1 operations
    public void Choice1() {
        if (index == choiceNumber1) {
            AbilityCatalog.powerWordKill = true;
            c1 = true;
            divergeText = true;
        } else if (index == choiceNumber2){
            AbilityCatalog.crescendo = true;
            c3 = true;
            divergeText = true;
        } else if (index == choiceNumber3){
            AbilityCatalog.layOnHands = true;
            c5 = true;
            divergeText = true;
        }
    }


    //Choice 2 operations
    public void Choice2(){
        if (index == choiceNumber1){
            AbilityCatalog.lightning = true;
            AbilityCatalog.endingPoints++;
            c2 = true;
            divergeText = true;
        }
        else if (index == choiceNumber2){
            AbilityCatalog.arpeggioPocoAPoco = true;
            AbilityCatalog.endingPoints++;
            c4 = true;
            divergeText = true;
        }
        else if (index == choiceNumber3){
            AbilityCatalog.shieldBash = true;
            AbilityCatalog.endingPoints++;
            divergeText = true;
            c6 = true;
        }
    }



    public void NextSentence(){
        //Clear UI
        continueButton.SetActive(false);
        choiceButton1.SetActive(false);
        choiceButton2.SetActive(false);
        choiceBackplate.SetActive(false);
        sequenceComplete = false;

        //Sentence progression
        if (divergeText == false){
            if (index < sentences.Length - 1){
                index++;
                textDisplay.text = "";
                StartCoroutine(Type());
            } else {
                textDisplay.text = "";
                continueButton.SetActive(false);
                dialogueBackplate.SetActive(false);
                SceneManager.LoadScene(sceneName: nextScene);
            }
        } else {
            if (index == choiceNumber1){
                //sequenceComplete = false;
                List<string> a1List = new List<string>{
                    "SCORPIO: You cannot be serious. It is not just me, right? Are you all too ignorant to see it? Obviously they will betray us once we finish doing their dirty work.",
                    "LEO: Not everyone is out to get you, Scorpio.",
                    "SCORPIO: Why are you so confident in them? Those incompetent fools did not even notice half their kingdoms being flooded!"
                };
                List<string> b1List = new List<string>{
                    "SCORPIO: I believe they are simply putting up a front. They will break the alliance as soon as this is over. I have lived long enough to know that peace never lasts, and it never will.",
                    "LEO: That doesn’t really matter to me. All I think we need to worry about right now is taking out that big fish.",
                    "SCORPIO: These are the same people that did not even realize half their kingdoms were flooded. Are you sure we should give no thought at all to the aftermath?"
                };
                //Debug.Log(a1List[1]);
                if (c1 == true){
                    sentences[index] = a1List[surrogate];
                } else if (c2 == true){
                    sentences[index] = b1List[surrogate];
                }

                textDisplay.text = "";
                surrogate++;
                Debug.Log(surrogate);
                StartCoroutine(Type());
                Debug.Log(divergeText);
                if (c1 == true && surrogate == divergeTextLength1)
                {
                    divergeText = false;
                    surrogate = 0;
                    sequenceComplete = true;
                }
                else if (c2 == true && surrogate == divergeTextLength2)
                {
                    divergeText = false;
                    surrogate = 0;
                    sequenceComplete = true;
                }
            } else if (index == choiceNumber2){
				musicChanger.change1 ();

                //sequenceComplete = false;
                List<string> a2List = new List<string>{
                    "LEO: W-well, I mean… it’s not like you have anyone to impress anymore, right?",
                    "LIBRA: Leo!",
                    "LEO: Uh, I mean, I’m sorry about your village Scor-",
                    "SCORPIO: I never knew you could be so contemptible. Don’t patronize me, mongrel.",
                    "LIBRA: Scorpio!",
                    "LEO: Now wait a minute. I didn’t mean to upset you. I just thought-",
                    "SCORPIO: I understand now.",
                    "LEO: Pardon?"
                };
                List<string> b2List = new List<string>{
                    "LEO: We’ll always be misfits, Scorpio. But it doesn't matter whether society accepts us or not, as long we have each other, right?",
                    "SCORPIO: Okay. But then what was the whole point of this quest?",
                    "LEO: Pardon?",
                    "SCORPIO: Uniting everybody, making them believe in their rulers again, making the world a better place. Was that not what we were fighting for?",
                    "LEO: W-well, I-"
                };
                if (c3 == true){
                    sentences[index] = a2List[surrogate];
                }
                else if (c4 == true){
                    sentences[index] = b2List[surrogate];
                }
                textDisplay.text = "";
                surrogate++;
                StartCoroutine(Type());
                if (c3 == true && surrogate == divergeTextLength3)
                {
                    divergeText = false;
                    surrogate = 0;
                    sequenceComplete = true;
                }
                else if (c4 == true && surrogate == divergeTextLength4)
                {
                    divergeText = false;
                    surrogate = 0;
                    sequenceComplete = true;
                }
            } else if (index == choiceNumber3){
				musicChanger.change2 ();

                //sequenceComplete = false;
                List<string> a3List = new List<string>{
                    "LIBRA: Both of you, listen to me for a second. I save people because it’s the right thing to do. No matter what our motivation is, whether it’s glory or revenge or self-righteousness, our number one duty is to protect the people. Let’s at least accomplish that before you two make a mess of each other.",
                    "LEO: ...purrhaps you’re right.",
                    "SCORPIO: Tch. I will admit you may have a point.",
                    "LEO: I’ll admit that I’ve been a bit disharmonious."
                };
                List<string> b3List = new List<string>{
                    "LIBRA: Both of you need to calm down. Enough with your petty squabbling! We all have our reasons for joining, so let’s just drop this and move on. What we need to do right now is pull ourselves together and fuck that fish!",
                    "LEO: ...",
                    "SCORPIO: ...",
                    "LIBRA: W-wait! Th-that’s not what- I mean, it’s-",
                    "SCORPIO: ...pfft-",
                    "LEO: Haha, Libra, I see what’s motivating you on this quest now.",
                    "LIBRA: I didn't mean it like that.",
                    "LEO: It's fine Libra. Aquarius is quite pretty, for a fish demon."
                };
                if (c5 == true){
                    sentences[index] = a3List[surrogate];
                }
                else if (c6 == true){
                    sentences[index] = b3List[surrogate];
                }
                textDisplay.text = "";
                surrogate++;
                StartCoroutine(Type());
                if (c5 == true && surrogate == divergeTextLength5){
                    divergeText = false;
                    surrogate = 0;
                    sequenceComplete = true;
                } else if (c6 == true && surrogate == divergeTextLength6){
                    divergeText = false;
                    surrogate = 0;
                    sequenceComplete = true;
                }
            }

        }



        //Change option text
        if (index == choiceNumber1){
            choiceButton1.GetComponent<UnityEngine.UI.Text>().text = "(Point out the obvious.)";
            choiceButton2.GetComponent<UnityEngine.UI.Text>().text = "(Explain your thoughts.)";
        } else if (index == choiceNumber2){
            choiceButton1.GetComponent<UnityEngine.UI.Text>().text = "(Wing it.)";
            choiceButton2.GetComponent<UnityEngine.UI.Text>().text = "(Get emotional.)";
        } else if (index == choiceNumber3){
            choiceButton1.GetComponent<UnityEngine.UI.Text>().text = "(Appeal to them with logic.)";
            choiceButton2.GetComponent<UnityEngine.UI.Text>().text = "(Tell them how you really feel.)";
        }
        Debug.Log(index);
    }

}
