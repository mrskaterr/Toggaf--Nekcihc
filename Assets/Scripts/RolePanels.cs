using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RolePanels : MonoBehaviour
{
    [SerializeField] GameObject astroPanel, blobPanel, nonePanel;

    private void Update()
    {
        if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("RoleID") && (int)PhotonNetwork.LocalPlayer.CustomProperties["RoleID"] > 0)
        {
            int tmp = (int)PhotonNetwork.LocalPlayer.CustomProperties["RoleID"];
            if (tmp == 1) { SetPanels(true, false, false); }
            if (tmp == 2 || tmp == 3) { SetPanels(false, true, false); }
        }
        else
        {
            SetPanels(false, false, true);
        }
    }

    void SetPanels(bool _astro, bool _blob, bool _none)
    {
        astroPanel.SetActive(_astro);
        blobPanel.SetActive(_blob);
        nonePanel.SetActive(_none);
    }
}