using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.UI;

public class SetRoleButton : MonoBehaviour
{
    ExitGames.Client.Photon.Hashtable _roleID = new ExitGames.Client.Photon.Hashtable();
    [SerializeField] RoleButtonsParent parent;

    public void SetRole(int index)
    {
        //if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("RoleID"))
        //{
        //    parent.BlockButtons(index - 1, (int)PhotonNetwork.LocalPlayer.CustomProperties["RoleID"] - 1);
        //}
        //else
        //{
        //    parent.BlockButtons(index - 1);
        //}
        //if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("Ready")) { _roleID["Ready"] = false; }

        _roleID["RoleID"] = index;
        PhotonNetwork.LocalPlayer.CustomProperties = _roleID;
        PhotonNetwork.SetPlayerCustomProperties(_roleID);//this one sh***y line gave me a lot of nerves

        parent.BlockButtons();

        Launcher.instance.RoleDisplay();
    }

    private void Start()
    {
        Launcher.instance.RoleDisplay();
    }
}