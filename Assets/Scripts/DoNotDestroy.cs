using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script manages the ability catalog. The GameObject with the
// "AbilityCatalog" tag is the preserved.

public class DoNotDestroy : MonoBehaviour
{
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("AbilityCatalog");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
}
