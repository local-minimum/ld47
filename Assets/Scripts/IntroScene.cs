using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class IntroScene : MonoBehaviour
{
    [SerializeField, Range(0, 10)]
    float loadLevelAfter = 3f;

    private void Update()
    {
        if (Time.timeSinceLevelLoad > loadLevelAfter)
        {
            SceneManager.LoadScene("scenes/Level");
        }
    }
}
