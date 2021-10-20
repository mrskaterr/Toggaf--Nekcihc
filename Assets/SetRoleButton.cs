using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class SetRoleButton : MonoBehaviour
{
    ExitGames.Client.Photon.Hashtable _roleID = new ExitGames.Client.Photon.Hashtable();

    public void SetRole(int index)
    {
        _roleID["RoleID"] = index;
        PhotonNetwork.LocalPlayer.CustomProperties = _roleID;
        PhotonNetwork.SetPlayerCustomProperties(_roleID);//this one sh***y line gave me a lot of nerves
    }
}