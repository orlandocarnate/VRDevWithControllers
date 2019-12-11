using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OculusInputFocus : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        OVRManager.InputFocusAcquired += OnInputFocusAcquired;
        OVRManager.InputFocusLost += OnInputFocusLost;
    }

    private void OnInputFocusAcquired()
    {
        Debug.Log("Input Focus Acquired, resume the game");
        Time.timeScale = 1;
    }

    private void OnInputFocusLost()
    {
        Debug.Log("Input Focus Lost, pause");
        Time.timeScale = 0;
    }
}
