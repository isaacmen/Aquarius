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
        if (!Input.GetKey(KeyCode.Backslash))
            return false;
        return true;
    }
}
