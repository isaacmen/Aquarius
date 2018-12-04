using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopSecretInstaKill : MonoBehaviour {
    /*
     * This is for the demonstration, so that the Aquarius can be killed quickly
     * without much hassle
     */

    //public GameLoop gameLoop; //So we can actually access the state of the game
    public List<Character> toKill;
    private EnemyCharacter enemyScript;
    private List<KeyCode> code;

    private void Start()
    {
        enemyScript = GetComponent<EnemyCharacter>();
        code = new List<KeyCode> {
            KeyCode.A,
            KeyCode.Q,
            KeyCode.U,
            KeyCode.R,
            KeyCode.I,
            KeyCode.S
        };
    }

    // Update is called once per frame
    void Update () {
        if (codeInputed())
            foreach (Character character in toKill)
                if (character.isActiveAndEnabled && character.gameObject.activeSelf)
                    character.GetComponent<EnemyCharacter>().takeDamage(character.GetComponent<EnemyCharacter>().maxHealth);
//        GetComponent<GameLoop>().updateGameStatus();
	}

    bool codeInputed()
    {
        //foreach (KeyCode key in code)
        //{
        //    if (!Input.GetKey(key))
        //        return false;
        //}
        if (!Input.GetKey(KeyCode.Backslash))
            return false;
        return true;
    }
}
