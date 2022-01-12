using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ReadyButton : MonoBehaviour
{
    public void SetReady()
    {
        Launcher.instance.SetReady(true);
    }
}