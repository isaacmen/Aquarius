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

    /*public void PlayGameMult() {
        SceneManager.LoadScene("NetworkLobby");
    }
    public void LevelSelectIsland(){
        SceneManager.LoadScene("Mediterranean");
    }
    public void LevelSelectCave(){
        SceneManager.LoadScene("Cave");
    }
    public void LevelSelectDesert(){
        SceneManager.LoadScene("Desert");
    }
    public void LevelSelectDemo(){
        SceneManager.LoadScene("Shooting_Scene");
    }
    public void LevelSelectSandbox(){
        SceneManager.LoadScene("Model Junkyard");
    }*/

    public void playGame(){
        SceneManager.LoadScene("Andrew_Intro_Scene");
    }

    public void QuitGame() {
        Debug.Log ("Quit");
        Application.Quit();
    }
}
