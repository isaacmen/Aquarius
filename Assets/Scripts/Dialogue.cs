using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue {

    public string name;
    //public string name2;

    [TextArea(5,15)]
    public string[] sentences;


}
