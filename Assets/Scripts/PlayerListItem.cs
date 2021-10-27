using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;

public class PlayerListItem : MonoBehaviourPunCallbacks
{
    Player player;
    [SerializeField] TMP_Text text;
    [SerializeField] Image image;
    [SerializeField] Sprite[] roleImages;

    public void SetUp(Player _player)
    {
        player = _player;
        text.text = _player.NickName;
        SetRoleDisplay();
    }

    public void SetRoleDisplay()
    {
        if (player.CustomProperties.ContainsKey("RoleID"))
        {
            int rIndex = (int)player.CustomProperties["RoleID"];
            if (rIndex < 1 && rIndex > 3)
            {
                return;
            }
            image.sprite = roleImages[rIndex - 1];
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (player == otherPlayer)
        {
            Destroy(gameObject);
        }
    }

    public override void OnLeftRoom()
    {
        Destroy(gameObject);
    }
}