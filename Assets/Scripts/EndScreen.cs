using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Invoke("Leave", 1f);
    }

    void Leave()
    {
        PhotonNetwork.Disconnect();
    }
    public void LoadScene(int index)
    {
        PhotonNetwork.LoadLevel(index);
    }
}