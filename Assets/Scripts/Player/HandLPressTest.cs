using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandLPressTest : MonoBehaviour
{
    Animator animHandL;

    void Start()
    {
        animHandL = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) animHandL.SetTrigger("HandLPhoneTrigger");
    }
}
