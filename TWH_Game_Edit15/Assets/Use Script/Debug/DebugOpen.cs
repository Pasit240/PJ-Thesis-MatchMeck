using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class DebugOpen : MonoBehaviour
{
    public GameObject debug;

    public bool _isDebug;

    private void Start()
    {
        _isDebug = false;
        debug.SetActive(false);
    }

    private void Update()
    {
        if (InputManager._isDebugOpen && !_isDebug)
        {
            OnPenDebug();
        }

        else if (InputManager._isDebugOpen && _isDebug)
        {
            CloseDebug();
        }
    }

    private void OnPenDebug()
    {
        debug.SetActive(true);
        _isDebug = true;
    }

    private void CloseDebug()
    {
        debug.SetActive(false);
        _isDebug = false;
    }
}
