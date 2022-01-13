using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class RoleButtonsParent : MonoBehaviour
{
    [SerializeField] PhotonView PV;
    [SerializeField] Button[] buttons;

    public void BlockButtons()
    {
        PV.RPC("RPC_BlockButton", RpcTarget.All);
    }

    private void OnEnable()
    {
        BlockButtons();
    }

    [PunRPC]
    void RPC_BlockButton()
    {
        //buttons[_cur].interactable = false;
        //if (_pre != -1) { buttons[_pre].interactable = true; }
        List<int> indexes = new List<int>();
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            if (PhotonNetwork.PlayerList[i].CustomProperties.ContainsKey("RoleID"))
            {
                indexes.Add((int)PhotonNetwork.PlayerList[i].CustomProperties["RoleID"] - 1);
            }
        }
        for (int i = 0; i < buttons.Length; i++)
        {
            if (indexes.Contains(i)) { buttons[i].interactable = false; }
            else { buttons[i].interactable = true; }
        }
    }
}