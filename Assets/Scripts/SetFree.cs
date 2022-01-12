using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetFree : MonoBehaviour
{
    [SerializeField] int clickAmount = 10;
    int currentClicksAmount;
    [SerializeField] Image bar;
    [SerializeField] PlayerController controller;

    private void OnEnable()
    {
        currentClicksAmount = 0;
        bar.fillAmount = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            LoadBar();
        }
    }

    void LoadBar()
    {
        currentClicksAmount++;
        if (currentClicksAmount >= clickAmount)
        {
            controller.GetComponent<ICatchable>()?.Catch(false);
        }
        else
        {
            FillBar();
        }
    }

    void FillBar()
    {
        bar.fillAmount = (float)currentClicksAmount / (float)clickAmount;
    }
}
