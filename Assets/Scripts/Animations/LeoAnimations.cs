﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeoAnimations : MonoBehaviour {

    Animator m_Animator;

    void Start()
    {
        //Fetch the Animator from your GameObject
        m_Animator = GetComponent<Animator>();
    }

    private void Update()
    {
        //Press the space key to play the "Attacking" state
        if (Input.GetKey(KeyCode.Space))
        {
            m_Animator.Play("Attacking");
        }
    }
}
