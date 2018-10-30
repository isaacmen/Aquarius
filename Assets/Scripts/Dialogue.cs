using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue {

    public string name;
    public int dialogueID;

    [TextArea(5,15)]
    public string[] sentences;


}
