using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aquarius_animations : MonoBehaviour
{

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
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            m_Animator.Play("Hat");
        }
        else if (Input.GetKey(KeyCode.UpArrow))
            m_Animator.Play("Jump");
    }
}
