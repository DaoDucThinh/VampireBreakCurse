using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    public static DialogueUI instance;
    public bool isDialog;
    public Action action;
    private void Awake()
    {
        instance = this;
    }
    public Text txt;
    public GameObject dialog;
    public void ShowDialog(string text)
    {
        txt.text = text;
        dialog.SetActive(true);
        isDialog = true;
    }
    private void Update()
    {
        if (!isDialog) return;
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("....");
            isDialog = false;
            dialog.SetActive(false);
            action();
            action = null;
        }
    }

}
