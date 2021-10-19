using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BlockWhenEmpty : MonoBehaviour
{
    [SerializeField] TMP_InputField inputField;
    [SerializeField] Button[] buttons;

    public void SetInteractivity()
    {
        //bool block = string.IsNullOrEmpty(inputField.text);

        if (!string.IsNullOrEmpty(inputField.text) && inputField.text.Length >= 3)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].interactable = true;
            } 
        }
        else
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].interactable = false;
            }
        }
    }

    private void Update()
    {
        SetInteractivity();
    }
}