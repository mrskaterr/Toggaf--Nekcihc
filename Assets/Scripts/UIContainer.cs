using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIContainer : MonoBehaviour
{
    [SerializeField] GameObject crosshair;
    [SerializeField] GameObject crosshair_bg;

    public void ToggleCrosshair(bool _p)
    {
        crosshair.SetActive(_p);
        crosshair_bg.SetActive(_p);
        GetComponent<Interacting>().enabled = _p;
    }
}