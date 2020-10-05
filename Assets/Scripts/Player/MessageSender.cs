using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageSender : MonoBehaviour
{
    bool sent;
    [SerializeField]
    string from;
    [SerializeField]
    string body;

    private void OnTriggerEnter(Collider other)
    {

        if (!sent && other.tag == "Player")
        {
            TextApp.RecieveMessage(from, body);
            sent = true;
        }
    }
}
