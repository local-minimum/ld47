using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LevelResetter : MonoBehaviour
{
    private static List<LevelResetter> _resetters = new List<LevelResetter>();

    public static void ResetLevel()
    {
        foreach (LevelResetter resetter in _resetters)
        {
            resetter.ResetMe();
        }
    }

    private void Awake()
    {
        _resetters.Add(this);
    }

    private void OnDestroy()
    {
        _resetters.Remove(this);
    }

    abstract protected void ResetMe();
}
