using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClippySpeed : MonoBehaviour
{
    Rigidbody clippy;
    Animator animMonster;

    // Start is called before the first frame update
    void Start()
    {
        clippy = GetComponent<Rigidbody>();
        animMonster = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        var v = Input.GetAxis("Vertical");
        animMonster.SetFloat("Speed", v);
    }
}
