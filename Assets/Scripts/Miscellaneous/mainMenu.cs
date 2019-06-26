using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class mainMenu : MonoBehaviour
{

    void Start()
    {
    }
    public void PlayGameSing()
    {
        //disable.toggle = true;
    }
    public void Back()
    {
        //disable.toggle = false;
    }
    public void Options()
    {
        //DisableOptions.toggle = true;
    }

    public void playGame(){
        SceneManager.LoadScene("Andrew_Intro_Scene");
    }

    public void QuitGame() {
        Debug.Log ("Quit");
        Application.Quit();
    }
}
