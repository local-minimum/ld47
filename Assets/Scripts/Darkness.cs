using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Darkness : MonoBehaviour
{
    
    [SerializeField,Range(0, 1)]
    float intensity = 0.02f;

    void Start()
    {
        Light light = GetComponent<Light>();
        light.intensity = intensity;
        light.transform.rotation = Quaternion.identity;
        light.transform.Rotate(new Vector3(90, 0, 0));
    }
}
