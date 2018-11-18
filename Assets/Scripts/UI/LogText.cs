using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogText : MonoBehaviour {
    /// <summary>
    /// Used for printing Debug.Log info to the UI
    /// </summary>

    public string output = "";
    public string stack = "";

    private void Update()
    {
        GetComponent<Text>().text = output;
    }

    void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        output = logString;
        stack = stackTrace;
    }
}
