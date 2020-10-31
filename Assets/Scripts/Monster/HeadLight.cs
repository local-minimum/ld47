using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadLight : MonoBehaviour
{
    [SerializeField] Transform parent;

    private void Awake()
    {
        transform.SetParent(parent, true);
    }
}
