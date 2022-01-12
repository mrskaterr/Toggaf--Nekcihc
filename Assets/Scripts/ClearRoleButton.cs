using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ClearRoleButton : MonoBehaviour
{
    ExitGames.Client.Photon.Hashtable _roleID = new ExitGames.Client.Photon.Hashtable();
    [SerializeField] RoleButtonsParent parent;

    public void ClearRole()
    {
        if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("RoleID"))
        {
            _roleID["RoleID"] = 0;
            PhotonNetwork.LocalPlayer.CustomProperties = _roleID;
            PhotonNetwork.SetPlayerCustomProperties(_roleID);
            parent.BlockButtons();
            Launcher.instance.RoleDisplay();
        }
    }
}