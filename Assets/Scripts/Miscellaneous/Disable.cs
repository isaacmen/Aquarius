using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disable : MonoBehaviour
{

    public GameObject objectToDisable;
    public GameObject objectToDisable1;
    public GameObject objectToDisable2;
    public GameObject objectToDisable3;
    public GameObject objectToDisable4;
    public GameObject objectToDisable5;
    public GameObject objectToEnable;
    public GameObject objectToEnable1;
    public GameObject objectToEnable2;
    public GameObject objectToEnable3;
    public GameObject objectToEnable4;
    public GameObject objectToEnable5;
    public static bool toggle = false;

    // Update is called once per frame
    void Update()
    {
        if (toggle == true)
        {
            objectToDisable.SetActive(false);
            objectToDisable1.SetActive(false);
            objectToDisable2.SetActive(false);
            objectToDisable3.SetActive(false);
            objectToDisable4.SetActive(false);
            objectToDisable5.SetActive(false);
            objectToEnable.SetActive(true);
            objectToEnable1.SetActive(true);
            objectToEnable2.SetActive(true);
            objectToEnable3.SetActive(true);
            objectToEnable4.SetActive(true);
            objectToEnable5.SetActive(true);
        }
        else if (toggle == false)
        {
            objectToDisable.SetActive(true);
            objectToDisable1.SetActive(true);
            objectToDisable2.SetActive(true);
            objectToDisable3.SetActive(true);
            objectToDisable4.SetActive(true);
            objectToDisable5.SetActive(true);
            objectToEnable.SetActive(false);
            objectToEnable1.SetActive(false);
            objectToEnable2.SetActive(false);
            objectToEnable3.SetActive(false);
            objectToEnable4.SetActive(false);
            objectToEnable5.SetActive(false);
        }
    }
}
