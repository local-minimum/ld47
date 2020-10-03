using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInteractable : MonoBehaviour
{

    static UIInteractable _instance;

    public static void Place(Transform target)
    {
        Debug.Log(target);
    }

    public static bool canInteract {
        set
        {

        }
    }

    public static void Hide()
    {
        Debug.Log("Hidden");
    }

    private void OnDestroy()
    {
        _instance = null;
    }
}
