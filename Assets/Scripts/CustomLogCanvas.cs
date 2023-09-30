using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CustomLogCanvas : MonoBehaviour
{
    public static CustomLogCanvas Instance;

    [SerializeField] private TMP_Text textLog;

    private void Awake() 
    {
        Instance = this;
        textLog.text = "";
        #if !UNITY_EDITOR
            textLog.enabled = false;
        #endif

        #if UNITY_EDITOR
            textLog.enabled = true;
        #endif
    }

    public void AddLog(string info)
    {
        textLog.text += "\n" + info;
    }


}
